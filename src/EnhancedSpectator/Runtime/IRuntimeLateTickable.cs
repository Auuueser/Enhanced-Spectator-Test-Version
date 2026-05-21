namespace EnhancedSpectator.Runtime;

/// <summary>
/// Represents a feature that needs a Unity LateUpdate tick.
/// </summary>
public interface IRuntimeLateTickable
{
    /// <summary>
    /// Runs once per Unity LateUpdate.
    /// </summary>
    void LateTick();
}
