using System;
using System.Collections.Generic;
using System.Text;
using EnhancedSpectator.Config;
using EnhancedSpectator.GameInterop;
using EnhancedSpectator.Logging;
using EnhancedSpectator.Runtime;
using UnityEngine;

namespace EnhancedSpectator.Features.ModelInspection;

/// <summary>
/// Runs one-shot inspections for runtime dead-body detached-head source candidates.
/// </summary>
public sealed class DeadBodyHeadSourceInspectionService
{
    private readonly EnhancedSpectatorConfig _config;
    private readonly IGameDeadBodyHeadSourceInspectionAdapter _adapter;

    /// <summary>
    /// Creates a detached-head source inspection service.
    /// </summary>
    public DeadBodyHeadSourceInspectionService(
        EnhancedSpectatorConfig config,
        IGameDeadBodyHeadSourceInspectionAdapter adapter)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
    }

    /// <summary>
    /// Runs one inspection pass and writes findings to the BepInEx log.
    /// </summary>
    public void InspectOnce()
    {
        try
        {
            if (!RuntimeConnectionState.CanUseModNetworking(out string reason))
            {
                ModLog.Debug($"Head source inspection skipped because runtime state is unsafe: {reason}.");
                return;
            }

            if (!_adapter.TryGetDeadBodyHeadSourceInspectionSnapshots(
                _config.RuntimeHeadSourceMaxTransformDepth.Value,
                _config.RuntimeHeadSourceIncludeRendererBounds.Value,
                _config.RuntimeHeadSourceIncludeMaterials.Value,
                out IReadOnlyList<DeadBodyHeadSourceInspectionSnapshot> snapshots))
            {
                ModLog.Info("Enhanced Spectator head source inspection found no current player dead bodies.");
                return;
            }

            ModLog.Info(BuildInspectionLog(snapshots));
        }
        catch (Exception ex)
        {
            ModLog.Error($"Enhanced Spectator head source inspection failed: {ex}");
        }
    }

    private string BuildInspectionLog(IReadOnlyList<DeadBodyHeadSourceInspectionSnapshot> snapshots)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Enhanced Spectator runtime head source inspection");
        builder.AppendLine($"SnapshotCount={snapshots.Count}");
        builder.AppendLine($"MaxTransformDepth={_config.RuntimeHeadSourceMaxTransformDepth.Value}");
        builder.AppendLine($"IncludeRendererBounds={_config.RuntimeHeadSourceIncludeRendererBounds.Value}");
        builder.AppendLine($"IncludeMaterials={_config.RuntimeHeadSourceIncludeMaterials.Value}");

        foreach (DeadBodyHeadSourceInspectionSnapshot snapshot in snapshots)
        {
            AppendSnapshot(builder, snapshot);
        }

        return builder.ToString();
    }

    private static void AppendSnapshot(StringBuilder builder, DeadBodyHeadSourceInspectionSnapshot snapshot)
    {
        builder.AppendLine("--- DeadBody Head Source ---");
        builder.AppendLine(
            $"playerClientId={snapshot.PlayerClientId}, actualClientId={snapshot.ActualClientId}, name={snapshot.PlayerUsername}, local={snapshot.IsLocalPlayer}, controlled={snapshot.IsPlayerControlled}, dead={snapshot.IsPlayerDead}, root={snapshot.PlayerRootName}");
        builder.AppendLine($"deadBody={snapshot.DeadBodyPath}, deactivated={snapshot.DeadBodyDeactivated}");
        builder.AppendLine(
            $"detachedHead={snapshot.DetachedHead}, hasDetachedHeadObject={snapshot.HasDetachedHeadObject}, detachedHeadObject={snapshot.DetachedHeadObjectPath}");
        builder.AppendLine(
            $"detachedHeadName={snapshot.DetachedHeadObjectName}, parent={snapshot.DetachedHeadParentPath}, activeSelf={snapshot.DetachedHeadActiveSelf}, activeInHierarchy={snapshot.DetachedHeadActiveInHierarchy}");
        builder.AppendLine($"bodyParts={snapshot.BodyPartsCount}, bodyParts[0]={snapshot.FirstBodyPartPath}");
        builder.AppendLine(
            $"components: rigidbodies={snapshot.RigidbodyCount}, colliders={snapshot.ColliderCount}, audioSources={snapshot.AudioSourceCount}, networkObjects={snapshot.NetworkObjectCount}");

        builder.AppendLine($"detachedHeadTransforms={snapshot.DetachedHeadTransforms.Count}");
        foreach (TransformInspectionInfo transform in snapshot.DetachedHeadTransforms)
        {
            builder.AppendLine($"  transform depth={transform.Depth} name={transform.Name} path={transform.Path}");
        }

        builder.AppendLine($"detachedHeadRenderers={snapshot.DetachedHeadRenderers.Count}");
        foreach (DeadBodyHeadRendererInspectionInfo renderer in snapshot.DetachedHeadRenderers)
        {
            builder.Append(
                $"  renderer type={renderer.RendererType} name={renderer.Name} path={renderer.Path} enabled={renderer.Enabled} forceOff={renderer.ForceRenderingOff} mesh={renderer.SharedMeshName} subMeshes={renderer.SubMeshCount} bones={renderer.BonesCount} rootBone={renderer.RootBoneName} materialSlots={renderer.MaterialSlotCount}");

            if (renderer.BoundsCenter.HasValue && renderer.BoundsSize.HasValue)
            {
                builder.Append(
                    $" boundsCenter={FormatVector(renderer.BoundsCenter.Value)} boundsSize={FormatVector(renderer.BoundsSize.Value)}");
            }

            if (renderer.MaterialNames.Count > 0)
            {
                builder.Append($" materials=[{string.Join(", ", renderer.MaterialNames)}]");
            }

            builder.AppendLine();
        }
    }

    private static string FormatVector(Vector3 vector)
    {
        return $"({vector.x:0.00}, {vector.y:0.00}, {vector.z:0.00})";
    }
}
