namespace EnhancedSpectator.Features.VoiceRouting;

/// <summary>
/// Describes local playback settings for one routed spectator voice source.
/// </summary>
public readonly struct SpectatorVoicePlaybackSettings
{
    /// <summary>
    /// Creates playback settings for a routed spectator voice source.
    /// </summary>
    public SpectatorVoicePlaybackSettings(
        float volume,
        bool useRemotePosePosition,
        bool enableDistanceAttenuation,
        float minDistance,
        float maxDistance,
        float rolloffPower,
        float minimumVolume,
        bool fallbackTo2DWhenPoseMissing)
    {
        Volume = volume;
        UseRemotePosePosition = useRemotePosePosition;
        EnableDistanceAttenuation = enableDistanceAttenuation;
        MinDistance = minDistance;
        MaxDistance = maxDistance;
        RolloffPower = rolloffPower;
        MinimumVolume = minimumVolume;
        FallbackTo2DWhenPoseMissing = fallbackTo2DWhenPoseMissing;
    }

    /// <summary>
    /// Gets the configured base route volume before distance attenuation.
    /// </summary>
    public float Volume { get; }

    /// <summary>
    /// Gets whether the synced spectator camera pose should position the voice source.
    /// </summary>
    public bool UseRemotePosePosition { get; }

    /// <summary>
    /// Gets whether volume should decrease as the listener moves away from the remote spectator pose.
    /// </summary>
    public bool EnableDistanceAttenuation { get; }

    /// <summary>
    /// Gets the distance that keeps full routed volume.
    /// </summary>
    public float MinDistance { get; }

    /// <summary>
    /// Gets the distance where routed volume reaches the configured minimum.
    /// </summary>
    public float MaxDistance { get; }

    /// <summary>
    /// Gets the rolloff curve exponent used between minimum and maximum distance.
    /// </summary>
    public float RolloffPower { get; }

    /// <summary>
    /// Gets the minimum volume multiplier applied at or beyond maximum distance.
    /// </summary>
    public float MinimumVolume { get; }

    /// <summary>
    /// Gets whether missing pose data should fall back to the previous 2D route behavior.
    /// </summary>
    public bool FallbackTo2DWhenPoseMissing { get; }
}
