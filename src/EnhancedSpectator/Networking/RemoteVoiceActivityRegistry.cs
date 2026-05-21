using System;
using System.Collections.Generic;
using EnhancedSpectator.Features.VoiceActivity;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Stores the last voice activity snapshots received from remote modded peers.
/// </summary>
public sealed class RemoteVoiceActivityRegistry
{
    private readonly Dictionary<ulong, Entry> _states = new Dictionary<ulong, Entry>();

    /// <summary>
    /// Registers, updates, or clears a remote voice activity snapshot.
    /// </summary>
    public void Update(VoiceActivityState state)
    {
        Update(state, DateTime.UtcNow.Ticks);
    }

    /// <summary>
    /// Registers, updates, or clears a remote voice activity snapshot with local receive time.
    /// </summary>
    public void Update(VoiceActivityState state, long receivedAtTicks)
    {
        if (_states.TryGetValue(state.ClientId, out Entry existing)
            && existing.State.TimestampTicks > state.TimestampTicks)
        {
            return;
        }

        if (!state.HasData)
        {
            _states.Remove(state.ClientId);
            return;
        }

        _states[state.ClientId] = new Entry(state, receivedAtTicks);
    }

    /// <summary>
    /// Attempts to get the last known voice activity for a remote peer.
    /// </summary>
    public bool TryGet(ulong clientId, out VoiceActivityState state)
    {
        if (TryGet(clientId, out state, out _))
        {
            return true;
        }

        state = VoiceActivityState.NoData;
        return false;
    }

    /// <summary>
    /// Attempts to get the last known voice activity and the local time it was received.
    /// </summary>
    public bool TryGet(ulong clientId, out VoiceActivityState state, out long receivedAtTicks)
    {
        if (_states.TryGetValue(clientId, out Entry storedEntry))
        {
            state = storedEntry.State;
            receivedAtTicks = storedEntry.ReceivedAtTicks;
            return true;
        }

        state = VoiceActivityState.NoData;
        receivedAtTicks = 0;
        return false;
    }

    /// <summary>
    /// Gets a copy of all stored remote voice activity snapshots.
    /// </summary>
    public List<VoiceActivityState> GetSnapshot()
    {
        List<VoiceActivityState> snapshot = new List<VoiceActivityState>(_states.Count);
        foreach (Entry entry in _states.Values)
        {
            snapshot.Add(entry.State);
        }

        return snapshot;
    }

    /// <summary>
    /// Removes one remote voice activity snapshot.
    /// </summary>
    public void Remove(ulong clientId)
    {
        _states.Remove(clientId);
    }

    /// <summary>
    /// Removes all remote voice activity snapshots.
    /// </summary>
    public void Clear()
    {
        _states.Clear();
    }

    private sealed class Entry
    {
        public Entry(VoiceActivityState state, long receivedAtTicks)
        {
            State = state;
            ReceivedAtTicks = receivedAtTicks;
        }

        public VoiceActivityState State { get; }

        public long ReceivedAtTicks { get; }
    }
}
