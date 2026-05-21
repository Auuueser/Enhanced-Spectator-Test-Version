using System;
using EnhancedSpectator.Runtime;

namespace EnhancedSpectator.Features.VoiceRouting;

/// <summary>
/// Feature module for the default-off spectator-to-target voice routing experiment.
/// </summary>
public sealed class SpectatorVoiceRoutingModule : IFeatureModule, IRuntimeLateTickable
{
    private readonly SpectatorVoiceRoutingService _service;
    private bool _initialized;

    /// <summary>
    /// Creates the spectator voice routing module.
    /// </summary>
    public SpectatorVoiceRoutingModule(SpectatorVoiceRoutingService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <inheritdoc />
    public void Initialize()
    {
        _initialized = true;
    }

    /// <inheritdoc />
    public void LateTick()
    {
        if (_initialized)
        {
            _service.LateTick();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (!_initialized)
        {
            return;
        }

        _service.Dispose();
        _initialized = false;
    }
}
