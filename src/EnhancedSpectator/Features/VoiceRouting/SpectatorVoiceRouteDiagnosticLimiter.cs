using System;
using System.Collections.Generic;

namespace EnhancedSpectator.Features.VoiceRouting;

/// <summary>
/// Rate-limits spectator voice route diagnostics while still surfacing meaningful playback changes.
/// </summary>
public sealed class SpectatorVoiceRouteDiagnosticLimiter
{
    private const float DistanceDeltaThreshold = 2.0f;
    private const float VolumeDeltaThreshold = 0.10f;
    private const float SpatialBlendDeltaThreshold = 0.05f;
    private readonly Dictionary<ulong, LastDiagnosticState> _lastStates = new Dictionary<ulong, LastDiagnosticState>();
    private readonly int _minimumIntervalFrames;

    /// <summary>
    /// Creates a route diagnostic limiter.
    /// </summary>
    public SpectatorVoiceRouteDiagnosticLimiter(int minimumIntervalFrames = 120)
    {
        _minimumIntervalFrames = Math.Max(1, minimumIntervalFrames);
    }

    /// <summary>
    /// Gets whether a route apply diagnostic should be emitted for the current playback state.
    /// </summary>
    public bool ShouldLog(
        ulong spectatorClientId,
        int frame,
        bool poseAvailable,
        bool fallbackTo2D,
        float distance,
        float finalVolume,
        float spatialBlend,
        bool set2D)
    {
        LastDiagnosticState next = new LastDiagnosticState(
            frame,
            poseAvailable,
            fallbackTo2D,
            distance,
            finalVolume,
            spatialBlend,
            set2D);

        if (!_lastStates.TryGetValue(spectatorClientId, out LastDiagnosticState previous)
            || previous.IsMeaningfullyDifferent(next, _minimumIntervalFrames))
        {
            _lastStates[spectatorClientId] = next;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Clears stored diagnostic state for a route.
    /// </summary>
    public void Clear(ulong spectatorClientId)
    {
        _lastStates.Remove(spectatorClientId);
    }

    private readonly struct LastDiagnosticState
    {
        public LastDiagnosticState(
            int frame,
            bool poseAvailable,
            bool fallbackTo2D,
            float distance,
            float finalVolume,
            float spatialBlend,
            bool set2D)
        {
            Frame = frame;
            PoseAvailable = poseAvailable;
            FallbackTo2D = fallbackTo2D;
            Distance = distance;
            FinalVolume = finalVolume;
            SpatialBlend = spatialBlend;
            Set2D = set2D;
        }

        public int Frame { get; }

        public bool PoseAvailable { get; }

        public bool FallbackTo2D { get; }

        public float Distance { get; }

        public float FinalVolume { get; }

        public float SpatialBlend { get; }

        public bool Set2D { get; }

        public bool IsMeaningfullyDifferent(LastDiagnosticState other, int minimumIntervalFrames)
        {
            return other.Frame - Frame >= minimumIntervalFrames
                || PoseAvailable != other.PoseAvailable
                || FallbackTo2D != other.FallbackTo2D
                || Set2D != other.Set2D
                || MathF.Abs(SpatialBlend - other.SpatialBlend) >= SpatialBlendDeltaThreshold
                || MathF.Abs(FinalVolume - other.FinalVolume) >= VolumeDeltaThreshold
                || MathF.Abs(Distance - other.Distance) >= DistanceDeltaThreshold;
        }
    }
}
