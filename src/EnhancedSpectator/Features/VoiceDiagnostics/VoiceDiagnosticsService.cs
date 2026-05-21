using System;
using EnhancedSpectator.Config;
using EnhancedSpectator.GameInterop;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Runtime;

namespace EnhancedSpectator.Features.VoiceDiagnostics;

/// <summary>
/// Runs one-shot read-only voice diagnostics and writes findings to the BepInEx log.
/// </summary>
public sealed class VoiceDiagnosticsService
{
    private readonly EnhancedSpectatorConfig _config;
    private readonly IGameVoiceDiagnosticsAdapter _adapter;

    /// <summary>
    /// Creates a voice diagnostics service.
    /// </summary>
    public VoiceDiagnosticsService(
        EnhancedSpectatorConfig config,
        IGameVoiceDiagnosticsAdapter adapter)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
    }

    /// <summary>
    /// Runs one voice diagnostics pass.
    /// </summary>
    public void InspectOnce()
    {
        try
        {
            if (!RuntimeConnectionState.CanRunLocalDiagnostics(out string reason))
            {
                ModLog.Info($"Enhanced Spectator voice diagnostics skipped because runtime state is unsafe: {reason}.");
                return;
            }

            bool includeLocal = _config.LogLocalVoiceStateOnKey.Value;
            bool includeRemote = _config.LogRemoteVoiceStatesOnKey.Value;
            if (!includeLocal && !includeRemote)
            {
                ModLog.Info("Enhanced Spectator voice diagnostics skipped: local and remote voice logging are both disabled.");
                return;
            }

            if (!_adapter.TryGetVoiceDiagnosticsSnapshot(
                includeLocal,
                includeRemote,
                _config.IncludeVoiceAudioSourceDetails.Value,
                _config.IncludeWalkieVoiceDiagnostics.Value,
                out VoiceDiagnosticsSnapshot snapshot))
            {
                ModLog.Info("Enhanced Spectator voice diagnostics found no current voice state.");
                return;
            }

            ModLog.Info(VoiceDiagnosticsReportFormatter.Build(snapshot));
        }
        catch (Exception ex)
        {
            ModLog.Error($"Enhanced Spectator voice diagnostics failed: {ex}");
        }
    }
}
