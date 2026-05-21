using System.Collections.Generic;
using EnhancedSpectator.Features.ModelInspection;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Adapter boundary for reading runtime player model hierarchy metadata.
/// </summary>
public interface IGamePlayerModelInspectionAdapter
{
    /// <summary>
    /// Attempts to read player model inspection snapshots from current game objects.
    /// </summary>
    bool TryGetPlayerModelInspectionSnapshots(
        bool includeLocalPlayer,
        bool includeRemotePlayers,
        int maxTransformDepth,
        bool includeRendererBounds,
        bool includeMaterials,
        out IReadOnlyList<PlayerModelInspectionSnapshot> snapshots);
}
