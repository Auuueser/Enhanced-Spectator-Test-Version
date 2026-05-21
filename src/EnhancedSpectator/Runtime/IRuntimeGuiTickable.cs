namespace EnhancedSpectator.Runtime;

/// <summary>
/// Receives Unity IMGUI callbacks for debug-only runtime overlays.
/// </summary>
public interface IRuntimeGuiTickable
{
    /// <summary>
    /// Ticks during Unity OnGUI.
    /// </summary>
    void GuiTick();
}
