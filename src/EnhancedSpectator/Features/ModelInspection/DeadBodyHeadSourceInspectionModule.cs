using System;
using EnhancedSpectator.Config;
using EnhancedSpectator.Features.Spectator;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Runtime;

namespace EnhancedSpectator.Features.ModelInspection;

/// <summary>
/// Feature module for key-triggered dead-body detached-head source inspection.
/// </summary>
public sealed class DeadBodyHeadSourceInspectionModule : IFeatureModule, IRuntimeTickable
{
    private readonly EnhancedSpectatorConfig _config;
    private readonly DeadBodyHeadSourceInspectionService _inspectionService;
    private bool _initialized;

    /// <summary>
    /// Creates a detached-head source inspection module.
    /// </summary>
    public DeadBodyHeadSourceInspectionModule(
        EnhancedSpectatorConfig config,
        DeadBodyHeadSourceInspectionService inspectionService)
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
        ModLog.Debug("Head source inspection module initialized.");
    }

    /// <inheritdoc />
    public void Tick()
    {
        if (!_initialized || !_config.EnableRuntimeHeadSourceInspection.Value)
        {
            return;
        }

        if (!SpectatorInputService.IsKeyPressedThisFrame(_config.RuntimeHeadSourceInspectionKey.Value))
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
        ModLog.Debug("Head source inspection module disposed.");
    }
}
