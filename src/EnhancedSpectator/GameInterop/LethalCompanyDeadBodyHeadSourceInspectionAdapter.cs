using System.Collections.Generic;
using EnhancedSpectator.Features.ModelInspection;
using GameNetcodeStuff;
using Unity.Netcode;
using UnityEngine;

namespace EnhancedSpectator.GameInterop;

/// <summary>
/// Reads runtime dead-body detached-head metadata through confirmed game members.
/// </summary>
public sealed class LethalCompanyDeadBodyHeadSourceInspectionAdapter : IGameDeadBodyHeadSourceInspectionAdapter
{
    /// <inheritdoc />
    public bool TryGetDeadBodyHeadSourceInspectionSnapshots(
        int maxTransformDepth,
        bool includeRendererBounds,
        bool includeMaterials,
        out IReadOnlyList<DeadBodyHeadSourceInspectionSnapshot> snapshots)
    {
        List<DeadBodyHeadSourceInspectionSnapshot> result = new List<DeadBodyHeadSourceInspectionSnapshot>();
        snapshots = result.AsReadOnly();

        StartOfRound round = StartOfRound.Instance;
        if (round == null || round.allPlayerScripts == null)
        {
            return false;
        }

        PlayerControllerB localPlayer = round.localPlayerController;
        foreach (PlayerControllerB player in round.allPlayerScripts)
        {
            if (player == null || player.deadBody == null)
            {
                continue;
            }

            bool isLocalPlayer = localPlayer != null && player == localPlayer;
            result.Add(BuildSnapshot(
                player,
                isLocalPlayer,
                Mathf.Max(0, maxTransformDepth),
                includeRendererBounds,
                includeMaterials));
        }

        return result.Count > 0;
    }

    private static DeadBodyHeadSourceInspectionSnapshot BuildSnapshot(
        PlayerControllerB player,
        bool isLocalPlayer,
        int maxTransformDepth,
        bool includeRendererBounds,
        bool includeMaterials)
    {
        DeadBodyInfo deadBody = player.deadBody;
        Transform deadBodyTransform = deadBody.transform;
        Transform? detachedHeadObject = deadBody.detachedHeadObject;

        if (detachedHeadObject == null)
        {
            return new DeadBodyHeadSourceInspectionSnapshot(
                player.playerClientId,
                player.actualClientId,
                string.IsNullOrWhiteSpace(player.playerUsername) ? "null" : player.playerUsername,
                isLocalPlayer,
                player.isPlayerDead,
                player.isPlayerControlled,
                SafeName(player.transform),
                GetPath(null, deadBodyTransform),
                deadBody.deactivated,
                deadBody.detachedHead,
                deadBody.bodyParts != null ? deadBody.bodyParts.Length : 0,
                GetFirstBodyPartPath(deadBody),
                false,
                "null",
                "null",
                "null",
                false,
                false,
                0,
                0,
                0,
                0,
                new List<TransformInspectionInfo>().AsReadOnly(),
                new List<DeadBodyHeadRendererInspectionInfo>().AsReadOnly());
        }

        return new DeadBodyHeadSourceInspectionSnapshot(
            player.playerClientId,
            player.actualClientId,
            string.IsNullOrWhiteSpace(player.playerUsername) ? "null" : player.playerUsername,
            isLocalPlayer,
            player.isPlayerDead,
            player.isPlayerControlled,
            SafeName(player.transform),
            GetPath(null, deadBodyTransform),
            deadBody.deactivated,
            deadBody.detachedHead,
            deadBody.bodyParts != null ? deadBody.bodyParts.Length : 0,
            GetFirstBodyPartPath(deadBody),
            true,
            GetPath(null, detachedHeadObject),
            SafeName(detachedHeadObject),
            GetPath(null, detachedHeadObject.parent),
            detachedHeadObject.gameObject.activeSelf,
            detachedHeadObject.gameObject.activeInHierarchy,
            detachedHeadObject.GetComponentsInChildren<Rigidbody>(true).Length,
            detachedHeadObject.GetComponentsInChildren<Collider>(true).Length,
            detachedHeadObject.GetComponentsInChildren<AudioSource>(true).Length,
            detachedHeadObject.GetComponentsInChildren<NetworkObject>(true).Length,
            CollectTransforms(detachedHeadObject, maxTransformDepth).AsReadOnly(),
            CollectRenderers(detachedHeadObject, includeRendererBounds, includeMaterials).AsReadOnly());
    }

    private static string GetFirstBodyPartPath(DeadBodyInfo deadBody)
    {
        if (deadBody.bodyParts == null || deadBody.bodyParts.Length == 0 || deadBody.bodyParts[0] == null)
        {
            return "null";
        }

        return GetPath(null, deadBody.bodyParts[0].transform);
    }

    private static List<TransformInspectionInfo> CollectTransforms(Transform root, int maxTransformDepth)
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

        transforms.Add(new TransformInspectionInfo(GetPath(root, current), SafeName(current), depth));
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

    private static List<DeadBodyHeadRendererInspectionInfo> CollectRenderers(
        Transform root,
        bool includeRendererBounds,
        bool includeMaterials)
    {
        List<DeadBodyHeadRendererInspectionInfo> renderers = new List<DeadBodyHeadRendererInspectionInfo>();
        Renderer[] rendererComponents = root.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in rendererComponents)
        {
            if (renderer == null)
            {
                continue;
            }

            string rendererType = "Renderer";
            Mesh? mesh = null;
            int bonesCount = 0;
            string rootBoneName = "null";

            if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
            {
                rendererType = "SkinnedMeshRenderer";
                mesh = skinnedMeshRenderer.sharedMesh;
                bonesCount = skinnedMeshRenderer.bones != null ? skinnedMeshRenderer.bones.Length : 0;
                rootBoneName = skinnedMeshRenderer.rootBone != null ? skinnedMeshRenderer.rootBone.name : "null";
            }
            else if (renderer is MeshRenderer)
            {
                rendererType = "MeshRenderer";
                MeshFilter? meshFilter = renderer.GetComponent<MeshFilter>();
                mesh = meshFilter != null ? meshFilter.sharedMesh : null;
            }

            Vector3? boundsCenter = includeRendererBounds ? renderer.bounds.center : null;
            Vector3? boundsSize = includeRendererBounds ? renderer.bounds.size : null;
            renderers.Add(new DeadBodyHeadRendererInspectionInfo(
                GetPath(root, renderer.transform),
                SafeName(renderer.transform),
                rendererType,
                renderer.enabled,
                renderer.forceRenderingOff,
                mesh != null ? mesh.name : "null",
                mesh != null ? mesh.subMeshCount : 0,
                bonesCount,
                rootBoneName,
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

    private static string GetPath(Transform? root, Transform? target)
    {
        if (target == null)
        {
            return "null";
        }

        List<string> names = new List<string>();
        Transform current = target;
        while (current != null)
        {
            names.Insert(0, SafeName(current));
            if (root != null && current == root)
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
