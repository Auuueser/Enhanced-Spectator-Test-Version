using System;
using Dissonance;
using EnhancedSpectator.Features.VoiceActivity;
using GameNetcodeStuff;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Lethal Company voice activity adapter using confirmed Dissonance player state.
/// </summary>
public sealed class LethalCompanyVoiceActivityAdapter : IGameVoiceActivityAdapter
{
    /// <inheritdoc />
    public bool TryGetVoiceActivity(ulong clientId, ulong slotId, out VoiceActivityState state)
    {
        PlayerControllerB? player = FindPlayer(clientId, slotId);
        if (player == null)
        {
            state = VoiceActivityState.NoData;
            return false;
        }

        if (IsLocalPlayer(player) && TryGetLocalVoiceActivity(player, out state))
        {
            return true;
        }

        VoicePlayerState voiceState = player.voicePlayerState;
        if (voiceState == null)
        {
            state = VoiceActivityState.NoData;
            return false;
        }

        bool isSpeaking = voiceState.IsSpeaking && !voiceState.IsLocallyMuted;
        state = new VoiceActivityState(
            true,
            isSpeaking,
            voiceState.Amplitude,
            voiceState.Volume,
            player.actualClientId,
            player.playerClientId,
            DateTime.UtcNow.Ticks);
        return true;
    }

    private static bool TryGetLocalVoiceActivity(PlayerControllerB player, out VoiceActivityState state)
    {
        state = VoiceActivityState.NoData;
        StartOfRound round = StartOfRound.Instance;
        if (round == null || round.voiceChatModule == null)
        {
            return false;
        }

        string localPlayerName = round.voiceChatModule.LocalPlayerName;
        if (string.IsNullOrWhiteSpace(localPlayerName))
        {
            return false;
        }

        VoicePlayerState voiceState = round.voiceChatModule.FindPlayer(localPlayerName);
        if (voiceState == null)
        {
            return false;
        }

        bool isSpeaking = voiceState.IsSpeaking && !round.voiceChatModule.IsMuted;
        state = new VoiceActivityState(
            true,
            isSpeaking,
            voiceState.Amplitude,
            voiceState.Volume,
            player.actualClientId,
            player.playerClientId,
            DateTime.UtcNow.Ticks);
        return true;
    }

    private static bool IsLocalPlayer(PlayerControllerB player)
    {
        GameNetworkManager networkManager = GameNetworkManager.Instance;
        return networkManager != null && networkManager.localPlayerController == player;
    }

    private static PlayerControllerB? FindPlayer(ulong clientId, ulong slotId)
    {
        StartOfRound round = StartOfRound.Instance;
        if (round == null || round.allPlayerScripts == null)
        {
            return null;
        }

        foreach (PlayerControllerB player in round.allPlayerScripts)
        {
            if (player == null)
            {
                continue;
            }

            if (player.actualClientId == clientId)
            {
                return player;
            }
        }

        foreach (PlayerControllerB player in round.allPlayerScripts)
        {
            if (player == null)
            {
                continue;
            }

            if (player.playerClientId == slotId)
            {
                return player;
            }
        }

        return null;
    }
}
