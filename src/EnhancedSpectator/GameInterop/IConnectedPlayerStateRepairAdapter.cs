using System.Collections.Generic;
using EnhancedSpectator.Networking;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Repairs vanilla local connected-player state when join-time ownership synchronization arrives late.
/// </summary>
public interface IConnectedPlayerStateRepairAdapter
{
    /// <summary>
    /// Attempts to reconcile connected player slots against mod-owned peer identity state.
    /// </summary>
    int RepairConnectedPlayerState(
        IReadOnlyList<PeerIdentityState> peerIdentities,
        IReadOnlyList<SpectatorTargetState> remoteSpectatorTargets,
        bool updatePlayerNames,
        bool updateQuickMenu,
        bool debug,
        out string summary);
}
