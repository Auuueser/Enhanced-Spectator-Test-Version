using System.Collections.Generic;

namespace EnhancedSpectator.Features.VoiceDiagnostics;

/// <summary>
/// Read-only snapshot of current vanilla and Dissonance voice state.
/// </summary>
public sealed class VoiceDiagnosticsSnapshot
{
    /// <summary>
    /// Creates a voice diagnostics snapshot.
    /// </summary>
    public VoiceDiagnosticsSnapshot(
        bool hasRound,
        bool hasLocalPlayer,
        bool hasVoiceChatModule,
        string localDissonancePlayerName,
        bool voiceChatMuted,
        bool voiceChatDeafened,
        ulong localClientId,
        ulong localPlayerSlotId,
        bool isLocalPlayerDead,
        bool isLocalPlayerSpectating,
        ulong? spectatedTargetClientId,
        ulong? spectatedTargetPlayerSlotId,
        bool includeAudioSourceDiagnostics,
        bool includeWalkieDiagnostics,
        IReadOnlyList<PlayerVoiceDiagnosticsSnapshot> players,
        long timestampTicks)
    {
        HasRound = hasRound;
        HasLocalPlayer = hasLocalPlayer;
        HasVoiceChatModule = hasVoiceChatModule;
        LocalDissonancePlayerName = localDissonancePlayerName;
        VoiceChatMuted = voiceChatMuted;
        VoiceChatDeafened = voiceChatDeafened;
        LocalClientId = localClientId;
        LocalPlayerSlotId = localPlayerSlotId;
        IsLocalPlayerDead = isLocalPlayerDead;
        IsLocalPlayerSpectating = isLocalPlayerSpectating;
        SpectatedTargetClientId = spectatedTargetClientId;
        SpectatedTargetPlayerSlotId = spectatedTargetPlayerSlotId;
        IncludeAudioSourceDiagnostics = includeAudioSourceDiagnostics;
        IncludeWalkieDiagnostics = includeWalkieDiagnostics;
        Players = players;
        TimestampTicks = timestampTicks;
    }

    /// <summary>
    /// Whether StartOfRound exists.
    /// </summary>
    public bool HasRound { get; }

    /// <summary>
    /// Whether the local player exists.
    /// </summary>
    public bool HasLocalPlayer { get; }

    /// <summary>
    /// Whether the Dissonance module exists.
    /// </summary>
    public bool HasVoiceChatModule { get; }

    /// <summary>
    /// Local Dissonance player id, if readable.
    /// </summary>
    public string LocalDissonancePlayerName { get; }

    /// <summary>
    /// Dissonance local mute state.
    /// </summary>
    public bool VoiceChatMuted { get; }

    /// <summary>
    /// Dissonance local deafened state.
    /// </summary>
    public bool VoiceChatDeafened { get; }

    /// <summary>
    /// Local Netcode client id.
    /// </summary>
    public ulong LocalClientId { get; }

    /// <summary>
    /// Local vanilla player slot id.
    /// </summary>
    public ulong LocalPlayerSlotId { get; }

    /// <summary>
    /// Whether the local player is dead.
    /// </summary>
    public bool IsLocalPlayerDead { get; }

    /// <summary>
    /// Whether the local player has begun spectating.
    /// </summary>
    public bool IsLocalPlayerSpectating { get; }

    /// <summary>
    /// Current vanilla spectated target actual client id.
    /// </summary>
    public ulong? SpectatedTargetClientId { get; }

    /// <summary>
    /// Current vanilla spectated target player slot id.
    /// </summary>
    public ulong? SpectatedTargetPlayerSlotId { get; }

    /// <summary>
    /// Whether the snapshot includes audio source details.
    /// </summary>
    public bool IncludeAudioSourceDiagnostics { get; }

    /// <summary>
    /// Whether the snapshot includes walkie details.
    /// </summary>
    public bool IncludeWalkieDiagnostics { get; }

    /// <summary>
    /// Player voice rows.
    /// </summary>
    public IReadOnlyList<PlayerVoiceDiagnosticsSnapshot> Players { get; }

    /// <summary>
    /// Snapshot timestamp.
    /// </summary>
    public long TimestampTicks { get; }
}
