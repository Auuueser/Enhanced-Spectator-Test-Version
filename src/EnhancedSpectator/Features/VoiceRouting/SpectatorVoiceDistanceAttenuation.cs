using System;

namespace EnhancedSpectator.Features.VoiceRouting;

/// <summary>
/// Calculates local playback volume for positional spectator voice routes.
/// </summary>
public static class SpectatorVoiceDistanceAttenuation
{
    /// <summary>
    /// Calculates the routed voice volume after optional distance attenuation.
    /// </summary>
    public static float CalculateVolume(
        float baseVolume,
        bool attenuationEnabled,
        float distance,
        float minDistance,
        float maxDistance,
        float rolloffPower,
        float minimumVolume)
    {
        float clampedBaseVolume = Clamp01(baseVolume);
        if (!attenuationEnabled)
        {
            return clampedBaseVolume;
        }

        float safeMinDistance = MathF.Max(0f, minDistance);
        float safeMaxDistance = MathF.Max(safeMinDistance + 0.01f, maxDistance);
        float safeDistance = MathF.Max(0f, distance);
        float safeRolloffPower = MathF.Max(0.01f, rolloffPower);
        float clampedMinimumVolume = Clamp01(minimumVolume);

        if (safeDistance <= safeMinDistance)
        {
            return clampedBaseVolume;
        }

        if (safeDistance >= safeMaxDistance)
        {
            return clampedBaseVolume * clampedMinimumVolume;
        }

        float normalizedDistance = (safeDistance - safeMinDistance) / (safeMaxDistance - safeMinDistance);
        float attenuation = MathF.Pow(normalizedDistance, safeRolloffPower);
        float multiplier = Lerp(1f, clampedMinimumVolume, attenuation);
        return clampedBaseVolume * multiplier;
    }

    private static float Clamp01(float value)
    {
        if (value <= 0f)
        {
            return 0f;
        }

        return value >= 1f ? 1f : value;
    }

    private static float Lerp(float from, float to, float t)
    {
        return from + ((to - from) * Clamp01(t));
    }
}
