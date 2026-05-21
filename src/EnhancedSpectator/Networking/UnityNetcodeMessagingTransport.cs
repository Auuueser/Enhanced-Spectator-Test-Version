using System;
using System.Collections.Generic;
using EnhancedSpectator.Features.VoiceActivity;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Runtime;
using Unity.Collections;
using Unity.Netcode;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Unity Netcode custom messaging transport for Enhanced Spectator messages.
/// </summary>
public sealed class UnityNetcodeMessagingTransport : IModNetworkTransport
{
    private readonly Func<bool> _debugEnabled;
    private NetworkManager? _networkManager;
    private CustomMessagingManager? _customMessagingManager;
    private Action<ulong, ModPeerCapability>? _capabilityReceived;
    private Action<ulong, SpectatorTargetState>? _spectatorTargetReceived;
    private Action<ulong, SpectatorPoseState>? _spectatorPoseReceived;
    private Action<ulong, PeerIdentityState>? _peerIdentityReceived;
    private Action<ulong, VoiceActivityState>? _voiceActivityReceived;
    private bool _registered;

    /// <summary>
    /// Creates a Unity Netcode custom messaging transport.
    /// </summary>
    public UnityNetcodeMessagingTransport(Func<bool> debugEnabled)
    {
        _debugEnabled = debugEnabled ?? throw new ArgumentNullException(nameof(debugEnabled));
    }

    /// <inheritdoc />
    public bool IsRegistered => _registered;

    /// <inheritdoc />
    public bool IsNetworkAvailable
    {
        get
        {
            NetworkManager? manager = NetworkManager.Singleton;
            return RuntimeConnectionState.CanUseModNetworking(out _)
                && manager != null
                && manager.CustomMessagingManager != null
                && (!_registered || ReferenceEquals(_networkManager, manager))
                && (!_registered || ReferenceEquals(_customMessagingManager, manager.CustomMessagingManager));
        }
    }

    /// <inheritdoc />
    public bool IsHost => NetworkManager.Singleton != null && NetworkManager.Singleton.IsHost;

    /// <inheritdoc />
    public ulong LocalClientId => NetworkManager.Singleton != null ? NetworkManager.Singleton.LocalClientId : 0UL;

    /// <inheritdoc />
    public ulong ServerClientId => NetworkManager.ServerClientId;

    /// <inheritdoc />
    public bool IsPeerConnected(ulong clientId)
    {
        NetworkManager? manager = _networkManager ?? NetworkManager.Singleton;
        if (!RuntimeConnectionState.CanUseModNetworking(out _) || manager == null)
        {
            return false;
        }

        if (_registered && !ReferenceEquals(manager, NetworkManager.Singleton))
        {
            return false;
        }

        if (clientId == manager.LocalClientId)
        {
            return true;
        }

        if (!manager.IsHost)
        {
            return clientId == NetworkManager.ServerClientId;
        }

        foreach (ulong connectedClientId in manager.ConnectedClientsIds)
        {
            if (connectedClientId == clientId)
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public bool TryRegister(
        Action<ulong, ModPeerCapability> capabilityReceived,
        Action<ulong, SpectatorTargetState> spectatorTargetReceived,
        Action<ulong, SpectatorPoseState> spectatorPoseReceived,
        Action<ulong, PeerIdentityState> peerIdentityReceived,
        Action<ulong, VoiceActivityState> voiceActivityReceived,
        out string reason)
    {
        reason = string.Empty;
        if (_registered)
        {
            return true;
        }

        if (!RuntimeConnectionState.CanUseModNetworking(out reason))
        {
            return false;
        }

        NetworkManager? manager = NetworkManager.Singleton;
        if (manager == null)
        {
            reason = "NetworkManager unavailable";
            return false;
        }

        if (!manager.IsClient && !manager.IsHost)
        {
            reason = "not connected as client or host";
            return false;
        }

        CustomMessagingManager? customMessagingManager = manager.CustomMessagingManager;
        if (customMessagingManager == null)
        {
            reason = "CustomMessagingManager unavailable";
            return false;
        }

        _networkManager = manager;
        _customMessagingManager = customMessagingManager;
        _capabilityReceived = capabilityReceived ?? throw new ArgumentNullException(nameof(capabilityReceived));
        _spectatorTargetReceived = spectatorTargetReceived ?? throw new ArgumentNullException(nameof(spectatorTargetReceived));
        _spectatorPoseReceived = spectatorPoseReceived ?? throw new ArgumentNullException(nameof(spectatorPoseReceived));
        _peerIdentityReceived = peerIdentityReceived ?? throw new ArgumentNullException(nameof(peerIdentityReceived));
        _voiceActivityReceived = voiceActivityReceived ?? throw new ArgumentNullException(nameof(voiceActivityReceived));

        try
        {
            customMessagingManager.RegisterNamedMessageHandler(
                ModNetworkConstants.CapabilityMessageName,
                HandleCapabilityMessage);
            customMessagingManager.RegisterNamedMessageHandler(
                ModNetworkConstants.SpectatorTargetMessageName,
                HandleSpectatorTargetMessage);
            customMessagingManager.RegisterNamedMessageHandler(
                ModNetworkConstants.SpectatorPoseMessageName,
                HandleSpectatorPoseMessage);
            customMessagingManager.RegisterNamedMessageHandler(
                ModNetworkConstants.PeerIdentityMessageName,
                HandlePeerIdentityMessage);
            customMessagingManager.RegisterNamedMessageHandler(
                ModNetworkConstants.VoiceActivityMessageName,
                HandleVoiceActivityMessage);
            _registered = true;
            Debug("Registered Enhanced Spectator named message handlers.");
            return true;
        }
        catch (Exception ex)
        {
            reason = $"handler registration failed: {ex.GetType().Name}";
            Unregister();
            return false;
        }
    }

    /// <inheritdoc />
    public void Unregister()
    {
        CustomMessagingManager? customMessagingManager = _customMessagingManager;
        if (customMessagingManager != null)
        {
            try
            {
                customMessagingManager.UnregisterNamedMessageHandler(ModNetworkConstants.CapabilityMessageName);
                customMessagingManager.UnregisterNamedMessageHandler(ModNetworkConstants.SpectatorTargetMessageName);
                customMessagingManager.UnregisterNamedMessageHandler(ModNetworkConstants.SpectatorPoseMessageName);
                customMessagingManager.UnregisterNamedMessageHandler(ModNetworkConstants.PeerIdentityMessageName);
                customMessagingManager.UnregisterNamedMessageHandler(ModNetworkConstants.VoiceActivityMessageName);
                Debug("Unregistered Enhanced Spectator named message handlers.");
            }
            catch (Exception ex)
            {
                Debug($"Named message handler unregister failed: {ex.GetType().Name}.");
            }
        }

        _registered = false;
        _networkManager = null;
        _customMessagingManager = null;
        _capabilityReceived = null;
        _spectatorTargetReceived = null;
        _spectatorPoseReceived = null;
        _peerIdentityReceived = null;
        _voiceActivityReceived = null;
    }

    /// <inheritdoc />
    public bool SendCapability(ModPeerCapability capability, IEnumerable<ulong>? recipients, out string reason)
    {
        reason = string.Empty;
        if (!TryGetMessagingManager(out NetworkManager manager, out CustomMessagingManager customMessagingManager, out reason))
        {
            return false;
        }

        FastBufferWriter writer = new FastBufferWriter(ModNetworkSerializer.CapabilityMessageSize, Allocator.Temp);
        try
        {
            ModNetworkSerializer.WriteCapability(ref writer, capability);

            if (recipients == null)
            {
                if (manager.IsHost)
                {
                    customMessagingManager.SendNamedMessageToAll(
                        ModNetworkConstants.CapabilityMessageName,
                        writer,
                        NetworkDelivery.ReliableSequenced);
                }
                else
                {
                    customMessagingManager.SendNamedMessage(
                        ModNetworkConstants.CapabilityMessageName,
                        NetworkManager.ServerClientId,
                        writer,
                        NetworkDelivery.ReliableSequenced);
                }

                return true;
            }

            bool sentAny = false;
            foreach (ulong clientId in recipients)
            {
                if (clientId == manager.LocalClientId)
                {
                    continue;
                }

                customMessagingManager.SendNamedMessage(
                    ModNetworkConstants.CapabilityMessageName,
                    clientId,
                    writer,
                    NetworkDelivery.ReliableSequenced);
                sentAny = true;
            }

            if (!sentAny)
            {
                reason = "no capability recipients";
            }

            return sentAny;
        }
        catch (Exception ex)
        {
            reason = $"capability send failed: {ex.GetType().Name}";
            return false;
        }
        finally
        {
            writer.Dispose();
        }
    }

    /// <inheritdoc />
    public bool SendSpectatorTarget(SpectatorTargetSyncMessage message, IEnumerable<ulong> recipients, out string reason)
    {
        reason = string.Empty;
        if (!TryGetMessagingManager(out NetworkManager manager, out CustomMessagingManager customMessagingManager, out reason))
        {
            return false;
        }

        FastBufferWriter writer = new FastBufferWriter(ModNetworkSerializer.SpectatorTargetMessageSize, Allocator.Temp);
        try
        {
            ModNetworkSerializer.WriteSpectatorTarget(ref writer, message);

            bool sentAny = false;
            foreach (ulong clientId in recipients)
            {
                if (clientId == manager.LocalClientId)
                {
                    continue;
                }

                customMessagingManager.SendNamedMessage(
                    ModNetworkConstants.SpectatorTargetMessageName,
                    clientId,
                    writer,
                    NetworkDelivery.ReliableSequenced);
                sentAny = true;
            }

            if (!sentAny)
            {
                reason = "no spectator target recipients";
            }

            return sentAny;
        }
        catch (Exception ex)
        {
            reason = $"spectator target send failed: {ex.GetType().Name}";
            return false;
        }
        finally
        {
            writer.Dispose();
        }
    }

    /// <inheritdoc />
    public bool SendSpectatorPose(SpectatorPoseSyncMessage message, IEnumerable<ulong> recipients, out string reason)
    {
        reason = string.Empty;
        if (!TryGetMessagingManager(out NetworkManager manager, out CustomMessagingManager customMessagingManager, out reason))
        {
            return false;
        }

        FastBufferWriter writer = new FastBufferWriter(ModNetworkSerializer.SpectatorPoseMessageSize, Allocator.Temp);
        try
        {
            ModNetworkSerializer.WriteSpectatorPose(ref writer, message);

            bool sentAny = false;
            foreach (ulong clientId in recipients)
            {
                if (clientId == manager.LocalClientId)
                {
                    continue;
                }

                customMessagingManager.SendNamedMessage(
                    ModNetworkConstants.SpectatorPoseMessageName,
                    clientId,
                    writer,
                    NetworkDelivery.UnreliableSequenced);
                sentAny = true;
            }

            if (!sentAny)
            {
                reason = "no spectator pose recipients";
            }

            return sentAny;
        }
        catch (Exception ex)
        {
            reason = $"spectator pose send failed: {ex.GetType().Name}";
            return false;
        }
        finally
        {
            writer.Dispose();
        }
    }

    /// <inheritdoc />
    public bool SendPeerIdentity(PeerIdentityState state, IEnumerable<ulong>? recipients, out string reason)
    {
        reason = string.Empty;
        if (!TryGetMessagingManager(out NetworkManager manager, out CustomMessagingManager customMessagingManager, out reason))
        {
            return false;
        }

        FastBufferWriter writer = new FastBufferWriter(ModNetworkSerializer.PeerIdentityMessageSize, Allocator.Temp);
        try
        {
            ModNetworkSerializer.WritePeerIdentity(ref writer, state);

            if (recipients == null)
            {
                if (manager.IsHost)
                {
                    customMessagingManager.SendNamedMessageToAll(
                        ModNetworkConstants.PeerIdentityMessageName,
                        writer,
                        NetworkDelivery.ReliableSequenced);
                }
                else
                {
                    customMessagingManager.SendNamedMessage(
                        ModNetworkConstants.PeerIdentityMessageName,
                        NetworkManager.ServerClientId,
                        writer,
                        NetworkDelivery.ReliableSequenced);
                }

                return true;
            }

            bool sentAny = false;
            foreach (ulong clientId in recipients)
            {
                if (clientId == manager.LocalClientId)
                {
                    continue;
                }

                customMessagingManager.SendNamedMessage(
                    ModNetworkConstants.PeerIdentityMessageName,
                    clientId,
                    writer,
                    NetworkDelivery.ReliableSequenced);
                sentAny = true;
            }

            if (!sentAny)
            {
                reason = "no peer identity recipients";
            }

            return sentAny;
        }
        catch (Exception ex)
        {
            reason = $"peer identity send failed: {ex.GetType().Name}";
            return false;
        }
        finally
        {
            writer.Dispose();
        }
    }

    /// <inheritdoc />
    public bool SendVoiceActivity(VoiceActivitySyncMessage message, IEnumerable<ulong> recipients, out string reason)
    {
        reason = string.Empty;
        if (!TryGetMessagingManager(out NetworkManager manager, out CustomMessagingManager customMessagingManager, out reason))
        {
            return false;
        }

        FastBufferWriter writer = new FastBufferWriter(ModNetworkSerializer.VoiceActivityMessageSize, Allocator.Temp);
        try
        {
            ModNetworkSerializer.WriteVoiceActivity(ref writer, message);

            bool sentAny = false;
            foreach (ulong clientId in recipients)
            {
                if (clientId == manager.LocalClientId)
                {
                    continue;
                }

                customMessagingManager.SendNamedMessage(
                    ModNetworkConstants.VoiceActivityMessageName,
                    clientId,
                    writer,
                    NetworkDelivery.UnreliableSequenced);
                sentAny = true;
            }

            if (!sentAny)
            {
                reason = "no voice activity recipients";
            }

            return sentAny;
        }
        catch (Exception ex)
        {
            reason = $"voice activity send failed: {ex.GetType().Name}";
            return false;
        }
        finally
        {
            writer.Dispose();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Unregister();
    }

    private bool TryGetMessagingManager(
        out NetworkManager manager,
        out CustomMessagingManager customMessagingManager,
        out string reason)
    {
        manager = null!;
        customMessagingManager = null!;
        reason = string.Empty;

        if (!RuntimeConnectionState.CanUseModNetworking(out reason))
        {
            return false;
        }

        if (!_registered || _networkManager == null || _customMessagingManager == null)
        {
            reason = "transport is not registered";
            return false;
        }

        if (!ReferenceEquals(_networkManager, NetworkManager.Singleton))
        {
            reason = "NetworkManager instance changed";
            return false;
        }

        if (!ReferenceEquals(_customMessagingManager, _networkManager.CustomMessagingManager))
        {
            reason = "CustomMessagingManager instance changed";
            return false;
        }

        if (!_networkManager.IsClient && !_networkManager.IsHost)
        {
            reason = "network is no longer connected";
            return false;
        }

        manager = _networkManager;
        customMessagingManager = _customMessagingManager;
        return true;
    }

    private void HandleCapabilityMessage(ulong senderClientId, FastBufferReader messagePayload)
    {
        try
        {
            if (!RuntimeConnectionState.CanUseModNetworking(out string lifecycleReason))
            {
                Debug($"Dropped capability message from {senderClientId}: {lifecycleReason}.");
                return;
            }

            FastBufferReader reader = messagePayload;
            if (!ModNetworkSerializer.TryReadCapability(ref reader, out ModPeerCapability capability, out string reason))
            {
                Debug($"Dropped capability message from {senderClientId}: {reason}.");
                return;
            }

            _capabilityReceived?.Invoke(senderClientId, capability);
        }
        catch (Exception ex)
        {
            Debug($"Capability handler failed for sender {senderClientId}: {ex.GetType().Name}.");
        }
    }

    private void HandleSpectatorTargetMessage(ulong senderClientId, FastBufferReader messagePayload)
    {
        try
        {
            if (!RuntimeConnectionState.CanUseModNetworking(out string lifecycleReason))
            {
                Debug($"Dropped spectator target message from {senderClientId}: {lifecycleReason}.");
                return;
            }

            FastBufferReader reader = messagePayload;
            if (!ModNetworkSerializer.TryReadSpectatorTarget(ref reader, out SpectatorTargetState state, out string reason))
            {
                Debug($"Dropped spectator target message from {senderClientId}: {reason}.");
                return;
            }

            _spectatorTargetReceived?.Invoke(senderClientId, state);
        }
        catch (Exception ex)
        {
            Debug($"Spectator target handler failed for sender {senderClientId}: {ex.GetType().Name}.");
        }
    }

    private void HandleSpectatorPoseMessage(ulong senderClientId, FastBufferReader messagePayload)
    {
        try
        {
            if (!RuntimeConnectionState.CanUseModNetworking(out string lifecycleReason))
            {
                Debug($"Dropped spectator pose message from {senderClientId}: {lifecycleReason}.");
                return;
            }

            FastBufferReader reader = messagePayload;
            if (!ModNetworkSerializer.TryReadSpectatorPose(ref reader, out SpectatorPoseState state, out string reason))
            {
                Debug($"Dropped spectator pose message from {senderClientId}: {reason}.");
                return;
            }

            _spectatorPoseReceived?.Invoke(senderClientId, state);
        }
        catch (Exception ex)
        {
            Debug($"Spectator pose handler failed for sender {senderClientId}: {ex.GetType().Name}.");
        }
    }

    private void HandlePeerIdentityMessage(ulong senderClientId, FastBufferReader messagePayload)
    {
        try
        {
            if (!RuntimeConnectionState.CanUseModNetworking(out string lifecycleReason))
            {
                Debug($"Dropped peer identity message from {senderClientId}: {lifecycleReason}.");
                return;
            }

            FastBufferReader reader = messagePayload;
            if (!ModNetworkSerializer.TryReadPeerIdentity(ref reader, out PeerIdentityState state, out string reason))
            {
                Debug($"Dropped peer identity message from {senderClientId}: {reason}.");
                return;
            }

            _peerIdentityReceived?.Invoke(senderClientId, state);
        }
        catch (Exception ex)
        {
            Debug($"Peer identity handler failed for sender {senderClientId}: {ex.GetType().Name}.");
        }
    }

    private void HandleVoiceActivityMessage(ulong senderClientId, FastBufferReader messagePayload)
    {
        try
        {
            if (!RuntimeConnectionState.CanUseModNetworking(out string lifecycleReason))
            {
                Debug($"Dropped voice activity message from {senderClientId}: {lifecycleReason}.");
                return;
            }

            FastBufferReader reader = messagePayload;
            if (!ModNetworkSerializer.TryReadVoiceActivity(ref reader, out VoiceActivityState state, out string reason))
            {
                Debug($"Dropped voice activity message from {senderClientId}: {reason}.");
                return;
            }

            _voiceActivityReceived?.Invoke(senderClientId, state);
        }
        catch (Exception ex)
        {
            Debug($"Voice activity handler failed for sender {senderClientId}: {ex.GetType().Name}.");
        }
    }

    private void Debug(string message)
    {
        if (_debugEnabled())
        {
            ModLog.Debug(message);
        }
    }
}
