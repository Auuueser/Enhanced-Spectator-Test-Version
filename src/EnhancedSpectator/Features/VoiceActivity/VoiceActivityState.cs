using UnityEngine;

namespace EnhancedSpectator.Features.VoiceActivity;

/// <summary>
/// Read-only voice activity snapshot for a player.
/// </summary>
public sealed class VoiceActivityState
{
    /// <summary>
    /// Creates a voice activity snapshot.
    /// </summary>
    public VoiceActivityState(
        bool hasData,
        bool isSpeaking,
        float amplitude,
        float volume,
        ulong clientId,
        ulong slotId,
        long timestampTicks)
    {
        HasData = hasData;
        IsSpeaking = isSpeaking;
        Amplitude = Mathf.Clamp01(amplitude);
        Volume = Mathf.Clamp01(volume);
        ClientId = clientId;
        SlotId = slotId;
        TimestampTicks = timestampTicks;
    }

    /// <summary>
    /// Gets an empty no-data voice state.
    /// </summary>
    public static VoiceActivityState NoData { get; } = new VoiceActivityState(false, false, 0f, 0f, 0, 0, 0);

    /// <summary>
    /// Gets whether voice activity data was available.
    /// </summary>
    public bool HasData { get; }

    /// <summary>
    /// Gets whether the player is currently speaking according to local voice state.
    /// </summary>
    public bool IsSpeaking { get; }

    /// <summary>
    /// Gets the local observed voice amplitude clamped to 0..1.
    /// </summary>
    public float Amplitude { get; }

    /// <summary>
    /// Gets the local observed voice playback volume clamped to 0..1.
    /// </summary>
    public float Volume { get; }

    /// <summary>
    /// Gets the Netcode client id for the represented player.
    /// </summary>
    public ulong ClientId { get; }

    /// <summary>
    /// Gets the player slot id for the represented player.
    /// </summary>
    public ulong SlotId { get; }

    /// <summary>
    /// Gets when this state was observed.
    /// </summary>
    public long TimestampTicks { get; }
}
