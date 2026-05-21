using Unity.Netcode;
using UnityEngine;

namespace EnhancedSpectator.Runtime;

/// <summary>
/// Tracks process and network lifecycle windows where mod-owned work should pause.
/// </summary>
public static class RuntimeConnectionState
{
    private const float SceneTransitionUnsafeSeconds = 0.5f;

    private static bool _applicationQuitting;
    private static bool _pluginShuttingDown;
    private static float _unsafeUntilRealtime;

    /// <summary>
    /// Clears lifecycle state for a new plugin run.
    /// </summary>
    public static void Reset()
    {
        _applicationQuitting = false;
        _pluginShuttingDown = false;
        _unsafeUntilRealtime = 0f;
    }

    /// <summary>
    /// Marks that Unity is quitting.
    /// </summary>
    public static void MarkApplicationQuitting()
    {
        _applicationQuitting = true;
        MarkUnsafeWindow();
    }

    /// <summary>
    /// Marks that the plugin is shutting down intentionally.
    /// </summary>
    public static void MarkPluginShuttingDown()
    {
        _pluginShuttingDown = true;
        MarkUnsafeWindow();
    }

    /// <summary>
    /// Marks a short scene transition window where vanilla objects may be partially destroyed.
    /// </summary>
    public static void MarkSceneTransition()
    {
        MarkUnsafeWindow();
    }

    /// <summary>
    /// Gets whether mod-owned network messages may be sent or handled.
    /// </summary>
    public static bool CanUseModNetworking(out string reason)
    {
        if (IsLifecycleUnsafe(out reason))
        {
            return false;
        }

        NetworkManager? networkManager = NetworkManager.Singleton;
        if (networkManager == null)
        {
            reason = "NetworkManager unavailable";
            return false;
        }

        if (networkManager.ShutdownInProgress)
        {
            reason = "NetworkManager shutdown in progress";
            return false;
        }

        if (!networkManager.IsListening)
        {
            reason = "NetworkManager is not listening";
            return false;
        }

        if (!networkManager.IsClient && !networkManager.IsHost)
        {
            reason = "not connected as client or host";
            return false;
        }

        if (!networkManager.IsConnectedClient && !networkManager.IsHost)
        {
            reason = "local client is not connected";
            return false;
        }

        if (networkManager.CustomMessagingManager == null)
        {
            reason = "CustomMessagingManager unavailable";
            return false;
        }

        reason = string.Empty;
        return true;
    }

    /// <summary>
    /// Gets whether local read-only diagnostics may inspect game state.
    /// </summary>
    public static bool CanRunLocalDiagnostics(out string reason)
    {
        if (IsLifecycleUnsafe(out reason))
        {
            return false;
        }

        NetworkManager? networkManager = NetworkManager.Singleton;
        if (networkManager != null && networkManager.ShutdownInProgress)
        {
            reason = "NetworkManager shutdown in progress";
            return false;
        }

        reason = string.Empty;
        return true;
    }

    /// <summary>
    /// Gets whether a narrow vanilla player-state repair may safely mutate local gameplay state.
    /// </summary>
    public static bool CanRepairVanillaPlayerState(out string reason)
    {
        if (IsLifecycleUnsafe(out reason))
        {
            return false;
        }

        NetworkManager? networkManager = NetworkManager.Singleton;
        if (networkManager != null && networkManager.ShutdownInProgress)
        {
            reason = "NetworkManager shutdown in progress";
            return false;
        }

        reason = string.Empty;
        return true;
    }

    /// <summary>
    /// Gets whether vanilla spectator target switching should be skipped during teardown.
    /// </summary>
    public static bool ShouldSkipVanillaSpectatorTargetSwitch(out string reason)
    {
        if (IsLifecycleUnsafe(out reason))
        {
            return true;
        }

        NetworkManager? networkManager = NetworkManager.Singleton;
        if (networkManager == null)
        {
            reason = "NetworkManager unavailable";
            return true;
        }

        if (networkManager.ShutdownInProgress)
        {
            reason = "NetworkManager shutdown in progress";
            return true;
        }

        if (!networkManager.IsListening || (!networkManager.IsClient && !networkManager.IsHost))
        {
            reason = "network is disconnected";
            return true;
        }

        reason = string.Empty;
        return false;
    }

    private static bool IsLifecycleUnsafe(out string reason)
    {
        if (_applicationQuitting)
        {
            reason = "application is quitting";
            return true;
        }

        if (_pluginShuttingDown)
        {
            reason = "plugin is shutting down";
            return true;
        }

        if (Time.realtimeSinceStartup < _unsafeUntilRealtime)
        {
            reason = "scene transition unsafe window";
            return true;
        }

        reason = string.Empty;
        return false;
    }

    private static void MarkUnsafeWindow()
    {
        float unsafeUntil = Time.realtimeSinceStartup + SceneTransitionUnsafeSeconds;
        if (unsafeUntil > _unsafeUntilRealtime)
        {
            _unsafeUntilRealtime = unsafeUntil;
        }
    }
}
