using System.Collections.Generic;

namespace EnhancedSpectator.Features.ModelInspection;

/// <summary>
/// Captures runtime metadata for one player's dead-body detached-head candidate.
/// </summary>
public sealed class DeadBodyHeadSourceInspectionSnapshot
{
    /// <summary>
    /// Creates a detached-head source inspection snapshot.
    /// </summary>
    public DeadBodyHeadSourceInspectionSnapshot(
        ulong playerClientId,
        ulong actualClientId,
        string playerUsername,
        bool isLocalPlayer,
        bool isPlayerDead,
        bool isPlayerControlled,
        string playerRootName,
        string deadBodyPath,
        bool deadBodyDeactivated,
        bool detachedHead,
        int bodyPartsCount,
        string firstBodyPartPath,
        bool hasDetachedHeadObject,
        string detachedHeadObjectPath,
        string detachedHeadObjectName,
        string detachedHeadParentPath,
        bool detachedHeadActiveSelf,
        bool detachedHeadActiveInHierarchy,
        int rigidbodyCount,
        int colliderCount,
        int audioSourceCount,
        int networkObjectCount,
        IReadOnlyList<TransformInspectionInfo> detachedHeadTransforms,
        IReadOnlyList<DeadBodyHeadRendererInspectionInfo> detachedHeadRenderers)
    {
        PlayerClientId = playerClientId;
        ActualClientId = actualClientId;
        PlayerUsername = playerUsername;
        IsLocalPlayer = isLocalPlayer;
        IsPlayerDead = isPlayerDead;
        IsPlayerControlled = isPlayerControlled;
        PlayerRootName = playerRootName;
        DeadBodyPath = deadBodyPath;
        DeadBodyDeactivated = deadBodyDeactivated;
        DetachedHead = detachedHead;
        BodyPartsCount = bodyPartsCount;
        FirstBodyPartPath = firstBodyPartPath;
        HasDetachedHeadObject = hasDetachedHeadObject;
        DetachedHeadObjectPath = detachedHeadObjectPath;
        DetachedHeadObjectName = detachedHeadObjectName;
        DetachedHeadParentPath = detachedHeadParentPath;
        DetachedHeadActiveSelf = detachedHeadActiveSelf;
        DetachedHeadActiveInHierarchy = detachedHeadActiveInHierarchy;
        RigidbodyCount = rigidbodyCount;
        ColliderCount = colliderCount;
        AudioSourceCount = audioSourceCount;
        NetworkObjectCount = networkObjectCount;
        DetachedHeadTransforms = detachedHeadTransforms;
        DetachedHeadRenderers = detachedHeadRenderers;
    }

    /// <summary>
    /// Gets the player slot id.
    /// </summary>
    public ulong PlayerClientId { get; }

    /// <summary>
    /// Gets the Netcode client id.
    /// </summary>
    public ulong ActualClientId { get; }

    /// <summary>
    /// Gets the in-game player display name known locally.
    /// </summary>
    public string PlayerUsername { get; }

    /// <summary>
    /// Gets whether this is the local player.
    /// </summary>
    public bool IsLocalPlayer { get; }

    /// <summary>
    /// Gets whether the player is dead.
    /// </summary>
    public bool IsPlayerDead { get; }

    /// <summary>
    /// Gets whether the player is controlled.
    /// </summary>
    public bool IsPlayerControlled { get; }

    /// <summary>
    /// Gets the player root name.
    /// </summary>
    public string PlayerRootName { get; }

    /// <summary>
    /// Gets the dead body transform path.
    /// </summary>
    public string DeadBodyPath { get; }

    /// <summary>
    /// Gets whether the dead body is deactivated.
    /// </summary>
    public bool DeadBodyDeactivated { get; }

    /// <summary>
    /// Gets whether the dead body reports a detached head.
    /// </summary>
    public bool DetachedHead { get; }

    /// <summary>
    /// Gets the corpse body part count.
    /// </summary>
    public int BodyPartsCount { get; }

    /// <summary>
    /// Gets the first body part path when present.
    /// </summary>
    public string FirstBodyPartPath { get; }

    /// <summary>
    /// Gets whether a detached-head object exists.
    /// </summary>
    public bool HasDetachedHeadObject { get; }

    /// <summary>
    /// Gets the detached-head object path.
    /// </summary>
    public string DetachedHeadObjectPath { get; }

    /// <summary>
    /// Gets the detached-head object name.
    /// </summary>
    public string DetachedHeadObjectName { get; }

    /// <summary>
    /// Gets the detached-head parent path.
    /// </summary>
    public string DetachedHeadParentPath { get; }

    /// <summary>
    /// Gets whether the detached-head GameObject is active.
    /// </summary>
    public bool DetachedHeadActiveSelf { get; }

    /// <summary>
    /// Gets whether the detached-head object is active in hierarchy.
    /// </summary>
    public bool DetachedHeadActiveInHierarchy { get; }

    /// <summary>
    /// Gets detached-head rigidbody count.
    /// </summary>
    public int RigidbodyCount { get; }

    /// <summary>
    /// Gets detached-head collider count.
    /// </summary>
    public int ColliderCount { get; }

    /// <summary>
    /// Gets detached-head audio source count.
    /// </summary>
    public int AudioSourceCount { get; }

    /// <summary>
    /// Gets detached-head network object count.
    /// </summary>
    public int NetworkObjectCount { get; }

    /// <summary>
    /// Gets transform paths below the detached-head object.
    /// </summary>
    public IReadOnlyList<TransformInspectionInfo> DetachedHeadTransforms { get; }

    /// <summary>
    /// Gets detached-head renderer metadata.
    /// </summary>
    public IReadOnlyList<DeadBodyHeadRendererInspectionInfo> DetachedHeadRenderers { get; }
}
