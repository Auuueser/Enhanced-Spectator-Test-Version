namespace EnhancedSpectator.Networking;

/// <summary>
/// Provides the local peer identity used by mod-owned name tag sync.
/// </summary>
public interface IPeerIdentityStateProvider
{
    /// <summary>
    /// Attempts to get the current local peer identity.
    /// </summary>
    bool TryGetLocalPeerIdentity(out PeerIdentityState state);
}
