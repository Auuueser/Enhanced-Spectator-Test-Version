using System.Collections.Generic;

namespace EnhancedSpectator.Features.SpectatorPresence;

/// <summary>
/// Current remote spectators visible to the local player.
/// </summary>
public sealed class LocalSpectatorPresenceState
{
    /// <summary>
    /// Creates a local spectator presence state.
    /// </summary>
    public LocalSpectatorPresenceState(bool hasLocalPlayer, IReadOnlyList<RemoteSpectatorInfo> remoteSpectators)
    {
        HasLocalPlayer = hasLocalPlayer;
        RemoteSpectators = remoteSpectators;
    }

    /// <summary>
    /// Gets whether local player identity was available when this state was captured.
    /// </summary>
    public bool HasLocalPlayer { get; }

    /// <summary>
    /// Gets remote spectators visible to the local player.
    /// </summary>
    public IReadOnlyList<RemoteSpectatorInfo> RemoteSpectators { get; }

    /// <summary>
    /// Gets an empty presence state.
    /// </summary>
    public static LocalSpectatorPresenceState Empty { get; } =
        new LocalSpectatorPresenceState(false, new List<RemoteSpectatorInfo>().AsReadOnly());
}
