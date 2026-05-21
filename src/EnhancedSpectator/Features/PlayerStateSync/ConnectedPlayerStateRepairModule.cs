using System;
using EnhancedSpectator.Config;
using EnhancedSpectator.GameInterop;
using EnhancedSpectator.Networking;
using EnhancedSpectator.Runtime;
using UnityEngine;

namespace EnhancedSpectator.Features.PlayerStateSync;

/// <summary>
/// Periodically repairs late vanilla connected-player state for already identified modded peers.
/// </summary>
public sealed class ConnectedPlayerStateRepairModule : IFeatureModule, IRuntimeTickable
{
    private const float RepairIntervalSeconds = 0.5f;

    private readonly EnhancedSpectatorConfig _config;
    private readonly IEnhancedSpectatorNetworkService _networkService;
    private readonly IConnectedPlayerStateRepairAdapter _repairAdapter;

    private float _nextRepairTime;
    private bool _initialized;

    /// <summary>
    /// Creates a connected player state repair module.
    /// </summary>
    public ConnectedPlayerStateRepairModule(
        EnhancedSpectatorConfig config,
        IEnhancedSpectatorNetworkService networkService,
        IConnectedPlayerStateRepairAdapter repairAdapter)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _networkService = networkService ?? throw new ArgumentNullException(nameof(networkService));
        _repairAdapter = repairAdapter ?? throw new ArgumentNullException(nameof(repairAdapter));
    }

    /// <inheritdoc />
    public void Initialize()
    {
        _initialized = true;
        _nextRepairTime = 0f;
    }

    /// <inheritdoc />
    public void Tick()
    {
        if (!_initialized
            || !_config.RepairVanillaConnectedPlayerState.Value
            || Time.unscaledTime < _nextRepairTime)
        {
            return;
        }

        _nextRepairTime = Time.unscaledTime + RepairIntervalSeconds;
        if (!RuntimeConnectionState.CanUseModNetworking(out _)
            || !_networkService.IsNetworkAvailable)
        {
            return;
        }

        int repairs = _repairAdapter.RepairConnectedPlayerState(
            _networkService.GetRemotePeerIdentities(),
            _networkService.GetRemoteSpectatorTargets(),
            updatePlayerNames: _config.RepairVanillaPlayerNames.Value,
            updateQuickMenu: true,
            debug: _config.DebugPlayerStateRepair.Value,
            out _);
        _ = repairs;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _initialized = false;
    }
}
