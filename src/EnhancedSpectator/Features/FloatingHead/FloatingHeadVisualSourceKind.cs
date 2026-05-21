namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Identifies the runtime source used to create a floating-head visual.
/// </summary>
public enum FloatingHeadVisualSourceKind
{
    /// <summary>
    /// Runtime-created placeholder marker.
    /// </summary>
    Placeholder,

    /// <summary>
    /// Runtime visual copy of a confirmed detached-head template object.
    /// </summary>
    RuntimeDetachedHead,
}
