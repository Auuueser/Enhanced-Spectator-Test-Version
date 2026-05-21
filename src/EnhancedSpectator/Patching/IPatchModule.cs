using HarmonyLib;

namespace EnhancedSpectator.Patching;

/// <summary>
/// Represents one Harmony patch registration unit.
/// </summary>
public interface IPatchModule
{
    /// <summary>
    /// Registers patches against the provided Harmony instance.
    /// </summary>
    void Register(Harmony harmony);

    /// <summary>
    /// Unregisters patches owned by this module.
    /// </summary>
    void Unregister(Harmony harmony);
}
