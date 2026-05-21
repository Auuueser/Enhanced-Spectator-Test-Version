namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Describes why the local spectator freecam cannot write the camera on the current frame.
/// </summary>
public enum SpectatorFreecamIneligibleReason
{
    /// <summary>
    /// The freecam is eligible.
    /// </summary>
    None,

    /// <summary>
    /// The feature is disabled by configuration.
    /// </summary>
    FeatureDisabled,

    /// <summary>
    /// The local player is not currently in a spectator-compatible state.
    /// </summary>
    NonSpectator,

    /// <summary>
    /// The vanilla spectator camera, target, or anchor is unavailable.
    /// </summary>
    MissingCameraAnchorOrTarget,

    /// <summary>
    /// The vanilla spectator camera is not currently the active camera.
    /// </summary>
    SpectateCameraInactive,

    /// <summary>
    /// Vanilla game-over spectator override is active.
    /// </summary>
    GameOverOverride
}

/// <summary>
/// Describes how the freecam should respond to an ineligible spectator snapshot.
/// </summary>
public enum SpectatorFreecamRecoveryAction
{
    /// <summary>
    /// Continue applying the enhanced freecam.
    /// </summary>
    Continue,

    /// <summary>
    /// Skip writing this frame, but preserve target and pose so the freecam can recover.
    /// </summary>
    SoftPausePreservePose,

    /// <summary>
    /// Deactivate without clearing the target pose.
    /// </summary>
    DeactivatePreservePose,

    /// <summary>
    /// Deactivate and clear the target pose.
    /// </summary>
    DeactivateClearPose,

    /// <summary>
    /// Reset the freecam because the local player is no longer spectating.
    /// </summary>
    ResetForNonSpectator
}

/// <summary>
/// Chooses whether short vanilla spectator transition windows should preserve freecam state.
/// </summary>
public static class SpectatorFreecamRecoveryPolicy
{
    /// <summary>
    /// Returns the recovery action for an ineligible spectator snapshot.
    /// </summary>
    public static SpectatorFreecamRecoveryAction GetIneligibleAction(
        SpectatorFreecamIneligibleReason reason,
        int currentFrame,
        bool hasPose,
        int targetSwitchGraceUntilFrame,
        int cameraInactiveGraceUntilFrame)
    {
        if (reason == SpectatorFreecamIneligibleReason.None)
        {
            return SpectatorFreecamRecoveryAction.Continue;
        }

        if (reason == SpectatorFreecamIneligibleReason.NonSpectator)
        {
            return SpectatorFreecamRecoveryAction.ResetForNonSpectator;
        }

        if (reason == SpectatorFreecamIneligibleReason.MissingCameraAnchorOrTarget)
        {
            return hasPose && IsWithinGrace(currentFrame, targetSwitchGraceUntilFrame)
                ? SpectatorFreecamRecoveryAction.SoftPausePreservePose
                : SpectatorFreecamRecoveryAction.DeactivateClearPose;
        }

        if (reason == SpectatorFreecamIneligibleReason.SpectateCameraInactive)
        {
            return hasPose && IsWithinGrace(currentFrame, cameraInactiveGraceUntilFrame)
                ? SpectatorFreecamRecoveryAction.SoftPausePreservePose
                : SpectatorFreecamRecoveryAction.DeactivatePreservePose;
        }

        return SpectatorFreecamRecoveryAction.DeactivatePreservePose;
    }

    /// <summary>
    /// Calculates the inclusive frame until which a grace window remains active.
    /// </summary>
    public static int ExtendGraceUntilFrame(int currentFrame, int graceFrames)
    {
        return currentFrame + (graceFrames < 0 ? 0 : graceFrames);
    }

    private static bool IsWithinGrace(int currentFrame, int graceUntilFrame)
    {
        return graceUntilFrame >= 0 && currentFrame <= graceUntilFrame;
    }
}
