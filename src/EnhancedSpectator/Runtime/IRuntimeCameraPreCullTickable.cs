using UnityEngine;

namespace EnhancedSpectator.Runtime;

/// <summary>
/// Receives a callback immediately before Unity renders a camera.
/// </summary>
public interface IRuntimeCameraPreCullTickable
{
    /// <summary>
    /// Ticks before the provided camera is culled and rendered.
    /// </summary>
    void CameraPreCullTick(Camera camera);
}
