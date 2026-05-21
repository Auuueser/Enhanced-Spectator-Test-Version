using System.Collections.Generic;

namespace EnhancedSpectator.Networking;

/// <summary>
/// Pure relay decision helpers for host-mediated spectator state fan-out.
/// </summary>
public static class HostRelayPlanner
{
    /// <summary>
    /// Gets direct compatible peers that should receive a host relay for an origin peer.
    /// </summary>
    public static IReadOnlyList<ulong> GetRelayRecipients(
        RemotePeerRegistry registry,
        ulong hostClientId,
        ulong originClientId)
    {
        List<ulong> recipients = registry.GetSpectatorTargetSyncPeerIds(hostClientId);
        for (int index = recipients.Count - 1; index >= 0; index--)
        {
            ulong recipient = recipients[index];
            if (recipient == hostClientId || recipient == originClientId)
            {
                recipients.RemoveAt(index);
            }
        }

        return recipients;
    }

    /// <summary>
    /// Gets direct compatible peers that should receive a host relay for origin voice activity.
    /// </summary>
    public static IReadOnlyList<ulong> GetVoiceActivityRelayRecipients(
        RemotePeerRegistry registry,
        ulong hostClientId,
        ulong originClientId)
    {
        List<ulong> recipients = registry.GetVoiceActivitySyncPeerIds(hostClientId);
        for (int index = recipients.Count - 1; index >= 0; index--)
        {
            ulong recipient = recipients[index];
            if (recipient == hostClientId || recipient == originClientId)
            {
                recipients.RemoveAt(index);
            }
        }

        return recipients;
    }

    /// <summary>
    /// Gets whether a spectator state message should be accepted from a sender for the given origin.
    /// </summary>
    public static bool CanAcceptSpectatorState(
        bool isHost,
        ulong localClientId,
        ulong serverClientId,
        ulong senderClientId,
        ulong originClientId,
        RemotePeerRegistry registry,
        out string reason)
    {
        if (originClientId == localClientId)
        {
            reason = "origin is local client";
            return false;
        }

        if (!registry.IsSpectatorTargetSyncPeer(originClientId))
        {
            reason = "origin is not a compatible spectator sync peer";
            return false;
        }

        if (isHost)
        {
            if (senderClientId == originClientId)
            {
                reason = string.Empty;
                return true;
            }

            reason = "host rejected state where sender does not match origin";
            return false;
        }

        if (senderClientId == originClientId && !registry.IsRelayedPeer(originClientId))
        {
            reason = string.Empty;
            return true;
        }

        if (senderClientId == serverClientId && registry.IsRelayedPeer(originClientId))
        {
            reason = string.Empty;
            return true;
        }

        reason = "client rejected non-host relayed foreign origin";
        return false;
    }
}
