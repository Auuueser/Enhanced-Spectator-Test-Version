using System;
using UnityEngine;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Describes a modded spectator's local camera pose for remote placeholder visuals.
/// </summary>
public sealed class SpectatorPoseState
{
    private const float PositionEpsilonSqr = 0.0004f;
    private const float RotationDotEpsilon = 0.9995f;

    /// <summary>
    /// Creates a spectator pose state snapshot.
    /// </summary>
    public SpectatorPoseState(
        bool isSpectating,
        ulong localClientId,
        ulong localPlayerSlotId,
        ulong? targetClientId,
        ulong? targetPlayerSlotId,
        Vector3 position,
        Quaternion rotation,
        long timestampTicks)
    {
        IsSpectating = isSpectating;
        LocalClientId = localClientId;
        LocalPlayerSlotId = localPlayerSlotId;
        TargetClientId = targetClientId;
        TargetPlayerSlotId = targetPlayerSlotId;
        Position = position;
        Rotation = rotation;
        TimestampTicks = timestampTicks;
    }

    /// <summary>
    /// Gets whether the local player is spectating.
    /// </summary>
    public bool IsSpectating { get; }

    /// <summary>
    /// Gets the spectator Netcode client id.
    /// </summary>
    public ulong LocalClientId { get; }

    /// <summary>
    /// Gets the spectator player slot id.
    /// </summary>
    public ulong LocalPlayerSlotId { get; }

    /// <summary>
    /// Gets the watched player's Netcode client id when available.
    /// </summary>
    public ulong? TargetClientId { get; }

    /// <summary>
    /// Gets the watched player's slot id when available.
    /// </summary>
    public ulong? TargetPlayerSlotId { get; }

    /// <summary>
    /// Gets the spectator camera world position.
    /// </summary>
    public Vector3 Position { get; }

    /// <summary>
    /// Gets the spectator camera world rotation.
    /// </summary>
    public Quaternion Rotation { get; }

    /// <summary>
    /// Gets when the pose was observed.
    /// </summary>
    public long TimestampTicks { get; }

    /// <summary>
    /// Gets whether this pose is close enough to another pose to skip a send.
    /// </summary>
    public bool ApproximatelyEquals(SpectatorPoseState? other)
    {
        if (other == null)
        {
            return false;
        }

        if (IsSpectating != other.IsSpectating
            || LocalClientId != other.LocalClientId
            || LocalPlayerSlotId != other.LocalPlayerSlotId
            || TargetClientId != other.TargetClientId
            || TargetPlayerSlotId != other.TargetPlayerSlotId)
        {
            return false;
        }

        float positionDeltaSqr = (Position - other.Position).sqrMagnitude;
        float rotationDot = Mathf.Abs(Quaternion.Dot(Rotation, other.Rotation));
        return positionDeltaSqr <= PositionEpsilonSqr && rotationDot >= RotationDotEpsilon;
    }
}
