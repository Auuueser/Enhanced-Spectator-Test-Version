using System;
using System.Collections.Generic;
using EnhancedSpectator.Features.ModelInspection;
using GameNetcodeStuff;
using UnityEngine;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Reads runtime player model hierarchy metadata through confirmed game members.
/// </summary>
public sealed class LethalCompanyPlayerModelInspectionAdapter : IGamePlayerModelInspectionAdapter
{
    private static readonly string[] HeadRelatedNameTokens =
    {
        "head",
        "skull",
        "neck",
        "spine",
        "visor",
        "helmet",
        "costume",
        "metarig",
        "rig",
    };

    /// <inheritdoc />
    public bool TryGetPlayerModelInspectionSnapshots(
        bool includeLocalPlayer,
        bool includeRemotePlayers,
        int maxTransformDepth,
        bool includeRendererBounds,
        bool includeMaterials,
        out IReadOnlyList<PlayerModelInspectionSnapshot> snapshots)
    {
        List<PlayerModelInspectionSnapshot> result = new List<PlayerModelInspectionSnapshot>();
        snapshots = result.AsReadOnly();

        StartOfRound round = StartOfRound.Instance;
        if (round == null || round.allPlayerScripts == null)
        {
            return false;
        }

        PlayerControllerB localPlayer = round.localPlayerController;
        foreach (PlayerControllerB player in round.allPlayerScripts)
        {
            if (player == null)
            {
                continue;
            }

            bool isLocalPlayer = localPlayer != null && player == localPlayer;
            if ((isLocalPlayer && !includeLocalPlayer) || (!isLocalPlayer && !includeRemotePlayers))
            {
                continue;
            }

            result.Add(BuildSnapshot(
                player,
                isLocalPlayer,
                Mathf.Max(0, maxTransformDepth),
                includeRendererBounds,
                includeMaterials));
        }

        return result.Count > 0;
    }

    private static PlayerModelInspectionSnapshot BuildSnapshot(
        PlayerControllerB player,
        bool isLocalPlayer,
        int maxTransformDepth,
        bool includeRendererBounds,
        bool includeMaterials)
    {
        Transform root = player.transform;
        return new PlayerModelInspectionSnapshot(
            player.playerClientId,
            player.actualClientId,
            player.isPlayerDead,
            player.isPlayerControlled,
            isLocalPlayer,
            SafeName(root),
            GetPath(root, player.thisPlayerBody),
            GetPath(root, player.meshContainer),
            GetPath(root, player.lowerSpine),
            GetPath(root, player.upperSpine),
            GetPath(root, player.playerGlobalHead),
            GetPath(root, player.playerEye),
            GetPath(root, player.headCostumeContainer),
            GetPath(root, player.headCostumeContainerLocal),
            player.thisPlayerModel != null,
            player.thisPlayerModelLOD1 != null,
            player.thisPlayerModelLOD2 != null,
            player.thisPlayerModelArms != null,
            CollectHeadRelatedTransforms(root, maxTransformDepth).AsReadOnly(),
            CollectSkinnedMeshRenderers(root, includeRendererBounds, includeMaterials).AsReadOnly());
    }

    private static List<TransformInspectionInfo> CollectHeadRelatedTransforms(Transform root, int maxTransformDepth)
    {
        List<TransformInspectionInfo> transforms = new List<TransformInspectionInfo>();
        VisitTransform(root, root, 0, maxTransformDepth, transforms);
        return transforms;
    }

    private static void VisitTransform(
        Transform root,
        Transform current,
        int depth,
        int maxDepth,
        List<TransformInspectionInfo> transforms)
    {
        if (current == null || depth > maxDepth)
        {
            return;
        }

        if (IsHeadRelatedName(current.name))
        {
            transforms.Add(new TransformInspectionInfo(GetPath(root, current), SafeName(current), depth));
        }

        if (depth == maxDepth)
        {
            return;
        }

        for (int index = 0; index < current.childCount; index++)
        {
            Transform child = current.GetChild(index);
            if (child != null)
            {
                VisitTransform(root, child, depth + 1, maxDepth, transforms);
            }
        }
    }

    private static List<RendererInspectionInfo> CollectSkinnedMeshRenderers(
        Transform root,
        bool includeRendererBounds,
        bool includeMaterials)
    {
        List<RendererInspectionInfo> renderers = new List<RendererInspectionInfo>();
        if (root == null)
        {
            return renderers;
        }

        SkinnedMeshRenderer[] skinnedMeshRenderers = root.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
        {
            if (renderer == null)
            {
                continue;
            }

            Mesh mesh = renderer.sharedMesh;
            Transform rootBone = renderer.rootBone;
            Vector3? boundsCenter = includeRendererBounds ? renderer.bounds.center : null;
            Vector3? boundsSize = includeRendererBounds ? renderer.bounds.size : null;
            renderers.Add(new RendererInspectionInfo(
                GetPath(root, renderer.transform),
                SafeName(renderer.transform),
                renderer.enabled,
                mesh != null ? mesh.name : "null",
                mesh != null ? mesh.subMeshCount : 0,
                renderer.bones != null ? renderer.bones.Length : 0,
                rootBone != null ? rootBone.name : "null",
                boundsCenter,
                boundsSize,
                renderer.sharedMaterials != null ? renderer.sharedMaterials.Length : 0,
                includeMaterials ? GetMaterialNames(renderer).AsReadOnly() : new List<string>().AsReadOnly()));
        }

        return renderers;
    }

    private static List<string> GetMaterialNames(Renderer renderer)
    {
        List<string> names = new List<string>();
        Material[] materials = renderer.sharedMaterials;
        if (materials == null)
        {
            return names;
        }

        foreach (Material material in materials)
        {
            names.Add(material != null ? material.name : "null");
        }

        return names;
    }

    private static bool IsHeadRelatedName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        foreach (string token in HeadRelatedNameTokens)
        {
            if (name.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
        }

        return false;
    }

    private static string GetPath(Transform root, Transform? target)
    {
        if (root == null || target == null)
        {
            return "null";
        }

        List<string> names = new List<string>();
        Transform? current = target;
        while (current != null)
        {
            names.Insert(0, SafeName(current));
            if (current == root)
            {
                return string.Join("/", names);
            }

            current = current.parent;
        }

        return string.Join("/", names);
    }

    private static string SafeName(Transform? transform)
    {
        return transform != null ? transform.name : "null";
    }
}
