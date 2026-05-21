namespace EnhancedSpectator.Config;

/// <summary>
/// Configures which modded local players can hear routed dead spectator voice.
/// </summary>
public enum SpectatorVoiceAudienceMode
{
    /// <summary>
    /// Only the current vanilla spectate target can hear the dead spectator.
    /// </summary>
    WatchedTargetOnly,

    /// <summary>
    /// All compatible modded players can hear dead spectators.
    /// </summary>
    AllModdedPlayers,

    /// <summary>
    /// Only living compatible modded players can hear dead spectators.
    /// </summary>
    AliveModdedPlayersOnly,

    /// <summary>
    /// Only dead or spectating compatible modded players can hear dead spectators.
    /// </summary>
    DeadModdedPlayersOnly,
}
