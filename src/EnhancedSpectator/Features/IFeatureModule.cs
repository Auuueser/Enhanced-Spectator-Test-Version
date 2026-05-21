using System;

namespace EnhancedSpectator.Features;

/// <summary>
/// Represents a feature module with a simple plugin lifetime.
/// </summary>
public interface IFeatureModule : IDisposable
{
    /// <summary>
    /// Initializes the feature module.
    /// </summary>
    void Initialize();
}
