namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Runtime-only placeholder shape used for remote spectator markers.
/// </summary>
public enum FloatingHeadVisualStyle
{
    /// <summary>
    /// Uses a runtime Unity sphere primitive.
    /// </summary>
    Sphere,

    /// <summary>
    /// Uses a runtime camera-facing quad mesh.
    /// </summary>
    Billboard,

    /// <summary>
    /// Uses a runtime camera-facing ring mesh.
    /// </summary>
    Ring,
}
