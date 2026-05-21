namespace EnhancedSpectator.Features.VoiceDiagnostics;

/// <summary>
/// Read-only voice diagnostics for one Lethal Company player slot.
/// </summary>
public sealed class PlayerVoiceDiagnosticsSnapshot
{
    /// <summary>
    /// Creates a player voice diagnostics snapshot.
    /// </summary>
    public PlayerVoiceDiagnosticsSnapshot(
        ulong playerClientId,
        ulong actualClientId,
        string playerName,
        string voicePlayerName,
        bool isLocalPlayer,
        bool isSpectatedTarget,
        bool isPlayerControlled,
        bool isPlayerDead,
        bool hasVoicePlayerState,
        bool voicePlayerIsConnected,
        bool voicePlayerIsSpeaking,
        bool voicePlayerIsLocallyMuted,
        float voiceAmplitude,
        float voiceVolume,
        bool hasCurrentVoiceAudioSource,
        bool voiceAudioIsPlaying,
        bool voiceAudioMuted,
        float voiceAudioVolume,
        float voiceAudioSpatialBlend,
        string voiceAudioMixerName,
        bool hasCurrentVoiceIngameSettings,
        bool voiceIngameSettingsSet2D,
        string voicePlaybackPlayerName,
        bool holdingWalkieTalkie,
        bool speakingToWalkieTalkie,
        bool voiceMuffledByEnemy)
    {
        PlayerClientId = playerClientId;
        ActualClientId = actualClientId;
        PlayerName = playerName;
        VoicePlayerName = voicePlayerName;
        IsLocalPlayer = isLocalPlayer;
        IsSpectatedTarget = isSpectatedTarget;
        IsPlayerControlled = isPlayerControlled;
        IsPlayerDead = isPlayerDead;
        HasVoicePlayerState = hasVoicePlayerState;
        VoicePlayerIsConnected = voicePlayerIsConnected;
        VoicePlayerIsSpeaking = voicePlayerIsSpeaking;
        VoicePlayerIsLocallyMuted = voicePlayerIsLocallyMuted;
        VoiceAmplitude = voiceAmplitude;
        VoiceVolume = voiceVolume;
        HasCurrentVoiceAudioSource = hasCurrentVoiceAudioSource;
        VoiceAudioIsPlaying = voiceAudioIsPlaying;
        VoiceAudioMuted = voiceAudioMuted;
        VoiceAudioVolume = voiceAudioVolume;
        VoiceAudioSpatialBlend = voiceAudioSpatialBlend;
        VoiceAudioMixerName = voiceAudioMixerName;
        HasCurrentVoiceIngameSettings = hasCurrentVoiceIngameSettings;
        VoiceIngameSettingsSet2D = voiceIngameSettingsSet2D;
        VoicePlaybackPlayerName = voicePlaybackPlayerName;
        HoldingWalkieTalkie = holdingWalkieTalkie;
        SpeakingToWalkieTalkie = speakingToWalkieTalkie;
        VoiceMuffledByEnemy = voiceMuffledByEnemy;
    }

    /// <summary>
    /// Vanilla player slot id.
    /// </summary>
    public ulong PlayerClientId { get; }

    /// <summary>
    /// Netcode actual client id.
    /// </summary>
    public ulong ActualClientId { get; }

    /// <summary>
    /// In-game display name if available.
    /// </summary>
    public string PlayerName { get; }

    /// <summary>
    /// Dissonance voice player id if available.
    /// </summary>
    public string VoicePlayerName { get; }

    /// <summary>
    /// Whether this entry represents the local player object.
    /// </summary>
    public bool IsLocalPlayer { get; }

    /// <summary>
    /// Whether this entry is the current vanilla spectated target for the local player.
    /// </summary>
    public bool IsSpectatedTarget { get; }

    /// <summary>
    /// Vanilla controlled-player flag.
    /// </summary>
    public bool IsPlayerControlled { get; }

    /// <summary>
    /// Vanilla dead-player flag.
    /// </summary>
    public bool IsPlayerDead { get; }

    /// <summary>
    /// Whether a Dissonance player state is mapped to this player.
    /// </summary>
    public bool HasVoicePlayerState { get; }

    /// <summary>
    /// Dissonance connected flag.
    /// </summary>
    public bool VoicePlayerIsConnected { get; }

    /// <summary>
    /// Dissonance speaking flag.
    /// </summary>
    public bool VoicePlayerIsSpeaking { get; }

    /// <summary>
    /// Dissonance local mute flag for this voice player.
    /// </summary>
    public bool VoicePlayerIsLocallyMuted { get; }

    /// <summary>
    /// Dissonance amplitude value.
    /// </summary>
    public float VoiceAmplitude { get; }

    /// <summary>
    /// Dissonance playback volume value.
    /// </summary>
    public float VoiceVolume { get; }

    /// <summary>
    /// Whether vanilla mapped a current voice audio source.
    /// </summary>
    public bool HasCurrentVoiceAudioSource { get; }

    /// <summary>
    /// Whether the mapped voice audio source is playing.
    /// </summary>
    public bool VoiceAudioIsPlaying { get; }

    /// <summary>
    /// Whether the mapped voice audio source is muted.
    /// </summary>
    public bool VoiceAudioMuted { get; }

    /// <summary>
    /// Mapped voice audio source volume.
    /// </summary>
    public float VoiceAudioVolume { get; }

    /// <summary>
    /// Mapped voice audio source spatial blend.
    /// </summary>
    public float VoiceAudioSpatialBlend { get; }

    /// <summary>
    /// Mapped voice audio source mixer name.
    /// </summary>
    public string VoiceAudioMixerName { get; }

    /// <summary>
    /// Whether vanilla mapped current voice in-game settings.
    /// </summary>
    public bool HasCurrentVoiceIngameSettings { get; }

    /// <summary>
    /// Vanilla PlayerVoiceIngameSettings 2D playback flag.
    /// </summary>
    public bool VoiceIngameSettingsSet2D { get; }

    /// <summary>
    /// Dissonance playback component player id if available.
    /// </summary>
    public string VoicePlaybackPlayerName { get; }

    /// <summary>
    /// Vanilla walkie listening flag.
    /// </summary>
    public bool HoldingWalkieTalkie { get; }

    /// <summary>
    /// Vanilla walkie speaking flag.
    /// </summary>
    public bool SpeakingToWalkieTalkie { get; }

    /// <summary>
    /// Vanilla enemy voice muffling flag.
    /// </summary>
    public bool VoiceMuffledByEnemy { get; }
}
