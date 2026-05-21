using System;
using System.Collections.Generic;
using EnhancedSpectator.Features.FloatingHead;
using EnhancedSpectator.Config;
using EnhancedSpectator.Features.ModelInspection;
using EnhancedSpectator.Features.PlayerStateSync;
using EnhancedSpectator.Features.Spectator;
using EnhancedSpectator.Features.SpectatorPresence;
using EnhancedSpectator.Features.VoiceActivity;
using EnhancedSpectator.Features.VoiceDiagnostics;
using EnhancedSpectator.Features.VoiceRouting;
using EnhancedSpectator.GameInterop;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Networking;
using EnhancedSpectator.Runtime;
using UnityEngine;

namespace EnhancedSpectator.Features;

/// <summary>
/// Coordinates feature module creation and lifetime.
/// </summary>
public sealed class FeatureBootstrapper : IDisposable
{
    private readonly List<IFeatureModule> _features = new List<IFeatureModule>();
    private bool _initialized;

    /// <summary>
    /// Creates the configured feature modules.
    /// </summary>
    public FeatureBootstrapper(EnhancedSpectatorConfig config)
    {
        if (config.EnableSpectatorModule.Value)
        {
            IGameSpectatorAdapter gameSpectatorAdapter = new LethalCompanySpectatorAdapter();
            SpectatorFreecamSettings freecamSettings = new SpectatorFreecamSettings(config);
            SpectatorModule spectatorModule = new SpectatorModule(gameSpectatorAdapter, freecamSettings);
            _features.Add(spectatorModule);

            if (config.EnableNetworking.Value)
            {
                LethalCompanyVoiceActivityProvider voiceActivityProvider = new LethalCompanyVoiceActivityProvider();
                EnhancedSpectatorNetworkService networkService = new EnhancedSpectatorNetworkService(
                    config,
                    spectatorModule,
                    spectatorModule,
                    spectatorModule,
                    voiceActivityProvider,
                    new UnityNetcodeMessagingTransport(() => config.DebugNetworkMessages.Value));
                _features.Add(new NetworkingModule(networkService));
                _features.Add(new ConnectedPlayerStateRepairModule(
                    config,
                    networkService,
                    new LethalCompanyConnectedPlayerStateRepairAdapter()));
                SpectatorPresenceService presenceService = new SpectatorPresenceService(
                    config,
                    gameSpectatorAdapter,
                    networkService);
                _features.Add(new SpectatorPresenceModule(presenceService));
                _features.Add(new SpectatorVoiceRoutingModule(
                    new SpectatorVoiceRoutingService(
                        config,
                        networkService,
                        new LethalCompanySpectatorVoiceRoutingAdapter(
                            networkService,
                            () => config.EnableDebugLogging.Value && config.DebugSpectatorVoiceRouting.Value))));

                if (config.EnableFloatingHeadVisuals.Value)
                {
                    FloatingHeadVisualService visualService = new FloatingHeadVisualService(
                        config,
                        presenceService,
                        voiceActivityProvider,
                        networkService,
                        new LethalCompanyDetachedHeadVisualSourceAdapter(),
                        new FloatingHeadPlacementService(gameSpectatorAdapter),
                        new PlaceholderHeadVisualFactory());
                    _features.Add(new FloatingHeadModule(visualService));
                }
            }
        }

        if (config.EnableModelInspection.Value)
        {
            _features.Add(new ModelInspectionModule(
                config,
                new PlayerModelInspectionService(config, new LethalCompanyPlayerModelInspectionAdapter())));
        }

        if (config.EnableRuntimeHeadSourceInspection.Value)
        {
            _features.Add(new DeadBodyHeadSourceInspectionModule(
                config,
                new DeadBodyHeadSourceInspectionService(
                    config,
                    new LethalCompanyDeadBodyHeadSourceInspectionAdapter())));
        }

        if (config.EnableVoiceDiagnostics.Value)
        {
            _features.Add(new VoiceDiagnosticsModule(
                config,
                new VoiceDiagnosticsService(
                    config,
                    new LethalCompanyVoiceDiagnosticsAdapter())));
        }
    }

    /// <summary>
    /// Initializes all configured feature modules.
    /// </summary>
    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        foreach (IFeatureModule feature in _features)
        {
            feature.Initialize();
        }

        _initialized = true;
        ModLog.Debug("Feature modules initialized.");
    }

    /// <summary>
    /// Ticks runtime feature modules during Unity Update.
    /// </summary>
    public void Tick()
    {
        if (!_initialized)
        {
            return;
        }

        foreach (IFeatureModule feature in _features)
        {
            if (feature is IRuntimeTickable tickable)
            {
                tickable.Tick();
            }
        }
    }

    /// <summary>
    /// Ticks runtime feature modules during Unity LateUpdate.
    /// </summary>
    public void LateTick()
    {
        if (!_initialized)
        {
            return;
        }

        foreach (IFeatureModule feature in _features)
        {
            if (feature is IRuntimeLateTickable lateTickable)
            {
                lateTickable.LateTick();
            }
        }
    }

    /// <summary>
    /// Ticks runtime feature modules immediately before Unity renders a camera.
    /// </summary>
    public void CameraPreCullTick(Camera camera)
    {
        if (!_initialized)
        {
            return;
        }

        foreach (IFeatureModule feature in _features)
        {
            if (feature is IRuntimeCameraPreCullTickable cameraPreCullTickable)
            {
                cameraPreCullTickable.CameraPreCullTick(camera);
            }
        }
    }

    /// <summary>
    /// Ticks runtime feature modules during Unity OnGUI.
    /// </summary>
    public void GuiTick()
    {
        if (!_initialized)
        {
            return;
        }

        foreach (IFeatureModule feature in _features)
        {
            if (feature is IRuntimeGuiTickable guiTickable)
            {
                guiTickable.GuiTick();
            }
        }
    }

    /// <summary>
    /// Disposes all configured feature modules in reverse order.
    /// </summary>
    public void Dispose()
    {
        if (!_initialized)
        {
            return;
        }

        for (int index = _features.Count - 1; index >= 0; index--)
        {
            _features[index].Dispose();
        }

        _initialized = false;
        ModLog.Debug("Feature modules disposed.");
    }
}
