using UnityEngine;

namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Snapshot of one local placeholder visual.
/// </summary>
public readonly struct FloatingHeadVisualState
{
    /// <summary>
    /// Creates a floating-head visual state snapshot.
    /// </summary>
    public FloatingHeadVisualState(
        ulong spectatorClientId,
        ulong spectatorSlotId,
        bool isVisible,
        Vector3 position)
    {
        SpectatorClientId = spectatorClientId;
        SpectatorSlotId = spectatorSlotId;
        IsVisible = isVisible;
        Position = position;
    }

    /// <summary>
    /// Gets the remote spectator Netcode client id represented by this visual.
    /// </summary>
    public ulong SpectatorClientId { get; }

    /// <summary>
    /// Gets the remote spectator player slot id represented by this visual.
    /// </summary>
    public ulong SpectatorSlotId { get; }

    /// <summary>
    /// Gets whether the visual is currently visible.
    /// </summary>
    public bool IsVisible { get; }

    /// <summary>
    /// Gets the current world position.
    /// </summary>
    public Vector3 Position { get; }
}
