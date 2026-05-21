using System.Collections.Generic;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Stores mod-owned identity data received from remote peers.
/// </summary>
public sealed class RemotePeerIdentityRegistry
{
    private readonly Dictionary<ulong, PeerIdentityState> _identities = new Dictionary<ulong, PeerIdentityState>();

    /// <summary>
    /// Registers or updates a remote peer identity.
    /// </summary>
    public void Update(PeerIdentityState state)
    {
        if (_identities.TryGetValue(state.ClientId, out PeerIdentityState existing))
        {
            if (state.TimestampTicks < existing.TimestampTicks)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(state.VoicePlayerName)
                && !string.IsNullOrWhiteSpace(existing.VoicePlayerName))
            {
                state = new PeerIdentityState(
                    state.ClientId,
                    state.PlayerSlotId,
                    state.DisplayName,
                    existing.VoicePlayerName,
                    state.TimestampTicks);
            }
        }

        _identities[state.ClientId] = state;
    }

    /// <summary>
    /// Attempts to get the last known identity for a peer.
    /// </summary>
    public bool TryGet(ulong clientId, out PeerIdentityState state)
    {
        return _identities.TryGetValue(clientId, out state!);
    }

    /// <summary>
    /// Gets a copy of all stored identities.
    /// </summary>
    public List<PeerIdentityState> GetSnapshot()
    {
        return new List<PeerIdentityState>(_identities.Values);
    }

    /// <summary>
    /// Removes one peer identity.
    /// </summary>
    public void Remove(ulong clientId)
    {
        _identities.Remove(clientId);
    }

    /// <summary>
    /// Removes all stored identities.
    /// </summary>
    public void Clear()
    {
        _identities.Clear();
    }
}
