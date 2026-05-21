using UnityEngine;

namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Pure rotation rules for floating-head visual orientation.
/// </summary>
public static class FloatingHeadRotationRules
{
    /// <summary>
    /// Default pitch correction for the detached-head template.
    /// </summary>
    public const float DefaultRuntimeDetachedHeadPitchOffsetDegrees = -90f;

    /// <summary>
    /// Default yaw correction for the detached-head template.
    /// </summary>
    public const float DefaultRuntimeDetachedHeadYawOffsetDegrees = 360f;

    /// <summary>
    /// Default roll correction for the detached-head template.
    /// </summary>
    public const float DefaultRuntimeDetachedHeadRollOffsetDegrees = 0f;

    /// <summary>
    /// Gets whether the visual should face the local camera instead of the remote spectator pose.
    /// </summary>
    public static bool ShouldFaceLocalCamera(FloatingHeadVisualSourceKind sourceKind, bool faceCameraConfig)
    {
        return faceCameraConfig && sourceKind != FloatingHeadVisualSourceKind.RuntimeDetachedHead;
    }

    /// <summary>
    /// Applies model-space rotation offsets to a remote spectator pose.
    /// </summary>
    public static Quaternion ApplyRuntimeDetachedHeadOffset(
        Quaternion remoteRotation,
        float pitchOffsetDegrees,
        float yawOffsetDegrees,
        float rollOffsetDegrees)
    {
        return remoteRotation * Quaternion.Euler(pitchOffsetDegrees, yawOffsetDegrees, rollOffsetDegrees);
    }
}
