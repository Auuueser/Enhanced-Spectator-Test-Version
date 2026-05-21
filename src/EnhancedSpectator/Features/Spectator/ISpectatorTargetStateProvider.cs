using EnhancedSpectator.Networking;

namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Provides the current local spectator target state for networking modules.
/// </summary>
public interface ISpectatorTargetStateProvider
{
    /// <summary>
    /// Attempts to read the current local spectator target state.
    /// </summary>
    bool TryGetCurrentSpectatorTarget(out SpectatorTargetState state);
}
