namespace EnhancedSpectator.Networking;

/// <summary>
/// Carries mod-owned identity data for one connected peer.
/// </summary>
public sealed class PeerIdentityState
{
    /// <summary>
    /// Creates a peer identity state.
    /// </summary>
    public PeerIdentityState(ulong clientId, ulong playerSlotId, string displayName, long timestampTicks)
        : this(clientId, playerSlotId, displayName, string.Empty, timestampTicks)
    {
    }

    /// <summary>
    /// Creates a peer identity state with a Dissonance voice player id.
    /// </summary>
    public PeerIdentityState(
        ulong clientId,
        ulong playerSlotId,
        string displayName,
        string voicePlayerName,
        long timestampTicks)
    {
        ClientId = clientId;
        PlayerSlotId = playerSlotId;
        DisplayName = displayName;
        VoicePlayerName = voicePlayerName;
        TimestampTicks = timestampTicks;
    }

    /// <summary>
    /// Gets the Netcode client id for the peer.
    /// </summary>
    public ulong ClientId { get; }

    /// <summary>
    /// Gets the Lethal Company player slot id for the peer.
    /// </summary>
    public ulong PlayerSlotId { get; }

    /// <summary>
    /// Gets the peer display name.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Gets the local Dissonance player id advertised by this peer, when available.
    /// </summary>
    public string VoicePlayerName { get; }

    /// <summary>
    /// Gets when this identity state was observed.
    /// </summary>
    public long TimestampTicks { get; }
}
