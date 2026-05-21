namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Contains pure safety rules for local vanilla connected-player state repair.
/// </summary>
public static class ConnectedPlayerStateRepairRules
{
    /// <summary>
    /// Gets whether a connected non-local vanilla player slot should be restored to controlled.
    /// </summary>
    public static bool ShouldRestoreConnectedAliveControl(
        bool isLocalPlayer,
        bool isCurrentlyConnected,
        bool isPlayerControlled,
        bool isPlayerDead)
    {
        return !isLocalPlayer
            && isCurrentlyConnected
            && !isPlayerControlled
            && !isPlayerDead;
    }

    /// <summary>
    /// Gets whether a connected non-local vanilla player slot should clear stale disconnect state.
    /// </summary>
    public static bool ShouldClearDisconnectedMidGame(
        bool isLocalPlayer,
        bool isCurrentlyConnected,
        bool disconnectedMidGame)
    {
        return !isLocalPlayer
            && isCurrentlyConnected
            && disconnectedMidGame;
    }

    /// <summary>
    /// Gets whether a vanilla fallback display name should replace a generic player-number label.
    /// </summary>
    public static bool ShouldUseVanillaFallbackDisplayName(
        bool updatePlayerNames,
        bool hasModIdentityDisplayName,
        string? currentDisplayName)
    {
        return updatePlayerNames
            && !hasModIdentityDisplayName
            && !string.IsNullOrWhiteSpace(currentDisplayName)
            && PlayerDisplayNameRules.IsGenericPlayerNumber(currentDisplayName);
    }
}
