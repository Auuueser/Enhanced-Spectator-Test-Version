using System;
using System.Collections.Generic;
using Dissonance;
using GameNetcodeStuff;
using EnhancedSpectator.Features.VoiceDiagnostics;
using UnityEngine;
using UnityEngine.Audio;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Reads local vanilla and Dissonance voice diagnostics through confirmed members.
/// </summary>
public sealed class LethalCompanyVoiceDiagnosticsAdapter : IGameVoiceDiagnosticsAdapter
{
    /// <inheritdoc />
    public bool TryGetVoiceDiagnosticsSnapshot(
        bool includeLocalPlayer,
        bool includeRemotePlayers,
        bool includeAudioSourceDiagnostics,
        bool includeWalkieDiagnostics,
        out VoiceDiagnosticsSnapshot snapshot)
    {
        StartOfRound? round = StartOfRound.Instance;
        if (round == null)
        {
            snapshot = EmptySnapshot(hasRound: false);
            return false;
        }

        PlayerControllerB? localPlayer = round.localPlayerController;
        DissonanceComms? voiceChatModule = round.voiceChatModule;
        bool hasLocalPlayer = localPlayer != null;
        ulong localClientId = 0UL;
        ulong localSlotId = 0UL;
        bool isLocalPlayerDead = false;
        bool hasBegunSpectating = false;
        PlayerControllerB? spectatedTarget = null;
        if (localPlayer != null)
        {
            localClientId = localPlayer.actualClientId;
            localSlotId = localPlayer.playerClientId;
            isLocalPlayerDead = localPlayer.isPlayerDead;
            hasBegunSpectating = localPlayer.hasBegunSpectating;
            spectatedTarget = localPlayer.spectatedPlayerScript;
        }

        List<PlayerVoiceDiagnosticsSnapshot> players = new List<PlayerVoiceDiagnosticsSnapshot>();
        if (round.allPlayerScripts != null)
        {
            foreach (PlayerControllerB player in round.allPlayerScripts)
            {
                if (player == null)
                {
                    continue;
                }

                bool isLocal = hasLocalPlayer && player == localPlayer;
                if ((isLocal && !includeLocalPlayer) || (!isLocal && !includeRemotePlayers))
                {
                    continue;
                }

                players.Add(BuildPlayerSnapshot(
                    player,
                    isLocal,
                    spectatedTarget != null && player == spectatedTarget,
                    includeAudioSourceDiagnostics,
                    includeWalkieDiagnostics));
            }
        }

        snapshot = new VoiceDiagnosticsSnapshot(
            hasRound: true,
            hasLocalPlayer,
            hasVoiceChatModule: voiceChatModule != null,
            voiceChatModule != null ? SafeString(voiceChatModule.LocalPlayerName) : string.Empty,
            voiceChatModule != null && voiceChatModule.IsMuted,
            voiceChatModule != null && voiceChatModule.IsDeafened,
            localClientId,
            localSlotId,
            isLocalPlayerDead,
            hasBegunSpectating,
            spectatedTarget != null ? spectatedTarget.actualClientId : (ulong?)null,
            spectatedTarget != null ? spectatedTarget.playerClientId : (ulong?)null,
            includeAudioSourceDiagnostics,
            includeWalkieDiagnostics,
            players.AsReadOnly(),
            DateTime.UtcNow.Ticks);
        return true;
    }

    private static VoiceDiagnosticsSnapshot EmptySnapshot(bool hasRound)
    {
        return new VoiceDiagnosticsSnapshot(
            hasRound,
            hasLocalPlayer: false,
            hasVoiceChatModule: false,
            localDissonancePlayerName: string.Empty,
            voiceChatMuted: false,
            voiceChatDeafened: false,
            localClientId: 0,
            localPlayerSlotId: 0,
            isLocalPlayerDead: false,
            isLocalPlayerSpectating: false,
            spectatedTargetClientId: null,
            spectatedTargetPlayerSlotId: null,
            includeAudioSourceDiagnostics: false,
            includeWalkieDiagnostics: false,
            players: Array.Empty<PlayerVoiceDiagnosticsSnapshot>(),
            timestampTicks: DateTime.UtcNow.Ticks);
    }

    private static PlayerVoiceDiagnosticsSnapshot BuildPlayerSnapshot(
        PlayerControllerB player,
        bool isLocal,
        bool isSpectatedTarget,
        bool includeAudioSourceDiagnostics,
        bool includeWalkieDiagnostics)
    {
        VoicePlayerState? voiceState = player.voicePlayerState;
        AudioSource? voiceAudio = includeAudioSourceDiagnostics ? player.currentVoiceChatAudioSource : null;
        PlayerVoiceIngameSettings? ingameSettings = includeAudioSourceDiagnostics
            ? player.currentVoiceChatIngameSettings
            : null;
        AudioMixerGroup? mixerGroup = voiceAudio != null ? voiceAudio.outputAudioMixerGroup : null;

        return new PlayerVoiceDiagnosticsSnapshot(
            player.playerClientId,
            player.actualClientId,
            SafeString(player.playerUsername),
            voiceState != null ? SafeString(voiceState.Name) : string.Empty,
            isLocal,
            isSpectatedTarget,
            player.isPlayerControlled,
            player.isPlayerDead,
            voiceState != null,
            voiceState != null && voiceState.IsConnected,
            voiceState != null && voiceState.IsSpeaking,
            voiceState != null && voiceState.IsLocallyMuted,
            voiceState != null ? voiceState.Amplitude : 0f,
            voiceState != null ? voiceState.Volume : 0f,
            voiceAudio != null,
            voiceAudio != null && voiceAudio.isPlaying,
            voiceAudio != null && voiceAudio.mute,
            voiceAudio != null ? voiceAudio.volume : 0f,
            voiceAudio != null ? voiceAudio.spatialBlend : 0f,
            mixerGroup != null ? SafeString(mixerGroup.name) : string.Empty,
            ingameSettings != null,
            ingameSettings != null && ingameSettings.set2D,
            ingameSettings != null && ingameSettings._playbackComponent != null
                ? SafeString(ingameSettings._playbackComponent.PlayerName)
                : string.Empty,
            includeWalkieDiagnostics && player.holdingWalkieTalkie,
            includeWalkieDiagnostics && player.speakingToWalkieTalkie,
            includeWalkieDiagnostics && player.voiceMuffledByEnemy);
    }

    private static string SafeString(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
    }
}
