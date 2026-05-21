using System;
using System.Text;

namespace EnhancedSpectator.Features.VoiceDiagnostics;

/// <summary>
/// Formats voice diagnostics snapshots for BepInEx logs.
/// </summary>
public static class VoiceDiagnosticsReportFormatter
{
    /// <summary>
    /// Builds a compact multi-line voice diagnostics report.
    /// </summary>
    public static string Build(VoiceDiagnosticsSnapshot snapshot)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Enhanced Spectator voice diagnostics");
        builder.AppendLine(
            $"hasRound={snapshot.HasRound}, hasLocalPlayer={snapshot.HasLocalPlayer}, hasVoiceChatModule={snapshot.HasVoiceChatModule}, localVoiceName={FormatString(snapshot.LocalDissonancePlayerName)}, voiceMuted={snapshot.VoiceChatMuted}, voiceDeafened={snapshot.VoiceChatDeafened}");
        builder.AppendLine(
            $"localClientId={snapshot.LocalClientId}, localSlot={snapshot.LocalPlayerSlotId}, localDead={snapshot.IsLocalPlayerDead}, localSpectating={snapshot.IsLocalPlayerSpectating}, spectatedTargetClient={FormatNullable(snapshot.SpectatedTargetClientId)}, spectatedTargetSlot={FormatNullable(snapshot.SpectatedTargetPlayerSlotId)}");
        builder.AppendLine(
            $"players={snapshot.Players.Count}, includeAudio={snapshot.IncludeAudioSourceDiagnostics}, includeWalkie={snapshot.IncludeWalkieDiagnostics}, timestampTicks={snapshot.TimestampTicks}");

        if (snapshot.Players.Count == 0)
        {
            builder.AppendLine("no player voice rows captured");
        }

        foreach (PlayerVoiceDiagnosticsSnapshot player in snapshot.Players)
        {
            AppendPlayer(builder, snapshot, player);
        }

        return builder.ToString();
    }

    private static void AppendPlayer(
        StringBuilder builder,
        VoiceDiagnosticsSnapshot snapshot,
        PlayerVoiceDiagnosticsSnapshot player)
    {
        builder.AppendLine(
            $"player slot={player.PlayerClientId} client={player.ActualClientId} name={FormatString(player.PlayerName)} local={player.IsLocalPlayer} target={player.IsSpectatedTarget} controlled={player.IsPlayerControlled} dead={player.IsPlayerDead}");
        builder.AppendLine(
            $"  voice present={player.HasVoicePlayerState} connected={player.VoicePlayerIsConnected} speaking={player.VoicePlayerIsSpeaking} muted={player.VoicePlayerIsLocallyMuted} amplitude={player.VoiceAmplitude:0.00} volume={player.VoiceVolume:0.00} voiceName={FormatString(player.VoicePlayerName)}");

        if (snapshot.IncludeAudioSourceDiagnostics)
        {
            builder.AppendLine(
                $"  audio present={player.HasCurrentVoiceAudioSource} playing={player.VoiceAudioIsPlaying} muted={player.VoiceAudioMuted} volume={player.VoiceAudioVolume:0.00} spatialBlend={player.VoiceAudioSpatialBlend:0.00} mixer={FormatString(player.VoiceAudioMixerName)} ingameSettings={player.HasCurrentVoiceIngameSettings} set2D={player.VoiceIngameSettingsSet2D} playbackName={FormatString(player.VoicePlaybackPlayerName)}");
        }

        if (snapshot.IncludeWalkieDiagnostics)
        {
            builder.AppendLine(
                $"  walkie holding={player.HoldingWalkieTalkie} speaking={player.SpeakingToWalkieTalkie} muffled={player.VoiceMuffledByEnemy}");
        }
    }

    private static string FormatNullable(ulong? value)
    {
        return value.HasValue ? value.Value.ToString() : "none";
    }

    private static string FormatString(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? "none" : value.Trim();
    }
}
