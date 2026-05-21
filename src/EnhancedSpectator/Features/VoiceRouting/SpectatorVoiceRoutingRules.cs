using EnhancedSpectator.Config;
using EnhancedSpectator.Features.SpectatorPresence;

namespace EnhancedSpectator.Features.VoiceRouting;

/// <summary>
/// Pure eligibility rules for spectator-to-target voice routing.
/// </summary>
public static class SpectatorVoiceRoutingRules
{
    /// <summary>
    /// Gets whether a remote spectator should be audible to the local watched player.
    /// </summary>
    public static bool ShouldRouteToLocalTarget(
        bool featureEnabled,
        bool hasLocalPlayer,
        bool isLocalPlayerDead,
        RemoteSpectatorInfo spectator)
    {
        return featureEnabled
            && hasLocalPlayer
            && !isLocalPlayerDead
            && spectator.IsWatchingLocalPlayer;
    }

    /// <summary>
    /// Gets whether a remote dead spectator should be audible to the local player for the configured audience mode.
    /// </summary>
    public static bool ShouldRouteToLocalPlayer(
        bool featureEnabled,
        bool hasLocalPlayer,
        bool isLocalPlayerDead,
        bool isRemoteSpectating,
        bool isWatchingLocalPlayer,
        SpectatorVoiceAudienceMode audienceMode)
    {
        if (!featureEnabled || !hasLocalPlayer || !isRemoteSpectating)
        {
            return false;
        }

        switch (audienceMode)
        {
            case SpectatorVoiceAudienceMode.WatchedTargetOnly:
                return isWatchingLocalPlayer;
            case SpectatorVoiceAudienceMode.AllModdedPlayers:
                return true;
            case SpectatorVoiceAudienceMode.AliveModdedPlayersOnly:
                return !isLocalPlayerDead;
            case SpectatorVoiceAudienceMode.DeadModdedPlayersOnly:
                return isLocalPlayerDead;
            default:
                return false;
        }
    }
}
