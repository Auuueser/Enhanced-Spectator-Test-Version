using System;
using EnhancedSpectator.Config;
using EnhancedSpectator.Features.Spectator;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Runtime;

namespace EnhancedSpectator.Features.ModelInspection;

/// <summary>
/// Feature module for key-triggered player model hierarchy inspection.
/// </summary>
public sealed class ModelInspectionModule : IFeatureModule, IRuntimeTickable
{
    private readonly EnhancedSpectatorConfig _config;
    private readonly PlayerModelInspectionService _inspectionService;
    private bool _initialized;

    /// <summary>
    /// Creates a model inspection module.
    /// </summary>
    public ModelInspectionModule(
        EnhancedSpectatorConfig config,
        PlayerModelInspectionService inspectionService)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _inspectionService = inspectionService ?? throw new ArgumentNullException(nameof(inspectionService));
    }

    /// <inheritdoc />
    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;
        ModLog.Debug("Model inspection module initialized.");
    }

    /// <inheritdoc />
    public void Tick()
    {
        if (!_initialized || !_config.EnableModelInspection.Value)
        {
            return;
        }

        if (!SpectatorInputService.IsKeyPressedThisFrame(_config.ModelInspectionKey.Value))
        {
            return;
        }

        _inspectionService.InspectOnce();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (!_initialized)
        {
            return;
        }

        _initialized = false;
        ModLog.Debug("Model inspection module disposed.");
    }
}
