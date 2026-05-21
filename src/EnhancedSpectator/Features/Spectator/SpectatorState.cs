namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Represents spectator state known to the mod without exposing game-specific types.
/// </summary>
public sealed class SpectatorState
{
    /// <summary>
    /// Placeholder state used before game interop is implemented.
    /// </summary>
    public static SpectatorState Unavailable { get; } = new SpectatorState(
        false,
        "Game spectator state is not wired yet.");

    /// <summary>
    /// Creates a spectator state value.
    /// </summary>
    public SpectatorState(
        bool isAvailable,
        string? status,
        bool isLocalPlayerSpectating = false,
        bool isFreecamActive = false)
    {
        IsAvailable = isAvailable;
        Status = status;
        IsLocalPlayerSpectating = isLocalPlayerSpectating;
        IsFreecamActive = isFreecamActive;
    }

    /// <summary>
    /// Gets whether the state came from an implemented game adapter.
    /// </summary>
    public bool IsAvailable { get; }

    /// <summary>
    /// Gets whether the local player is in a spectator-compatible state.
    /// </summary>
    public bool IsLocalPlayerSpectating { get; }

    /// <summary>
    /// Gets whether enhanced freecam is actively writing the spectator camera.
    /// </summary>
    public bool IsFreecamActive { get; }

    /// <summary>
    /// Gets a human-readable diagnostic status.
    /// </summary>
    public string? Status { get; }
}
