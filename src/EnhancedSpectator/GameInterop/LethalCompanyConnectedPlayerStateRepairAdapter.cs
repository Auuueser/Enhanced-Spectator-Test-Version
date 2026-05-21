using System;
using System.Collections.Generic;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Networking;
using GameNetcodeStuff;
using Unity.Netcode;
using UnityEngine;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Repairs confirmed Lethal Company connected-player flags using vanilla ClientPlayerList as the source of truth.
/// </summary>
public sealed class LethalCompanyConnectedPlayerStateRepairAdapter : IConnectedPlayerStateRepairAdapter
{
    /// <inheritdoc />
    public int RepairConnectedPlayerState(
        IReadOnlyList<PeerIdentityState> peerIdentities,
        IReadOnlyList<SpectatorTargetState> remoteSpectatorTargets,
        bool updatePlayerNames,
        bool updateQuickMenu,
        bool debug,
        out string summary)
    {
        summary = string.Empty;
        if (peerIdentities == null || peerIdentities.Count == 0)
        {
            return 0;
        }

        StartOfRound? round = StartOfRound.Instance;
        if (round == null || round.ClientPlayerList == null || round.allPlayerScripts == null)
        {
            summary = "StartOfRound player state unavailable";
            return 0;
        }

        NetworkManager? networkManager = NetworkManager.Singleton;
        if (networkManager == null)
        {
            summary = "NetworkManager unavailable";
            return 0;
        }

        ulong localClientId = networkManager.LocalClientId;
        QuickMenuManager? quickMenuManager = UnityEngine.Object.FindObjectOfType<QuickMenuManager>();
        int repairs = 0;
        for (int index = 0; index < peerIdentities.Count; index++)
        {
            PeerIdentityState identity = peerIdentities[index];
            if (identity.ClientId == localClientId)
            {
                continue;
            }

            if (!IsClientCurrentlyConnected(networkManager, identity.ClientId))
            {
                if (debug)
                {
                    ModLog.Debug($"Skipping connected-player repair for client={identity.ClientId}; client is not currently connected.");
                }

                continue;
            }

            if (!round.ClientPlayerList.TryGetValue(identity.ClientId, out int slot))
            {
                continue;
            }

            if (slot < 0 || slot >= round.allPlayerScripts.Length)
            {
                continue;
            }

            if (identity.PlayerSlotId != (ulong)slot)
            {
                if (debug)
                {
                    ModLog.Debug(
                        $"Skipping connected-player repair for client={identity.ClientId}; identity slot={identity.PlayerSlotId}, vanilla slot={slot}.");
                }

                continue;
            }

            PlayerControllerB player = round.allPlayerScripts[slot];
            if (player == null)
            {
                continue;
            }

            bool hasRemoteSpectatorTargetState = TryGetRemoteSpectatorTarget(
                remoteSpectatorTargets,
                identity.ClientId,
                out SpectatorTargetState? remoteSpectatorTargetState);
            bool remotePeerIsSpectating = hasRemoteSpectatorTargetState
                && remoteSpectatorTargetState != null
                && remoteSpectatorTargetState.IsSpectating;
            bool repaired = false;
            if (remotePeerIsSpectating)
            {
                // A peer that reports an active spectator target may have died before this client joined.
                // Vanilla join sync does not reliably replay that death state to late joiners.
                if (!player.isPlayerDead)
                {
                    player.isPlayerDead = true;
                    repaired = true;
                }

                if (player.isPlayerControlled)
                {
                    player.isPlayerControlled = false;
                    repaired = true;
                }
            }
            else if (hasRemoteSpectatorTargetState && player.isPlayerDead)
            {
                // A known non-spectating target state means the remote peer has left spectator mode.
                // This clears mod-repaired death flags after revive without guessing from stale absence.
                player.isPlayerDead = false;
                if (!player.isPlayerControlled)
                {
                    player.isPlayerControlled = true;
                }

                repaired = true;
            }
            else if (!player.isPlayerControlled && !player.isPlayerDead)
            {
                player.isPlayerControlled = true;
                repaired = true;
            }

            if (player.disconnectedMidGame)
            {
                player.disconnectedMidGame = false;
                repaired = true;
            }

            if (!remotePeerIsSpectating && RepairAliveConnectedPlayerVisualState(round, slot, player))
            {
                repaired = true;
            }

            if (player.actualClientId != identity.ClientId)
            {
                player.actualClientId = identity.ClientId;
                repaired = true;
            }

            if (player.playerClientId != (ulong)slot)
            {
                player.playerClientId = (ulong)slot;
                repaired = true;
            }

            string displayName = string.Empty;
            if (updatePlayerNames
                && PlayerDisplayNameRules.TryNormalize(identity.DisplayName, out displayName)
                && !StringComparer.Ordinal.Equals(player.playerUsername, displayName))
            {
                player.playerUsername = displayName;
                repaired = true;
            }

            if (updateQuickMenu && quickMenuManager != null)
            {
                string quickMenuName = !string.IsNullOrWhiteSpace(displayName)
                    ? displayName
                    : player.playerUsername;
                quickMenuManager.AddUserToPlayerList(player.playerSteamId, quickMenuName, slot);
            }

            if (!repaired)
            {
                continue;
            }

            repairs++;
            if (debug)
            {
                ModLog.Debug(
                    $"Repaired vanilla connected player slot: client={identity.ClientId}, slot={slot}, name={player.playerUsername}, controlled={player.isPlayerControlled}, dead={player.isPlayerDead}, disconnected={player.disconnectedMidGame}, setPositionOfDeadPlayer={player.setPositionOfDeadPlayer}, modelHidden={IsPlayerModelHidden(player)}, targetStateKnown={hasRemoteSpectatorTargetState}, spectator={remotePeerIsSpectating}.");
            }
        }

        if (repairs > 0)
        {
            summary = $"repaired {repairs} connected player slot(s)";
        }

        return repairs;
    }

    private static bool TryGetRemoteSpectatorTarget(
        IReadOnlyList<SpectatorTargetState>? remoteSpectatorTargets,
        ulong clientId,
        out SpectatorTargetState? spectatorTargetState)
    {
        spectatorTargetState = default;
        if (remoteSpectatorTargets == null)
        {
            return false;
        }

        for (int index = 0; index < remoteSpectatorTargets.Count; index++)
        {
            SpectatorTargetState state = remoteSpectatorTargets[index];
            if (state.LocalClientId == clientId)
            {
                spectatorTargetState = state;
                return true;
            }
        }

        return false;
    }

    private static bool RepairAliveConnectedPlayerVisualState(StartOfRound round, int slot, PlayerControllerB player)
    {
        bool repaired = false;
        if (player.setPositionOfDeadPlayer)
        {
            player.setPositionOfDeadPlayer = false;
            repaired = true;
        }

        if (IsPlayerModelHidden(player)
            && round.allPlayerObjects != null
            && slot >= 0
            && slot < round.allPlayerObjects.Length
            && round.allPlayerObjects[slot] != null)
        {
            // Death and disconnect paths can disable the live SkinnedMeshRenderers, while
            // vanilla reconnect only marks the slot controlled again. Re-enable the same
            // live model renderers that vanilla ReviveDeadPlayers enables for alive players.
            player.DisablePlayerModel(round.allPlayerObjects[slot], enable: true, disableLocalArms: true);
            repaired = true;
        }

        if (repaired && player.playerBodyAnimator != null)
        {
            player.playerBodyAnimator.SetBool("Limp", false);
        }

        return repaired;
    }

    private static bool IsPlayerModelHidden(PlayerControllerB player)
    {
        return (player.thisPlayerModel != null && !player.thisPlayerModel.enabled)
            || (player.thisPlayerModelLOD1 != null && !player.thisPlayerModelLOD1.enabled)
            || (player.thisPlayerModelLOD2 != null && !player.thisPlayerModelLOD2.enabled);
    }

    private static bool IsClientCurrentlyConnected(NetworkManager networkManager, ulong clientId)
    {
        if (clientId == networkManager.LocalClientId)
        {
            return true;
        }

        IReadOnlyList<ulong> connectedClientIds = networkManager.ConnectedClientsIds;
        for (int index = 0; index < connectedClientIds.Count; index++)
        {
            if (connectedClientIds[index] == clientId)
            {
                return true;
            }
        }

        return false;
    }
}
