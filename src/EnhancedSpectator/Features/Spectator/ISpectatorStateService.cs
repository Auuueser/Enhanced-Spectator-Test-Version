namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Exposes the current spectator state through a feature-level abstraction.
/// </summary>
public interface ISpectatorStateService
{
    /// <summary>
    /// Gets the latest known spectator state.
    /// </summary>
    SpectatorState Current { get; }

    /// <summary>
    /// Refreshes the current spectator state from the game adapter.
    /// </summary>
    void Refresh();
}
