using System;

namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Calculates visual scale factors from local voice activity.
/// </summary>
public static class FloatingHeadVoiceScaleRules
{
    /// <summary>
    /// Default fallback voice level when a speaker is active but amplitude is unavailable.
    /// </summary>
    public const float DefaultMinimumSpeakingVoiceLevel = 0.35f;

    /// <summary>
    /// Default extra pulse amount applied at peak speaking pulse.
    /// </summary>
    public const float DefaultSpeakingPulseAmount = 0.32f;

    /// <summary>
    /// Default smoothing time used when voice activity starts.
    /// </summary>
    public const float DefaultVoiceAttackSmoothTime = 0.005f;

    /// <summary>
    /// Default smoothing time used when voice activity stops.
    /// </summary>
    public const float DefaultVoiceReleaseSmoothTime = 0.008f;

    /// <summary>
    /// Resolves a normalized voice level from speaking state and amplitude.
    /// </summary>
    public static float ResolveTargetVoiceLevel(
        bool hasVoiceData,
        bool isSpeaking,
        float amplitude,
        float minimumSpeakingVoiceLevel)
    {
        if (!hasVoiceData)
        {
            return 0f;
        }

        if (!isSpeaking)
        {
            return 0f;
        }

        float clampedAmplitude = Clamp01(amplitude);
        if (clampedAmplitude > 0f)
        {
            return clampedAmplitude;
        }

        return Clamp01(minimumSpeakingVoiceLevel);
    }

    /// <summary>
    /// Resolves a scale multiplier from normalized voice level and pulse state.
    /// </summary>
    public static float ResolveScaleMultiplier(
        float silenceScaleMultiplier,
        float speakingScaleMultiplier,
        float voiceLevel,
        bool pulseWhenSpeaking,
        float speakingPulseAmount,
        float pulse01)
    {
        float clampedVoiceLevel = Clamp01(voiceLevel);
        float scaleMultiplier = Lerp(
            Math.Max(0.01f, silenceScaleMultiplier),
            Math.Max(0.01f, speakingScaleMultiplier),
            clampedVoiceLevel);

        if (pulseWhenSpeaking && clampedVoiceLevel > 0.01f)
        {
            scaleMultiplier *= 1f + (clampedVoiceLevel * Math.Max(0f, speakingPulseAmount) * Clamp01(pulse01));
        }

        return Math.Max(0.01f, scaleMultiplier);
    }

    /// <summary>
    /// Chooses attack or release smoothing based on whether voice level rises or falls.
    /// </summary>
    public static float ResolveVoiceSmoothTime(
        float currentLevel,
        float targetLevel,
        float attackSmoothTime,
        float releaseSmoothTime)
    {
        return targetLevel > currentLevel
            ? Math.Max(0f, attackSmoothTime)
            : Math.Max(0f, releaseSmoothTime);
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
