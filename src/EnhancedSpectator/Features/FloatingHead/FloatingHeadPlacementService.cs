using System;
using EnhancedSpectator.GameInterop;
using UnityEngine;

namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Resolves local head anchor placement for placeholder visuals.
/// </summary>
public sealed class FloatingHeadPlacementService
{
    private readonly IGameSpectatorAdapter _gameSpectatorAdapter;

    /// <summary>
    /// Creates a floating-head placement service.
    /// </summary>
    public FloatingHeadPlacementService(IGameSpectatorAdapter gameSpectatorAdapter)
    {
        _gameSpectatorAdapter = gameSpectatorAdapter ?? throw new ArgumentNullException(nameof(gameSpectatorAdapter));
    }

    /// <summary>
    /// Attempts to get the current local player head anchor position.
    /// </summary>
    public bool TryGetAnchorPosition(out Vector3 position)
    {
        return _gameSpectatorAdapter.TryGetLocalPlayerHeadAnchorPosition(out position);
    }

    /// <summary>
    /// Calculates a placeholder pose around the provided anchor.
    /// </summary>
    public bool TryGetPose(
        Vector3 anchorPosition,
        int visualIndex,
        int visualCount,
        float ringRadius,
        float heightOffset,
        bool useCameraVisiblePlacement,
        float cameraForwardOffset,
        bool faceCamera,
        Camera? renderingCamera,
        out Vector3 position,
        out Quaternion rotation)
    {
        int count = Mathf.Max(1, visualCount);
        float radius = Mathf.Max(0f, ringRadius);
        rotation = Quaternion.identity;

        Camera? camera = renderingCamera ?? (TryGetFacingCamera(out Camera? facingCamera) ? facingCamera : null);
        if (useCameraVisiblePlacement && camera != null)
        {
            position = GetCameraVisiblePosition(
                anchorPosition,
                camera,
                visualIndex,
                count,
                radius,
                heightOffset,
                Mathf.Max(0.2f, cameraForwardOffset));
        }
        else
        {
            float angle = (Mathf.PI * 2f * visualIndex) / count;
            Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, heightOffset, Mathf.Sin(angle) * radius);
            position = anchorPosition + offset;
        }

        if (faceCamera && camera != null)
        {
            Vector3 toCamera = camera.transform.position - position;
            if (toCamera.sqrMagnitude > 0.0001f)
            {
                rotation = Quaternion.LookRotation(toCamera.normalized, Vector3.up);
            }
        }

        return true;
    }

    /// <summary>
    /// Attempts to get the local active camera.
    /// </summary>
    public bool TryGetActiveCamera(out Camera? camera)
    {
        if (_gameSpectatorAdapter.TryGetActiveCamera(out camera) && camera != null)
        {
            return true;
        }

        camera = Camera.main;
        if (camera != null)
        {
            return true;
        }

        camera = Camera.current;
        return camera != null;
    }

    /// <summary>
    /// Attempts to get the current in-game display name for a player.
    /// </summary>
    public bool TryGetPlayerDisplayName(ulong clientId, ulong slotId, out string displayName)
    {
        return _gameSpectatorAdapter.TryGetPlayerDisplayName(clientId, slotId, out displayName);
    }

    private bool TryGetFacingCamera(out Camera? camera)
    {
        // Prefer the game-owned active camera because the gameplay camera is not
        // guaranteed to be tagged MainCamera in every modpack.
        return TryGetActiveCamera(out camera);
    }

    private static Vector3 GetCameraVisiblePosition(
        Vector3 anchorPosition,
        Camera camera,
        int visualIndex,
        int visualCount,
        float ringRadius,
        float heightOffset,
        float cameraForwardOffset)
    {
        Transform cameraTransform = camera.transform;
        Vector3 forward = cameraTransform.forward.sqrMagnitude > 0.0001f
            ? cameraTransform.forward.normalized
            : Vector3.forward;
        Vector3 right = cameraTransform.right.sqrMagnitude > 0.0001f
            ? cameraTransform.right.normalized
            : Vector3.right;
        Vector3 up = cameraTransform.up.sqrMagnitude > 0.0001f
            ? cameraTransform.up.normalized
            : Vector3.up;

        float centerIndex = (visualCount - 1) * 0.5f;
        float lateral = (visualIndex - centerIndex) * Mathf.Max(0.2f, ringRadius);
        float vertical = heightOffset + (Mathf.Abs(lateral) * 0.15f);
        float distance = Mathf.Max(camera.nearClipPlane + 0.35f, cameraForwardOffset);

        _ = anchorPosition;
        return cameraTransform.position
            + (forward * distance)
            + (right * lateral)
            + (up * vertical);
    }
}
