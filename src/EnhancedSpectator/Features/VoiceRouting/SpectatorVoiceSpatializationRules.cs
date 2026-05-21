using UnityEngine;

namespace EnhancedSpectator.Features.VoiceRouting;

/// <summary>
/// Pure rules for mapping remote spectator voice poses into Unity's active audio listener frame.
/// </summary>
public static class SpectatorVoiceSpatializationRules
{
    /// <summary>
    /// Resolves the AudioSource position that preserves the perceived remote pose when the rendered listener
    /// camera differs from Unity's active AudioListener.
    /// </summary>
    public static Vector3 ResolvePlaybackSourcePosition(
        Vector3 remotePosePosition,
        Vector3 desiredListenerPosition,
        Quaternion desiredListenerRotation,
        Vector3 actualListenerPosition,
        Quaternion actualListenerRotation)
    {
        Vector3 desiredLocalOffset = RotateByQuaternion(
            remotePosePosition - desiredListenerPosition,
            Inverse(desiredListenerRotation));
        return actualListenerPosition + RotateByQuaternion(desiredLocalOffset, actualListenerRotation);
    }

    private static Quaternion Inverse(Quaternion value)
    {
        float lengthSquared = (value.x * value.x)
            + (value.y * value.y)
            + (value.z * value.z)
            + (value.w * value.w);
        if (lengthSquared <= 0.000001f)
        {
            return Quaternion.identity;
        }

        float inverseLengthSquared = 1f / lengthSquared;
        return new Quaternion(
            -value.x * inverseLengthSquared,
            -value.y * inverseLengthSquared,
            -value.z * inverseLengthSquared,
            value.w * inverseLengthSquared);
    }

    private static Vector3 RotateByQuaternion(Vector3 value, Quaternion rotation)
    {
        float x = rotation.x * 2f;
        float y = rotation.y * 2f;
        float z = rotation.z * 2f;
        float xx = rotation.x * x;
        float yy = rotation.y * y;
        float zz = rotation.z * z;
        float xy = rotation.x * y;
        float xz = rotation.x * z;
        float yz = rotation.y * z;
        float wx = rotation.w * x;
        float wy = rotation.w * y;
        float wz = rotation.w * z;

        return new Vector3(
            ((1f - (yy + zz)) * value.x) + ((xy - wz) * value.y) + ((xz + wy) * value.z),
            ((xy + wz) * value.x) + ((1f - (xx + zz)) * value.y) + ((yz - wx) * value.z),
            ((xz - wy) * value.x) + ((yz + wx) * value.y) + ((1f - (xx + yy)) * value.z));
    }
}
