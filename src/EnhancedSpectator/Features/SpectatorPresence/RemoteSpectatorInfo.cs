using System;
using EnhancedSpectator.Networking;

namespace EnhancedSpectator.Features.SpectatorPresence;

/// <summary>
/// Describes a remote modded spectator inferred to be watching the local player.
/// </summary>
public sealed class RemoteSpectatorInfo : IEquatable<RemoteSpectatorInfo>
{
    /// <summary>
    /// Creates a remote spectator info entry.
    /// </summary>
    public RemoteSpectatorInfo(
        ulong spectatorClientId,
        ulong spectatorSlotId,
        bool isWatchingLocalPlayer,
        long lastObservedTicks,
        SpectatorPoseState? poseState)
    {
        SpectatorClientId = spectatorClientId;
        SpectatorSlotId = spectatorSlotId;
        IsWatchingLocalPlayer = isWatchingLocalPlayer;
        LastObservedTicks = lastObservedTicks;
        PoseState = poseState;
    }

    /// <summary>
    /// Gets the remote spectator Netcode client id.
    /// </summary>
    public ulong SpectatorClientId { get; }

    /// <summary>
    /// Gets the remote spectator player slot id.
    /// </summary>
    public ulong SpectatorSlotId { get; }

    /// <summary>
    /// Gets whether this spectator's current vanilla target is the local player.
    /// </summary>
    public bool IsWatchingLocalPlayer { get; }

    /// <summary>
    /// Gets when the remote spectator state was last observed.
    /// </summary>
    public long LastObservedTicks { get; }

    /// <summary>
    /// Gets the last received camera pose for this spectator when available.
    /// </summary>
    public SpectatorPoseState? PoseState { get; }

    /// <inheritdoc />
    public bool Equals(RemoteSpectatorInfo? other)
    {
        return other != null
            && SpectatorClientId == other.SpectatorClientId
            && SpectatorSlotId == other.SpectatorSlotId;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as RemoteSpectatorInfo);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        int hash = SpectatorClientId.GetHashCode();
        hash = (hash * 397) ^ SpectatorSlotId.GetHashCode();
        return hash;
    }
}
