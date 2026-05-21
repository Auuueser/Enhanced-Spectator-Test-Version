using System;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Runtime;

namespace EnhancedSpectator.Features.SpectatorPresence;

/// <summary>
/// Feature module for debug-only remote spectator presence inference.
/// </summary>
public sealed class SpectatorPresenceModule : IFeatureModule, IRuntimeTickable
{
    private readonly SpectatorPresenceService _presenceService;
    private bool _initialized;

    /// <summary>
    /// Creates a spectator presence module.
    /// </summary>
    public SpectatorPresenceModule(SpectatorPresenceService presenceService)
    {
        _presenceService = presenceService ?? throw new ArgumentNullException(nameof(presenceService));
    }

    /// <inheritdoc />
    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;
        ModLog.Debug("Spectator presence debug module initialized.");
    }

    /// <inheritdoc />
    public void Tick()
    {
        if (!_initialized)
        {
            return;
        }

        _presenceService.Tick();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (!_initialized)
        {
            return;
        }

        _presenceService.Clear();
        _initialized = false;
        ModLog.Debug("Spectator presence debug module disposed.");
    }
}
