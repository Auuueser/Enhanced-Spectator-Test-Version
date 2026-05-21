using UnityEngine;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Resolves runtime detached-head visual template objects through confirmed game members.
/// </summary>
public sealed class LethalCompanyDetachedHeadVisualSourceAdapter : IGameDetachedHeadVisualSourceAdapter
{
    private const int GhostGirlDeathAnimationIndex = 1;

    /// <inheritdoc />
    public bool TryGetDetachedHeadVisualTemplate(out Transform? source)
    {
        StartOfRound round = StartOfRound.Instance;
        if (round == null
            || round.playerRagdolls == null
            || round.playerRagdolls.Count <= GhostGirlDeathAnimationIndex)
        {
            source = null;
            return false;
        }

        GameObject ragdollPrefab = round.playerRagdolls[GhostGirlDeathAnimationIndex];
        return TryGetSource(ragdollPrefab, out source);
    }

    private static bool TryGetSource(GameObject ragdollPrefab, out Transform? source)
    {
        if (ragdollPrefab == null)
        {
            source = null;
            return false;
        }

        DeadBodyInfo deadBody = ragdollPrefab.GetComponent<DeadBodyInfo>();
        if (deadBody == null)
        {
            deadBody = ragdollPrefab.GetComponentInChildren<DeadBodyInfo>(true);
        }

        return TryGetSource(deadBody, out source);
    }

    private static bool TryGetSource(DeadBodyInfo deadBody, out Transform? source)
    {
        if (deadBody == null
            || !deadBody.detachedHead
            || deadBody.detachedHeadObject == null
            || !HasSupportedVisualSource(deadBody.detachedHeadObject))
        {
            source = null;
            return false;
        }

        source = deadBody.detachedHeadObject;
        return true;
    }

    private static bool HasSupportedVisualSource(Transform source)
    {
        MeshRenderer renderer = source.GetComponentInChildren<MeshRenderer>(true);
        if (renderer == null)
        {
            return false;
        }

        MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();
        return meshFilter != null && meshFilter.sharedMesh != null;
    }
}
