using System.Collections.Generic;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Stores capability handshake state for local and remote Netcode peers.
/// </summary>
public sealed class RemotePeerRegistry
{
    private readonly Dictionary<ulong, ModPeerCapability> _capabilities = new Dictionary<ulong, ModPeerCapability>();
    private readonly HashSet<ulong> _relayedPeerIds = new HashSet<ulong>();

    /// <summary>
    /// Registers or updates the local peer capability.
    /// </summary>
    public void RegisterLocal(ModPeerCapability capability)
    {
        _capabilities[capability.ClientId] = capability;
        _relayedPeerIds.Remove(capability.ClientId);
    }

    /// <summary>
    /// Registers or updates a remote peer capability.
    /// </summary>
    public bool RegisterRemote(ModPeerCapability capability)
    {
        return RegisterRemote(capability, isRelayed: false);
    }

    /// <summary>
    /// Registers or updates a remote peer capability.
    /// </summary>
    public bool RegisterRemote(ModPeerCapability capability, bool isRelayed)
    {
        ModPeerCapability normalized = NormalizeRemoteCapability(capability);
        _capabilities[normalized.ClientId] = normalized;
        if (isRelayed)
        {
            _relayedPeerIds.Add(normalized.ClientId);
        }
        else
        {
            _relayedPeerIds.Remove(normalized.ClientId);
        }

        return normalized.HandshakeComplete;
    }

    /// <summary>
    /// Attempts to get a capability entry.
    /// </summary>
    public bool TryGetCapability(ulong clientId, out ModPeerCapability capability)
    {
        return _capabilities.TryGetValue(clientId, out capability!);
    }

    /// <summary>
    /// Gets a copy of all known peer capabilities.
    /// </summary>
    public List<ModPeerCapability> GetCapabilitiesSnapshot()
    {
        return new List<ModPeerCapability>(_capabilities.Values);
    }

    /// <summary>
    /// Gets compatible remote peers that can receive spectator target sync.
    /// </summary>
    public List<ulong> GetSpectatorTargetSyncPeerIds(ulong localClientId)
    {
        return GetSpectatorTargetSyncPeerIds(localClientId, includeRelayed: false);
    }

    /// <summary>
    /// Gets compatible remote peers that can receive spectator target sync.
    /// </summary>
    public List<ulong> GetSpectatorTargetSyncPeerIds(ulong localClientId, bool includeRelayed)
    {
        return GetPeerIds(localClientId, includeRelayed, ModPeerCapabilityRules.SupportsCurrentSpectatorTargetSync);
    }

    /// <summary>
    /// Gets compatible remote peers that can receive visual-only voice activity sync.
    /// </summary>
    public List<ulong> GetVoiceActivitySyncPeerIds(ulong localClientId)
    {
        return GetVoiceActivitySyncPeerIds(localClientId, includeRelayed: false);
    }

    /// <summary>
    /// Gets compatible remote peers that can receive visual-only voice activity sync.
    /// </summary>
    public List<ulong> GetVoiceActivitySyncPeerIds(ulong localClientId, bool includeRelayed)
    {
        return GetPeerIds(localClientId, includeRelayed, ModPeerCapabilityRules.SupportsCurrentVoiceActivitySync);
    }

    /// <summary>
    /// Gets whether any compatible remote peer can receive spectator target sync.
    /// </summary>
    public bool HasSpectatorTargetSyncPeer(ulong localClientId)
    {
        foreach (KeyValuePair<ulong, ModPeerCapability> pair in _capabilities)
        {
            if (pair.Key == localClientId)
            {
                continue;
            }

            ModPeerCapability capability = pair.Value;
            if (ModPeerCapabilityRules.SupportsCurrentSpectatorTargetSync(capability))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets whether a remote peer has completed a compatible target-sync handshake.
    /// </summary>
    public bool IsSpectatorTargetSyncPeer(ulong clientId)
    {
        return _capabilities.TryGetValue(clientId, out ModPeerCapability capability)
            && ModPeerCapabilityRules.SupportsCurrentSpectatorTargetSync(capability);
    }

    /// <summary>
    /// Gets whether a peer capability was relayed by the host rather than handshaken directly.
    /// </summary>
    public bool IsRelayedPeer(ulong clientId)
    {
        return _relayedPeerIds.Contains(clientId);
    }

    /// <summary>
    /// Gets all known remote peer ids.
    /// </summary>
    public List<ulong> GetRemotePeerIds(ulong localClientId)
    {
        List<ulong> peerIds = new List<ulong>();
        foreach (ulong clientId in _capabilities.Keys)
        {
            if (clientId != localClientId)
            {
                peerIds.Add(clientId);
            }
        }

        return peerIds;
    }

    /// <summary>
    /// Removes one peer capability entry.
    /// </summary>
    public void Remove(ulong clientId)
    {
        _capabilities.Remove(clientId);
        _relayedPeerIds.Remove(clientId);
    }

    /// <summary>
    /// Removes all remote capability entries while keeping no local assumptions.
    /// </summary>
    public void Clear()
    {
        _capabilities.Clear();
        _relayedPeerIds.Clear();
    }

    private static ModPeerCapability NormalizeRemoteCapability(ModPeerCapability capability)
    {
        bool handshakeComplete = capability.ProtocolVersion == ModNetworkConstants.ProtocolVersion
            && capability.SupportsCapabilityHandshake;

        return new ModPeerCapability(
            capability.ClientId,
            capability.ProtocolVersion,
            capability.SupportsCapabilityHandshake,
            capability.SupportsSpectatorTargetSync,
            handshakeComplete,
            capability.LastSeenTicks,
            capability.SupportsVoiceActivitySync,
            capability.SupportsSpectatorVoiceToTarget);
    }

    private List<ulong> GetPeerIds(
        ulong localClientId,
        bool includeRelayed,
        System.Func<ModPeerCapability, bool> predicate)
    {
        List<ulong> peerIds = new List<ulong>();
        foreach (KeyValuePair<ulong, ModPeerCapability> pair in _capabilities)
        {
            if (pair.Key == localClientId)
            {
                continue;
            }

            if (!includeRelayed && _relayedPeerIds.Contains(pair.Key))
            {
                continue;
            }

            if (predicate(pair.Value))
            {
                peerIds.Add(pair.Key);
            }
        }

        return peerIds;
    }
}
