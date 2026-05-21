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
/// Runs one-shot runtime player model hierarchy inspections.
/// </summary>
public sealed class PlayerModelInspectionService
{
    private readonly EnhancedSpectatorConfig _config;
    private readonly IGamePlayerModelInspectionAdapter _adapter;

    /// <summary>
    /// Creates a player model inspection service.
    /// </summary>
    public PlayerModelInspectionService(
        EnhancedSpectatorConfig config,
        IGamePlayerModelInspectionAdapter adapter)
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
                ModLog.Debug($"Model inspection skipped because runtime state is unsafe: {reason}.");
                return;
            }

            bool includeLocal = _config.LogLocalPlayerModelOnKey.Value;
            bool includeRemote = _config.LogRemotePlayerModelsOnKey.Value;
            if (!includeLocal && !includeRemote)
            {
                ModLog.Info("Enhanced Spectator model inspection skipped: local and remote player logging are both disabled.");
                return;
            }

            if (!_adapter.TryGetPlayerModelInspectionSnapshots(
                includeLocal,
                includeRemote,
                _config.MaxTransformDepth.Value,
                _config.IncludeRendererBounds.Value,
                _config.IncludeMaterials.Value,
                out IReadOnlyList<PlayerModelInspectionSnapshot> snapshots))
            {
                ModLog.Info("Enhanced Spectator model inspection found no current player model snapshots.");
                return;
            }

            ModLog.Info(BuildInspectionLog(snapshots));
        }
        catch (Exception ex)
        {
            ModLog.Error($"Enhanced Spectator model inspection failed: {ex}");
        }
    }

    private string BuildInspectionLog(IReadOnlyList<PlayerModelInspectionSnapshot> snapshots)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Enhanced Spectator player model inspection");
        builder.AppendLine($"SnapshotCount={snapshots.Count}");
        builder.AppendLine($"MaxTransformDepth={_config.MaxTransformDepth.Value}");
        builder.AppendLine($"IncludeRendererBounds={_config.IncludeRendererBounds.Value}");
        builder.AppendLine($"IncludeMaterials={_config.IncludeMaterials.Value}");

        foreach (PlayerModelInspectionSnapshot snapshot in snapshots)
        {
            AppendSnapshot(builder, snapshot);
        }

        return builder.ToString();
    }

    private static void AppendSnapshot(StringBuilder builder, PlayerModelInspectionSnapshot snapshot)
    {
        builder.AppendLine("--- Player ---");
        builder.AppendLine(
            $"playerClientId={snapshot.PlayerClientId}, actualClientId={snapshot.ActualClientId}, local={snapshot.IsLocalPlayer}, controlled={snapshot.IsPlayerControlled}, dead={snapshot.IsPlayerDead}, root={snapshot.RootName}");
        builder.AppendLine($"thisPlayerBody={snapshot.ThisPlayerBodyPath}");
        builder.AppendLine($"meshContainer={snapshot.MeshContainerPath}");
        builder.AppendLine($"lowerSpine={snapshot.LowerSpinePath}");
        builder.AppendLine($"upperSpine={snapshot.UpperSpinePath}");
        builder.AppendLine($"playerGlobalHead={snapshot.PlayerGlobalHeadPath}");
        builder.AppendLine($"playerEye={snapshot.PlayerEyePath}");
        builder.AppendLine($"headCostumeContainer={snapshot.HeadCostumeContainerPath}");
        builder.AppendLine($"headCostumeContainerLocal={snapshot.HeadCostumeContainerLocalPath}");
        builder.AppendLine(
            $"renderersExist: thisPlayerModel={snapshot.HasThisPlayerModel}, LOD1={snapshot.HasThisPlayerModelLod1}, LOD2={snapshot.HasThisPlayerModelLod2}, arms={snapshot.HasThisPlayerModelArms}");

        builder.AppendLine($"headRelatedTransforms={snapshot.HeadRelatedTransforms.Count}");
        foreach (TransformInspectionInfo transform in snapshot.HeadRelatedTransforms)
        {
            builder.AppendLine($"  transform depth={transform.Depth} name={transform.Name} path={transform.Path}");
        }

        builder.AppendLine($"skinnedMeshRenderers={snapshot.SkinnedMeshRenderers.Count}");
        foreach (RendererInspectionInfo renderer in snapshot.SkinnedMeshRenderers)
        {
            builder.Append(
                $"  renderer name={renderer.Name} path={renderer.Path} enabled={renderer.Enabled} mesh={renderer.SharedMeshName} subMeshes={renderer.SubMeshCount} bones={renderer.BonesCount} rootBone={renderer.RootBoneName} materialSlots={renderer.MaterialSlotCount}");

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
