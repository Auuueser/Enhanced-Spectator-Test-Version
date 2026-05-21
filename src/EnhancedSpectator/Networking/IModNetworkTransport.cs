using System;
using System.Collections.Generic;
using EnhancedSpectator.Features.VoiceActivity;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Sends and receives Enhanced Spectator mod-owned network messages.
/// </summary>
public interface IModNetworkTransport : IDisposable
{
    /// <summary>
    /// Gets whether named message handlers are currently registered.
    /// </summary>
    bool IsRegistered { get; }

    /// <summary>
    /// Gets whether Netcode custom messaging is currently available.
    /// </summary>
    bool IsNetworkAvailable { get; }

    /// <summary>
    /// Gets whether the local process is the host.
    /// </summary>
    bool IsHost { get; }

    /// <summary>
    /// Gets the local Netcode client id.
    /// </summary>
    ulong LocalClientId { get; }

    /// <summary>
    /// Gets the Netcode server client id.
    /// </summary>
    ulong ServerClientId { get; }

    /// <summary>
    /// Gets whether a peer is currently connected from the transport perspective.
    /// </summary>
    bool IsPeerConnected(ulong clientId);

    /// <summary>
    /// Registers named message handlers.
    /// </summary>
    bool TryRegister(
        Action<ulong, ModPeerCapability> capabilityReceived,
        Action<ulong, SpectatorTargetState> spectatorTargetReceived,
        Action<ulong, SpectatorPoseState> spectatorPoseReceived,
        Action<ulong, PeerIdentityState> peerIdentityReceived,
        Action<ulong, VoiceActivityState> voiceActivityReceived,
        out string reason);

    /// <summary>
    /// Unregisters named message handlers.
    /// </summary>
    void Unregister();

    /// <summary>
    /// Sends local capability either to default peers or to explicit recipients.
    /// </summary>
    bool SendCapability(ModPeerCapability capability, IEnumerable<ulong>? recipients, out string reason);

    /// <summary>
    /// Sends a spectator target state to explicit recipients.
    /// </summary>
    bool SendSpectatorTarget(SpectatorTargetSyncMessage message, IEnumerable<ulong> recipients, out string reason);

    /// <summary>
    /// Sends a spectator camera pose to explicit recipients.
    /// </summary>
    bool SendSpectatorPose(SpectatorPoseSyncMessage message, IEnumerable<ulong> recipients, out string reason);

    /// <summary>
    /// Sends a peer identity state to explicit recipients or the default capability path.
    /// </summary>
    bool SendPeerIdentity(PeerIdentityState state, IEnumerable<ulong>? recipients, out string reason);

    /// <summary>
    /// Sends a visual-only voice activity state to explicit recipients.
    /// </summary>
    bool SendVoiceActivity(VoiceActivitySyncMessage message, IEnumerable<ulong> recipients, out string reason);
}
