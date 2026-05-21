using System;
using EnhancedSpectator.Logging;

namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Receives lightweight notifications from spectator lifecycle patches.
/// </summary>
public static class SpectatorLifecycleEvents
{
    /// <summary>
    /// Raised when a low-risk vanilla spectator lifecycle event occurs.
    /// </summary>
    public static event Action<SpectatorLifecycleEventKind>? Changed;

    /// <summary>
    /// Raises a lifecycle event for feature modules.
    /// </summary>
    public static void Raise(SpectatorLifecycleEventKind kind)
    {
        try
        {
            Changed?.Invoke(kind);
        }
        catch (Exception ex)
        {
            ModLog.Error($"Spectator lifecycle subscriber failed for {kind}: {ex}");
        }
    }
}
