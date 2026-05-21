using System;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Describes a spectator target synchronization message payload.
/// </summary>
public sealed class SpectatorTargetSyncMessage
{
    /// <summary>
    /// Creates a spectator target sync message.
    /// </summary>
    public SpectatorTargetSyncMessage(int protocolVersion, SpectatorTargetState state, long messageTicks)
    {
        ProtocolVersion = protocolVersion;
        State = state ?? throw new ArgumentNullException(nameof(state));
        MessageTicks = messageTicks;
    }

    /// <summary>
    /// Gets the message protocol version.
    /// </summary>
    public int ProtocolVersion { get; }

    /// <summary>
    /// Gets the spectator target state carried by this message.
    /// </summary>
    public SpectatorTargetState State { get; }

    /// <summary>
    /// Gets when the message was created.
    /// </summary>
    public long MessageTicks { get; }
}
