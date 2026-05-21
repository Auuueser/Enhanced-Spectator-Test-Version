using System;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Represents mod-owned spectator target identity without exposing game objects.
/// </summary>
public sealed class SpectatorTargetState : IEquatable<SpectatorTargetState>
{
    /// <summary>
    /// Creates a spectator target state.
    /// </summary>
    public SpectatorTargetState(
        bool isSpectating,
        ulong localClientId,
        ulong localPlayerSlotId,
        ulong? targetClientId,
        ulong? targetPlayerSlotId,
        long timestampTicks)
    {
        IsSpectating = isSpectating;
        LocalClientId = localClientId;
        LocalPlayerSlotId = localPlayerSlotId;
        TargetClientId = targetClientId;
        TargetPlayerSlotId = targetPlayerSlotId;
        TimestampTicks = timestampTicks;
    }

    /// <summary>
    /// Gets whether the local player is in the vanilla dead spectator state.
    /// </summary>
    public bool IsSpectating { get; }

    /// <summary>
    /// Gets the local Netcode client id.
    /// </summary>
    public ulong LocalClientId { get; }

    /// <summary>
    /// Gets the local player slot id.
    /// </summary>
    public ulong LocalPlayerSlotId { get; }

    /// <summary>
    /// Gets the spectated target Netcode client id.
    /// </summary>
    public ulong? TargetClientId { get; }

    /// <summary>
    /// Gets the spectated target player slot id.
    /// </summary>
    public ulong? TargetPlayerSlotId { get; }

    /// <summary>
    /// Gets the timestamp captured for this state.
    /// </summary>
    public long TimestampTicks { get; }

    /// <inheritdoc />
    public bool Equals(SpectatorTargetState? other)
    {
        return other != null
            && IsSpectating == other.IsSpectating
            && LocalClientId == other.LocalClientId
            && LocalPlayerSlotId == other.LocalPlayerSlotId
            && TargetClientId == other.TargetClientId
            && TargetPlayerSlotId == other.TargetPlayerSlotId;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as SpectatorTargetState);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        int hash = IsSpectating ? 17 : 31;
        hash = (hash * 397) ^ LocalClientId.GetHashCode();
        hash = (hash * 397) ^ LocalPlayerSlotId.GetHashCode();
        hash = (hash * 397) ^ TargetClientId.GetHashCode();
        hash = (hash * 397) ^ TargetPlayerSlotId.GetHashCode();
        return hash;
    }
}
