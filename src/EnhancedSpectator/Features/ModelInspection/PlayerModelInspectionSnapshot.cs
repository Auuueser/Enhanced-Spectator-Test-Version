using System.Collections.Generic;

namespace EnhancedSpectator.Features.ModelInspection;

/// <summary>
/// Captures runtime metadata for one player model hierarchy.
/// </summary>
public sealed class PlayerModelInspectionSnapshot
{
    /// <summary>
    /// Creates a player model inspection snapshot.
    /// </summary>
    public PlayerModelInspectionSnapshot(
        ulong playerClientId,
        ulong actualClientId,
        bool isPlayerDead,
        bool isPlayerControlled,
        bool isLocalPlayer,
        string rootName,
        string thisPlayerBodyPath,
        string meshContainerPath,
        string lowerSpinePath,
        string upperSpinePath,
        string playerGlobalHeadPath,
        string playerEyePath,
        string headCostumeContainerPath,
        string headCostumeContainerLocalPath,
        bool hasThisPlayerModel,
        bool hasThisPlayerModelLod1,
        bool hasThisPlayerModelLod2,
        bool hasThisPlayerModelArms,
        IReadOnlyList<TransformInspectionInfo> headRelatedTransforms,
        IReadOnlyList<RendererInspectionInfo> skinnedMeshRenderers)
    {
        PlayerClientId = playerClientId;
        ActualClientId = actualClientId;
        IsPlayerDead = isPlayerDead;
        IsPlayerControlled = isPlayerControlled;
        IsLocalPlayer = isLocalPlayer;
        RootName = rootName;
        ThisPlayerBodyPath = thisPlayerBodyPath;
        MeshContainerPath = meshContainerPath;
        LowerSpinePath = lowerSpinePath;
        UpperSpinePath = upperSpinePath;
        PlayerGlobalHeadPath = playerGlobalHeadPath;
        PlayerEyePath = playerEyePath;
        HeadCostumeContainerPath = headCostumeContainerPath;
        HeadCostumeContainerLocalPath = headCostumeContainerLocalPath;
        HasThisPlayerModel = hasThisPlayerModel;
        HasThisPlayerModelLod1 = hasThisPlayerModelLod1;
        HasThisPlayerModelLod2 = hasThisPlayerModelLod2;
        HasThisPlayerModelArms = hasThisPlayerModelArms;
        HeadRelatedTransforms = headRelatedTransforms;
        SkinnedMeshRenderers = skinnedMeshRenderers;
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
    /// Gets whether the player is dead.
    /// </summary>
    public bool IsPlayerDead { get; }

    /// <summary>
    /// Gets whether the player is currently controlled.
    /// </summary>
    public bool IsPlayerControlled { get; }

    /// <summary>
    /// Gets whether this snapshot is for the local player.
    /// </summary>
    public bool IsLocalPlayer { get; }

    /// <summary>
    /// Gets the root transform name.
    /// </summary>
    public string RootName { get; }

    /// <summary>
    /// Gets the `thisPlayerBody` path.
    /// </summary>
    public string ThisPlayerBodyPath { get; }

    /// <summary>
    /// Gets the `meshContainer` path.
    /// </summary>
    public string MeshContainerPath { get; }

    /// <summary>
    /// Gets the `lowerSpine` path.
    /// </summary>
    public string LowerSpinePath { get; }

    /// <summary>
    /// Gets the `upperSpine` path.
    /// </summary>
    public string UpperSpinePath { get; }

    /// <summary>
    /// Gets the `playerGlobalHead` path.
    /// </summary>
    public string PlayerGlobalHeadPath { get; }

    /// <summary>
    /// Gets the `playerEye` path.
    /// </summary>
    public string PlayerEyePath { get; }

    /// <summary>
    /// Gets the `headCostumeContainer` path.
    /// </summary>
    public string HeadCostumeContainerPath { get; }

    /// <summary>
    /// Gets the `headCostumeContainerLocal` path.
    /// </summary>
    public string HeadCostumeContainerLocalPath { get; }

    /// <summary>
    /// Gets whether `thisPlayerModel` exists.
    /// </summary>
    public bool HasThisPlayerModel { get; }

    /// <summary>
    /// Gets whether `thisPlayerModelLOD1` exists.
    /// </summary>
    public bool HasThisPlayerModelLod1 { get; }

    /// <summary>
    /// Gets whether `thisPlayerModelLOD2` exists.
    /// </summary>
    public bool HasThisPlayerModelLod2 { get; }

    /// <summary>
    /// Gets whether `thisPlayerModelArms` exists.
    /// </summary>
    public bool HasThisPlayerModelArms { get; }

    /// <summary>
    /// Gets head-related transform path candidates.
    /// </summary>
    public IReadOnlyList<TransformInspectionInfo> HeadRelatedTransforms { get; }

    /// <summary>
    /// Gets skinned mesh renderer metadata.
    /// </summary>
    public IReadOnlyList<RendererInspectionInfo> SkinnedMeshRenderers { get; }
}
