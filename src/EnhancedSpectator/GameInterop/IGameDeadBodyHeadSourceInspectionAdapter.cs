using System.Collections.Generic;
using EnhancedSpectator.Features.ModelInspection;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Adapter boundary for reading runtime dead-body detached-head metadata.
/// </summary>
public interface IGameDeadBodyHeadSourceInspectionAdapter
{
    /// <summary>
    /// Attempts to read detached-head source snapshots from current player dead bodies.
    /// </summary>
    bool TryGetDeadBodyHeadSourceInspectionSnapshots(
        int maxTransformDepth,
        bool includeRendererBounds,
        bool includeMaterials,
        out IReadOnlyList<DeadBodyHeadSourceInspectionSnapshot> snapshots);
}
