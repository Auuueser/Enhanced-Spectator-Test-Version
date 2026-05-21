namespace EnhancedSpectator.Features.VoiceActivity;

/// <summary>
/// Provides read-only voice activity for remote spectator visuals.
/// </summary>
public interface IVoiceActivityProvider
{
    /// <summary>
    /// Attempts to read local voice activity data for a player.
    /// </summary>
    bool TryGetVoiceActivity(ulong clientId, ulong slotId, out VoiceActivityState state);
}
