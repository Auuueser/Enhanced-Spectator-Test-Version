using UnityEngine;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Adapter boundary for resolving runtime detached-head visual template objects.
/// </summary>
public interface IGameDetachedHeadVisualSourceAdapter
{
    /// <summary>
    /// Attempts to resolve a reusable runtime detached-head visual template object.
    /// </summary>
    bool TryGetDetachedHeadVisualTemplate(out Transform? source);
}
