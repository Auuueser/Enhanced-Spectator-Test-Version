using System.Collections.Generic;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Stores the last spectator camera pose received from remote modded peers.
/// </summary>
public sealed class RemoteSpectatorPoseRegistry
{
    private readonly Dictionary<ulong, SpectatorPoseState> _poses = new Dictionary<ulong, SpectatorPoseState>();

    /// <summary>
    /// Registers or updates a remote spectator pose.
    /// </summary>
    public void Update(SpectatorPoseState state)
    {
        if (!state.IsSpectating)
        {
            _poses.Remove(state.LocalClientId);
            return;
        }

        _poses[state.LocalClientId] = state;
    }

    /// <summary>
    /// Attempts to get the last known active pose for a remote peer.
    /// </summary>
    public bool TryGet(ulong clientId, out SpectatorPoseState state)
    {
        if (_poses.TryGetValue(clientId, out SpectatorPoseState storedState))
        {
            state = storedState;
            return true;
        }

        state = null!;
        return false;
    }

    /// <summary>
    /// Gets a copy of all stored remote poses.
    /// </summary>
    public List<SpectatorPoseState> GetSnapshot()
    {
        List<SpectatorPoseState> snapshot = new List<SpectatorPoseState>();
        foreach (SpectatorPoseState state in _poses.Values)
        {
            snapshot.Add(state);
        }

        return snapshot;
    }

    /// <summary>
    /// Removes one remote pose.
    /// </summary>
    public void Remove(ulong clientId)
    {
        _poses.Remove(clientId);
    }

    /// <summary>
    /// Removes all remote poses.
    /// </summary>
    public void Clear()
    {
        _poses.Clear();
    }
}
