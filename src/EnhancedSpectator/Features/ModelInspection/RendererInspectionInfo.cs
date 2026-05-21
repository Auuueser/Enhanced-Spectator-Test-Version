using System.Collections.Generic;
using UnityEngine;

namespace EnhancedSpectator.Features.ModelInspection;

/// <summary>
/// Captures renderer metadata without reading mesh vertex data.
/// </summary>
public sealed class RendererInspectionInfo
{
    /// <summary>
    /// Creates a renderer inspection entry.
    /// </summary>
    public RendererInspectionInfo(
        string path,
        string name,
        bool enabled,
        string sharedMeshName,
        int subMeshCount,
        int bonesCount,
        string rootBoneName,
        Vector3? boundsCenter,
        Vector3? boundsSize,
        int materialSlotCount,
        IReadOnlyList<string> materialNames)
    {
        Path = path;
        Name = name;
        Enabled = enabled;
        SharedMeshName = sharedMeshName;
        SubMeshCount = subMeshCount;
        BonesCount = bonesCount;
        RootBoneName = rootBoneName;
        BoundsCenter = boundsCenter;
        BoundsSize = boundsSize;
        MaterialSlotCount = materialSlotCount;
        MaterialNames = materialNames;
    }

    /// <summary>
    /// Gets the renderer path from the inspected player root.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Gets the renderer object name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets whether the renderer is enabled.
    /// </summary>
    public bool Enabled { get; }

    /// <summary>
    /// Gets the shared mesh name.
    /// </summary>
    public string SharedMeshName { get; }

    /// <summary>
    /// Gets the shared mesh submesh count.
    /// </summary>
    public int SubMeshCount { get; }

    /// <summary>
    /// Gets the renderer bone count.
    /// </summary>
    public int BonesCount { get; }

    /// <summary>
    /// Gets the root bone name.
    /// </summary>
    public string RootBoneName { get; }

    /// <summary>
    /// Gets renderer world bounds center when requested.
    /// </summary>
    public Vector3? BoundsCenter { get; }

    /// <summary>
    /// Gets renderer world bounds size when requested.
    /// </summary>
    public Vector3? BoundsSize { get; }

    /// <summary>
    /// Gets material slot count.
    /// </summary>
    public int MaterialSlotCount { get; }

    /// <summary>
    /// Gets material names when explicitly requested.
    /// </summary>
    public IReadOnlyList<string> MaterialNames { get; }
}
