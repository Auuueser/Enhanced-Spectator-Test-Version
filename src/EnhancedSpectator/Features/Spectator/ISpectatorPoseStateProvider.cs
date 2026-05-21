using EnhancedSpectator.Networking;

namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Provides the current local spectator camera pose for networking modules.
/// </summary>
public interface ISpectatorPoseStateProvider
{
    /// <summary>
    /// Attempts to read the current local spectator camera pose.
    /// </summary>
    bool TryGetCurrentSpectatorPose(out SpectatorPoseState state);
}
