namespace EnhancedSpectator.Networking;

/// <summary>
/// Network message carrying one spectator camera pose snapshot.
/// </summary>
public sealed class SpectatorPoseSyncMessage
{
    /// <summary>
    /// Creates a spectator pose sync message.
    /// </summary>
    public SpectatorPoseSyncMessage(int protocolVersion, SpectatorPoseState state, long timestampTicks)
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
    /// Gets the pose state payload.
    /// </summary>
    public SpectatorPoseState State { get; }

    /// <summary>
    /// Gets when this message was created.
    /// </summary>
    public long TimestampTicks { get; }
}
