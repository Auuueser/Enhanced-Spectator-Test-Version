using UnityEngine;

namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Shares local freecam input suppression state with spectator patches.
/// </summary>
public static class SpectatorVanillaInputGuard
{
    private static bool _freecamWantsVerticalInput;
    private static KeyCode _ascendKey = KeyCode.None;
    private static KeyCode _descendKey = KeyCode.None;
    private static bool _quickMenuBlocksInput;

    /// <summary>
    /// Updates the current input state that should suppress vanilla spectator controls.
    /// </summary>
    public static void Update(
        bool freecamEnabled,
        KeyCode ascendKey,
        KeyCode descendKey,
        bool quickMenuBlocksInput)
    {
        _freecamWantsVerticalInput = freecamEnabled;
        _ascendKey = ascendKey;
        _descendKey = descendKey;
        _quickMenuBlocksInput = quickMenuBlocksInput;
    }

    /// <summary>
    /// Clears all vanilla input suppression state.
    /// </summary>
    public static void Clear()
    {
        _freecamWantsVerticalInput = false;
        _ascendKey = KeyCode.None;
        _descendKey = KeyCode.None;
        _quickMenuBlocksInput = false;
    }

    /// <summary>
    /// Gets whether vanilla target switching should be suppressed for the current frame.
    /// </summary>
    public static bool ShouldSuppressTargetSwitchInput(out string reason)
    {
        if (_freecamWantsVerticalInput
            && (SpectatorInputService.IsKeyHeld(_ascendKey) || SpectatorInputService.IsKeyHeld(_descendKey)))
        {
            reason = "freecam vertical movement is held";
            return true;
        }

        reason = string.Empty;
        return false;
    }

    /// <summary>
    /// Gets whether local gameplay interaction input should be suppressed for the current frame.
    /// </summary>
    public static bool ShouldSuppressGameplayInteractInput()
    {
        return _quickMenuBlocksInput;
    }
}
