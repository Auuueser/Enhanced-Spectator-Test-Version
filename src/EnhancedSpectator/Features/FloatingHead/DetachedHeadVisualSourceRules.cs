namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Pure decision rules for runtime detached-head visual source selection.
/// </summary>
public static class DetachedHeadVisualSourceRules
{
    /// <summary>
    /// Gets whether a runtime detached-head source should be used for the visual.
    /// </summary>
    public static bool ShouldUseRuntimeDetachedHead(
        bool useRuntimeDetachedHeadVisuals,
        bool hasRuntimeDetachedHeadTemplate)
    {
        return useRuntimeDetachedHeadVisuals && hasRuntimeDetachedHeadTemplate;
    }

    /// <summary>
    /// Resolves the visual source kind to create for the current configuration and source availability.
    /// </summary>
    public static bool TryResolveVisualSourceKind(
        bool enablePlaceholderVisuals,
        bool useRuntimeDetachedHeadVisuals,
        bool hasRuntimeDetachedHeadTemplate,
        bool fallbackToPlaceholderWhenDetachedHeadUnavailable,
        out FloatingHeadVisualSourceKind sourceKind)
    {
        if (ShouldUseRuntimeDetachedHead(useRuntimeDetachedHeadVisuals, hasRuntimeDetachedHeadTemplate))
        {
            sourceKind = FloatingHeadVisualSourceKind.RuntimeDetachedHead;
            return true;
        }

        if (!enablePlaceholderVisuals)
        {
            sourceKind = FloatingHeadVisualSourceKind.Placeholder;
            return false;
        }

        if (!useRuntimeDetachedHeadVisuals || fallbackToPlaceholderWhenDetachedHeadUnavailable)
        {
            sourceKind = FloatingHeadVisualSourceKind.Placeholder;
            return true;
        }

        sourceKind = FloatingHeadVisualSourceKind.Placeholder;
        return false;
    }
}
