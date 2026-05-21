using EnhancedSpectator.Features.VoiceDiagnostics;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Adapter boundary for reading local vanilla and Dissonance voice diagnostics.
/// </summary>
public interface IGameVoiceDiagnosticsAdapter
{
    /// <summary>
    /// Attempts to read a current voice diagnostics snapshot.
    /// </summary>
    bool TryGetVoiceDiagnosticsSnapshot(
        bool includeLocalPlayer,
        bool includeRemotePlayers,
        bool includeAudioSourceDiagnostics,
        bool includeWalkieDiagnostics,
        out VoiceDiagnosticsSnapshot snapshot);
}
