using EnhancedSpectator.Features.VoiceActivity;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Reads confirmed vanilla and Dissonance voice activity without modifying voice routing.
/// </summary>
public interface IGameVoiceActivityAdapter
{
    /// <summary>
    /// Attempts to read voice activity for the requested player.
    /// </summary>
    bool TryGetVoiceActivity(ulong clientId, ulong slotId, out VoiceActivityState state);
}
