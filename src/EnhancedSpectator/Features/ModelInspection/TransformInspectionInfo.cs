namespace EnhancedSpectator.Features.ModelInspection;

/// <summary>
/// Captures a transform path relevant to player head hierarchy research.
/// </summary>
public sealed class TransformInspectionInfo
{
    /// <summary>
    /// Creates a transform inspection entry.
    /// </summary>
    public TransformInspectionInfo(string path, string name, int depth)
    {
        Path = path;
        Name = name;
        Depth = depth;
    }

    /// <summary>
    /// Gets the path from the inspected player root.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Gets the transform name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the depth below the inspected player root.
    /// </summary>
    public int Depth { get; }
}
