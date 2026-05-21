namespace EnhancedSpectator.Runtime;

/// <summary>
/// Represents a feature that needs a Unity Update tick.
/// </summary>
public interface IRuntimeTickable
{
    /// <summary>
    /// Runs once per Unity Update.
    /// </summary>
    void Tick();
}
