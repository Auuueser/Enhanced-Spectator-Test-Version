using EnhancedSpectator.Features.VoiceActivity;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Network message carrying one local voice activity snapshot for visual-only remote head scaling.
/// </summary>
public sealed class VoiceActivitySyncMessage
{
    /// <summary>
    /// Creates a voice activity sync message.
    /// </summary>
    public VoiceActivitySyncMessage(int protocolVersion, VoiceActivityState state, long timestampTicks)
    {
        ProtocolVersion = protocolVersion;
        State = state;
        TimestampTicks = timestampTicks;
    }

    /// <summary>
    /// Gets the protocol version used to encode this message.
    /// </summary>
    public int ProtocolVersion { get; }

    /// <summary>
    /// Gets the voice activity payload.
    /// </summary>
    public VoiceActivityState State { get; }

    /// <summary>
    /// Gets when this message was created.
    /// </summary>
    public long TimestampTicks { get; }
}
