using System;
using EnhancedSpectator.Runtime;
using UnityEngine;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Provides runtime lifecycle and time data used by mod-owned networking.
/// </summary>
public interface INetworkRuntimeState
{
    /// <summary>
    /// Gets the current Unity frame count.
    /// </summary>
    int FrameCount { get; }

    /// <summary>
    /// Gets Unity realtime since startup.
    /// </summary>
    float RealtimeSinceStartup { get; }

    /// <summary>
    /// Gets Unity unscaled time.
    /// </summary>
    float UnscaledTime { get; }

    /// <summary>
    /// Gets current UTC ticks.
    /// </summary>
    long UtcNowTicks { get; }

    /// <summary>
    /// Gets whether mod-owned network messaging may currently run.
    /// </summary>
    bool CanUseModNetworking(out string reason);
}

/// <summary>
/// Runtime implementation backed by Unity and the shared lifecycle guard.
/// </summary>
public sealed class UnityNetworkRuntimeState : INetworkRuntimeState
{
    /// <summary>
    /// Gets the shared runtime state instance.
    /// </summary>
    public static UnityNetworkRuntimeState Instance { get; } = new UnityNetworkRuntimeState();

    private UnityNetworkRuntimeState()
    {
    }

    /// <inheritdoc />
    public int FrameCount => Time.frameCount;

    /// <inheritdoc />
    public float RealtimeSinceStartup => Time.realtimeSinceStartup;

    /// <inheritdoc />
    public float UnscaledTime => Time.unscaledTime;

    /// <inheritdoc />
    public long UtcNowTicks => DateTime.UtcNow.Ticks;

    /// <inheritdoc />
    public bool CanUseModNetworking(out string reason)
    {
        return RuntimeConnectionState.CanUseModNetworking(out reason);
    }
}
