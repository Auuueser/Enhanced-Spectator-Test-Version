namespace EnhancedSpectator.Networking;

/// <summary>
/// Tracks the Enhanced Spectator capability state known for one Netcode peer.
/// </summary>
public sealed class ModPeerCapability
{
    /// <summary>
    /// Creates a peer capability entry.
    /// </summary>
    public ModPeerCapability(
        ulong clientId,
        int protocolVersion,
        bool supportsCapabilityHandshake,
        bool supportsSpectatorTargetSync,
        bool handshakeComplete,
        long lastSeenTicks,
        bool supportsVoiceActivitySync = false,
        bool supportsSpectatorVoiceToTarget = false)
    {
        ClientId = clientId;
        ProtocolVersion = protocolVersion;
        SupportsCapabilityHandshake = supportsCapabilityHandshake;
        SupportsSpectatorTargetSync = supportsSpectatorTargetSync;
        HandshakeComplete = handshakeComplete;
        LastSeenTicks = lastSeenTicks;
        SupportsVoiceActivitySync = supportsVoiceActivitySync;
        SupportsSpectatorVoiceToTarget = supportsSpectatorVoiceToTarget;
    }

    /// <summary>
    /// Gets the Netcode client id for the peer.
    /// </summary>
    public ulong ClientId { get; }

    /// <summary>
    /// Gets the peer protocol version.
    /// </summary>
    public int ProtocolVersion { get; }

    /// <summary>
    /// Gets whether the peer supports capability handshake messages.
    /// </summary>
    public bool SupportsCapabilityHandshake { get; }

    /// <summary>
    /// Gets whether the peer supports spectator target sync messages.
    /// </summary>
    public bool SupportsSpectatorTargetSync { get; }

    /// <summary>
    /// Gets whether the peer supports visual-only voice activity sync messages.
    /// </summary>
    public bool SupportsVoiceActivitySync { get; }

    /// <summary>
    /// Gets whether the peer opts into spectator-to-target voice playback.
    /// </summary>
    public bool SupportsSpectatorVoiceToTarget { get; }

    /// <summary>
    /// Gets whether this peer completed a mod capability handshake.
    /// </summary>
    public bool HandshakeComplete { get; }

    /// <summary>
    /// Gets when this peer was last observed.
    /// </summary>
    public long LastSeenTicks { get; }
}
