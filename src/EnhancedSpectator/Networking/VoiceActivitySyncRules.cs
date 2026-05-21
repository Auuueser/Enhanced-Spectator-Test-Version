using System;
using EnhancedSpectator.Features.VoiceActivity;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Pure rules for deciding whether a voice activity snapshot has changed enough to send.
/// </summary>
public static class VoiceActivitySyncRules
{
    private const float AmplitudeEpsilon = 0.02f;
    private const float VolumeEpsilon = 0.02f;

    /// <summary>
    /// Gets whether two voice activity snapshots are effectively equivalent for network throttling.
    /// </summary>
    public static bool ApproximatelyEquals(VoiceActivityState? left, VoiceActivityState? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left == null || right == null)
        {
            return false;
        }

        return left.HasData == right.HasData
            && left.IsSpeaking == right.IsSpeaking
            && left.ClientId == right.ClientId
            && left.SlotId == right.SlotId
            && Math.Abs(left.Amplitude - right.Amplitude) <= AmplitudeEpsilon
            && Math.Abs(left.Volume - right.Volume) <= VolumeEpsilon;
    }

    /// <summary>
    /// Gets whether a received voice activity snapshot is still fresh enough to drive visuals.
    /// </summary>
    public static bool IsFresh(long receivedAtTicks, long nowTicks, long staleTicks)
    {
        if (receivedAtTicks <= 0)
        {
            return false;
        }

        if (staleTicks <= 0)
        {
            return true;
        }

        long ageTicks = nowTicks - receivedAtTicks;
        return ageTicks >= 0 && ageTicks <= staleTicks;
    }
}
