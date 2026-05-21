using System;
using EnhancedSpectator.Config;
using EnhancedSpectator.Features.Spectator;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Runtime;

namespace EnhancedSpectator.Features.VoiceDiagnostics;

/// <summary>
/// Feature module for key-triggered read-only voice diagnostics.
/// </summary>
public sealed class VoiceDiagnosticsModule : IFeatureModule, IRuntimeTickable
{
    private readonly EnhancedSpectatorConfig _config;
    private readonly VoiceDiagnosticsService _diagnosticsService;
    private bool _initialized;

    /// <summary>
    /// Creates a voice diagnostics module.
    /// </summary>
    public VoiceDiagnosticsModule(
        EnhancedSpectatorConfig config,
        VoiceDiagnosticsService diagnosticsService)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _diagnosticsService = diagnosticsService ?? throw new ArgumentNullException(nameof(diagnosticsService));
    }

    /// <inheritdoc />
    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;
        ModLog.Debug("Voice diagnostics module initialized.");
    }

    /// <inheritdoc />
    public void Tick()
    {
        if (!_initialized || !_config.EnableVoiceDiagnostics.Value)
        {
            return;
        }

        if (!SpectatorInputService.IsKeyPressedThisFrame(_config.VoiceDiagnosticsKey.Value))
        {
            return;
        }

        ModLog.Info($"Enhanced Spectator voice diagnostics key pressed: {_config.VoiceDiagnosticsKey.Value}.");
        _diagnosticsService.InspectOnce();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (!_initialized)
        {
            return;
        }

        _initialized = false;
        ModLog.Debug("Voice diagnostics module disposed.");
    }
}
