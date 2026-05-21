namespace EnhancedSpectator.Features.SpectatorPresence;

/// <summary>
/// Read-only source of remote spectators visible to the local player.
/// </summary>
public interface ISpectatorPresenceProvider
{
    /// <summary>
    /// Gets the latest local spectator visibility snapshot.
    /// </summary>
    LocalSpectatorPresenceState Current { get; }
}
