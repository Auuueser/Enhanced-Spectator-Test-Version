using System;
using System.Collections.Generic;
using EnhancedSpectator.Features.VoiceActivity;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Rate-limits voice activity diagnostics while preserving state changes useful for testing.
/// </summary>
public sealed class VoiceActivityDebugLimiter
{
    private const float AmplitudeDeltaThreshold = 0.20f;
    private const float VolumeDeltaThreshold = 0.20f;

    private readonly Dictionary<DebugKey, LastDebugState> _lastStates = new Dictionary<DebugKey, LastDebugState>();
    private readonly int _minimumIntervalFrames;

    /// <summary>
    /// Creates a voice activity debug limiter.
    /// </summary>
    public VoiceActivityDebugLimiter(int minimumIntervalFrames = 180)
    {
        _minimumIntervalFrames = Math.Max(1, minimumIntervalFrames);
    }

    /// <summary>
    /// Gets whether a diagnostic should be emitted for the current voice state.
    /// </summary>
    public bool ShouldLog(string category, ulong peerId, int frame, VoiceActivityState state, bool isRelayed)
    {
        DebugKey key = new DebugKey(category, peerId);
        LastDebugState next = new LastDebugState(frame, state, isRelayed);
        if (!_lastStates.TryGetValue(key, out LastDebugState previous)
            || previous.IsMeaningfullyDifferent(next, _minimumIntervalFrames))
        {
            _lastStates[key] = next;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Clears all stored limiter state.
    /// </summary>
    public void Clear()
    {
        _lastStates.Clear();
    }

    private readonly struct DebugKey : IEquatable<DebugKey>
    {
        public DebugKey(string category, ulong peerId)
        {
            Category = category ?? string.Empty;
            PeerId = peerId;
        }

        public string Category { get; }

        public ulong PeerId { get; }

        public bool Equals(DebugKey other)
        {
            return PeerId == other.PeerId && string.Equals(Category, other.Category, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            return obj is DebugKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Category != null ? Category.GetHashCode() : 0) * 397) ^ PeerId.GetHashCode();
            }
        }
    }

    private readonly struct LastDebugState
    {
        public LastDebugState(int frame, VoiceActivityState state, bool isRelayed)
        {
            Frame = frame;
            HasData = state.HasData;
            IsSpeaking = state.IsSpeaking;
            Amplitude = state.Amplitude;
            Volume = state.Volume;
            IsRelayed = isRelayed;
        }

        public int Frame { get; }

        public bool HasData { get; }

        public bool IsSpeaking { get; }

        public float Amplitude { get; }

        public float Volume { get; }

        public bool IsRelayed { get; }

        public bool IsMeaningfullyDifferent(LastDebugState other, int minimumIntervalFrames)
        {
            return other.Frame - Frame >= minimumIntervalFrames
                || HasData != other.HasData
                || IsSpeaking != other.IsSpeaking
                || IsRelayed != other.IsRelayed
                || MathF.Abs(Amplitude - other.Amplitude) >= AmplitudeDeltaThreshold
                || MathF.Abs(Volume - other.Volume) >= VolumeDeltaThreshold;
        }
    }
}
