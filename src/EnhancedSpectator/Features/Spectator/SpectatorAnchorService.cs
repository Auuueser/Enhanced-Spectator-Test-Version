using EnhancedSpectator.GameInterop;
using UnityEngine;

namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Tracks the current spectator anchor and detects target changes.
/// </summary>
public sealed class SpectatorAnchorService
{
    private string? _targetKey;

    /// <summary>
    /// Attempts to update the active anchor from a spectator snapshot.
    /// </summary>
    public bool TryUpdate(GameSpectatorSnapshot snapshot, out Transform? anchor, out bool targetChanged)
    {
        anchor = snapshot.Anchor;
        targetChanged = false;

        if (anchor == null || !snapshot.HasSpectatedTarget)
        {
            Clear();
            return false;
        }

        string nextKey = CreateTargetKey(snapshot, anchor);
        targetChanged = _targetKey != null && _targetKey != nextKey;
        _targetKey = nextKey;
        return true;
    }

    /// <summary>
    /// Clears the remembered anchor identity.
    /// </summary>
    public void Clear()
    {
        _targetKey = null;
    }

    private static string CreateTargetKey(GameSpectatorSnapshot snapshot, Transform anchor)
    {
        if (snapshot.SpectatedPlayerSlotId.HasValue)
        {
            return snapshot.SpectatedPlayerActualClientId.HasValue
                ? $"{snapshot.SpectatedPlayerSlotId.Value}:{snapshot.SpectatedPlayerActualClientId.Value}"
                : snapshot.SpectatedPlayerSlotId.Value.ToString();
        }

        return $"anchor:{anchor.GetInstanceID()}";
    }
}
