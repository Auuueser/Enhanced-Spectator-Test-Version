namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Describes low-risk vanilla spectator lifecycle events observed by patches.
/// </summary>
public enum SpectatorLifecycleEventKind
{
    /// <summary>
    /// A player death method completed.
    /// </summary>
    PlayerDied,

    /// <summary>
    /// Vanilla spectated-player effects were applied.
    /// </summary>
    SpectatedPlayerEffectsApplied,

    /// <summary>
    /// Vanilla switched the active camera.
    /// </summary>
    CameraSwitched,

    /// <summary>
    /// Vanilla changed game-over spectator camera override state.
    /// </summary>
    GameOverOverrideChanged,

    /// <summary>
    /// Vanilla revived dead players or cleaned up spectator state.
    /// </summary>
    Revived,
}
