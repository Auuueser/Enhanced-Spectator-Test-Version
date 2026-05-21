using BepInEx.Configuration;
using EnhancedSpectator.Features.FloatingHead;
using UnityEngine;

namespace EnhancedSpectator.Config;

/// <summary>
/// Owns all BepInEx configuration entries for Enhanced Spectator.
/// </summary>
public sealed class EnhancedSpectatorConfig
{
    private EnhancedSpectatorConfig(
        ConfigEntry<bool> enableSpectatorModule,
        ConfigEntry<bool> enableEnhancedSpectator,
        ConfigEntry<bool> enableFreecam,
        ConfigEntry<bool> freecamDefaultOn,
        ConfigEntry<float> freecamRadius,
        ConfigEntry<float> freecamMoveSpeed,
        ConfigEntry<float> freecamFastMoveMultiplier,
        ConfigEntry<float> freecamSlowMoveMultiplier,
        ConfigEntry<float> freecamLookSensitivity,
        ConfigEntry<float> freecamSmoothTime,
        ConfigEntry<bool> clampCameraToRadius,
        ConfigEntry<bool> recenterOnTargetSwitch,
        ConfigEntry<bool> disableDuringGameOverOverride,
        ConfigEntry<KeyCode> toggleFreecamKey,
        ConfigEntry<KeyCode> recenterKey,
        ConfigEntry<KeyCode> resetToVanillaViewKey,
        ConfigEntry<KeyCode> fastMoveKey,
        ConfigEntry<KeyCode> slowMoveKey,
        ConfigEntry<KeyCode> ascendKey,
        ConfigEntry<KeyCode> descendKey,
        ConfigEntry<bool> enableDebugLogging,
        ConfigEntry<bool> enableNetworking,
        ConfigEntry<bool> enableCapabilityHandshake,
        ConfigEntry<bool> enableSpectatorTargetSync,
        ConfigEntry<bool> enableSpectatorPoseSync,
        ConfigEntry<bool> enableHostRelay,
        ConfigEntry<float> spectatorPoseSyncInterval,
        ConfigEntry<bool> enableVoiceActivitySync,
        ConfigEntry<float> voiceActivitySyncInterval,
        ConfigEntry<float> voiceActivityStaleSeconds,
        ConfigEntry<bool> debugVoiceActivitySync,
        ConfigEntry<bool> enableSpectatorVoiceToTarget,
        ConfigEntry<SpectatorVoiceAudienceMode> spectatorVoiceAudienceMode,
        ConfigEntry<float> spectatorVoiceToTargetVolume,
        ConfigEntry<bool> spectatorVoiceUseRemotePosePosition,
        ConfigEntry<bool> spectatorVoiceEnableDistanceAttenuation,
        ConfigEntry<float> spectatorVoiceMinDistance,
        ConfigEntry<float> spectatorVoiceMaxDistance,
        ConfigEntry<float> spectatorVoiceRolloffPower,
        ConfigEntry<float> spectatorVoiceMinimumVolume,
        ConfigEntry<bool> spectatorVoiceFallbackTo2DWhenPoseMissing,
        ConfigEntry<bool> debugSpectatorVoiceRouting,
        ConfigEntry<bool> repairVanillaConnectedPlayerState,
        ConfigEntry<bool> repairVanillaPlayerNames,
        ConfigEntry<bool> debugPlayerStateRepair,
        ConfigEntry<bool> debugNetworkMessages,
        ConfigEntry<bool> debugPoseMessages,
        ConfigEntry<bool> enableSpectatorPresenceDebug,
        ConfigEntry<bool> debugLogPresenceChanges,
        ConfigEntry<bool> enableModelInspection,
        ConfigEntry<bool> logLocalPlayerModelOnKey,
        ConfigEntry<bool> logRemotePlayerModelsOnKey,
        ConfigEntry<KeyCode> modelInspectionKey,
        ConfigEntry<bool> includeRendererBounds,
        ConfigEntry<bool> includeMaterials,
        ConfigEntry<int> maxTransformDepth,
        ConfigEntry<bool> enableRuntimeHeadSourceInspection,
        ConfigEntry<KeyCode> runtimeHeadSourceInspectionKey,
        ConfigEntry<bool> runtimeHeadSourceIncludeRendererBounds,
        ConfigEntry<bool> runtimeHeadSourceIncludeMaterials,
        ConfigEntry<int> runtimeHeadSourceMaxTransformDepth,
        ConfigEntry<bool> enableVoiceDiagnostics,
        ConfigEntry<KeyCode> voiceDiagnosticsKey,
        ConfigEntry<bool> logLocalVoiceStateOnKey,
        ConfigEntry<bool> logRemoteVoiceStatesOnKey,
        ConfigEntry<bool> includeVoiceAudioSourceDetails,
        ConfigEntry<bool> includeWalkieVoiceDiagnostics,
        ConfigEntry<bool> enableFloatingHeadVisuals,
        ConfigEntry<bool> enablePlaceholderVisuals,
        ConfigEntry<bool> useRuntimeDetachedHeadVisuals,
        ConfigEntry<float> runtimeDetachedHeadScale,
        ConfigEntry<float> runtimeDetachedHeadPitchOffset,
        ConfigEntry<float> runtimeDetachedHeadYawOffset,
        ConfigEntry<float> runtimeDetachedHeadRollOffset,
        ConfigEntry<bool> fallbackToPlaceholderWhenDetachedHeadUnavailable,
        ConfigEntry<bool> showRemoteSpectators,
        ConfigEntry<bool> showOnlySpectatorsWatchingMe,
        ConfigEntry<bool> showDeadSpectatorsToAlivePlayers,
        ConfigEntry<bool> showDeadSpectatorsToDeadPlayers,
        ConfigEntry<int> maxFloatingHeadsVisible,
        ConfigEntry<FloatingHeadVisualStyle> visualStyle,
        ConfigEntry<float> placeholderScale,
        ConfigEntry<float> billboardSize,
        ConfigEntry<float> baseAlpha,
        ConfigEntry<bool> useUnlitMaterial,
        ConfigEntry<bool> enableDepthTest,
        ConfigEntry<float> floatingHeadRingRadius,
        ConfigEntry<float> floatingHeadHeightOffset,
        ConfigEntry<bool> useCameraVisiblePlacement,
        ConfigEntry<float> cameraForwardOffset,
        ConfigEntry<float> remotePoseSmoothTime,
        ConfigEntry<bool> keepRemotePoseInView,
        ConfigEntry<float> remotePoseVisibleProxyDistance,
        ConfigEntry<bool> enableScreenFallbackVisual,
        ConfigEntry<float> screenFallbackSize,
        ConfigEntry<float> presenceLostGraceSeconds,
        ConfigEntry<bool> floatingHeadFaceCamera,
        ConfigEntry<bool> pulseWhenSpeaking,
        ConfigEntry<float> speakingScaleMultiplier,
        ConfigEntry<float> speakingPulseSpeed,
        ConfigEntry<float> minimumSpeakingVoiceLevel,
        ConfigEntry<float> speakingPulseAmount,
        ConfigEntry<float> voiceAttackSmoothTime,
        ConfigEntry<float> voiceReleaseSmoothTime,
        ConfigEntry<float> silenceScaleMultiplier,
        ConfigEntry<float> amplitudeSmoothing,
        ConfigEntry<bool> destroyOnPresenceLost,
        ConfigEntry<bool> debugVisualLifecycle,
        ConfigEntry<bool> showNameTags,
        ConfigEntry<float> nameTagScale,
        ConfigEntry<float> nameTagHeightOffset,
        ConfigEntry<float> nameTagMaxDistance,
        ConfigEntry<bool> nameTagUseGamePlayerNames,
        ConfigEntry<bool> nameTagUseFallbackIds,
        ConfigEntry<bool> debugNameTagLifecycle)
    {
        EnableSpectatorModule = enableSpectatorModule;
        EnableEnhancedSpectator = enableEnhancedSpectator;
        EnableFreecam = enableFreecam;
        FreecamDefaultOn = freecamDefaultOn;
        FreecamRadius = freecamRadius;
        FreecamMoveSpeed = freecamMoveSpeed;
        FreecamFastMoveMultiplier = freecamFastMoveMultiplier;
        FreecamSlowMoveMultiplier = freecamSlowMoveMultiplier;
        FreecamLookSensitivity = freecamLookSensitivity;
        FreecamSmoothTime = freecamSmoothTime;
        ClampCameraToRadius = clampCameraToRadius;
        RecenterOnTargetSwitch = recenterOnTargetSwitch;
        DisableDuringGameOverOverride = disableDuringGameOverOverride;
        ToggleFreecamKey = toggleFreecamKey;
        RecenterKey = recenterKey;
        ResetToVanillaViewKey = resetToVanillaViewKey;
        FastMoveKey = fastMoveKey;
        SlowMoveKey = slowMoveKey;
        AscendKey = ascendKey;
        DescendKey = descendKey;
        EnableDebugLogging = enableDebugLogging;
        EnableNetworking = enableNetworking;
        EnableCapabilityHandshake = enableCapabilityHandshake;
        EnableSpectatorTargetSync = enableSpectatorTargetSync;
        EnableSpectatorPoseSync = enableSpectatorPoseSync;
        EnableHostRelay = enableHostRelay;
        SpectatorPoseSyncInterval = spectatorPoseSyncInterval;
        EnableVoiceActivitySync = enableVoiceActivitySync;
        VoiceActivitySyncInterval = voiceActivitySyncInterval;
        VoiceActivityStaleSeconds = voiceActivityStaleSeconds;
        DebugVoiceActivitySync = debugVoiceActivitySync;
        EnableSpectatorVoiceToTarget = enableSpectatorVoiceToTarget;
        SpectatorVoiceAudienceMode = spectatorVoiceAudienceMode;
        SpectatorVoiceToTargetVolume = spectatorVoiceToTargetVolume;
        SpectatorVoiceUseRemotePosePosition = spectatorVoiceUseRemotePosePosition;
        SpectatorVoiceEnableDistanceAttenuation = spectatorVoiceEnableDistanceAttenuation;
        SpectatorVoiceMinDistance = spectatorVoiceMinDistance;
        SpectatorVoiceMaxDistance = spectatorVoiceMaxDistance;
        SpectatorVoiceRolloffPower = spectatorVoiceRolloffPower;
        SpectatorVoiceMinimumVolume = spectatorVoiceMinimumVolume;
        SpectatorVoiceFallbackTo2DWhenPoseMissing = spectatorVoiceFallbackTo2DWhenPoseMissing;
        DebugSpectatorVoiceRouting = debugSpectatorVoiceRouting;
        RepairVanillaConnectedPlayerState = repairVanillaConnectedPlayerState;
        RepairVanillaPlayerNames = repairVanillaPlayerNames;
        DebugPlayerStateRepair = debugPlayerStateRepair;
        DebugNetworkMessages = debugNetworkMessages;
        DebugPoseMessages = debugPoseMessages;
        EnableSpectatorPresenceDebug = enableSpectatorPresenceDebug;
        DebugLogPresenceChanges = debugLogPresenceChanges;
        EnableModelInspection = enableModelInspection;
        LogLocalPlayerModelOnKey = logLocalPlayerModelOnKey;
        LogRemotePlayerModelsOnKey = logRemotePlayerModelsOnKey;
        ModelInspectionKey = modelInspectionKey;
        IncludeRendererBounds = includeRendererBounds;
        IncludeMaterials = includeMaterials;
        MaxTransformDepth = maxTransformDepth;
        EnableRuntimeHeadSourceInspection = enableRuntimeHeadSourceInspection;
        RuntimeHeadSourceInspectionKey = runtimeHeadSourceInspectionKey;
        RuntimeHeadSourceIncludeRendererBounds = runtimeHeadSourceIncludeRendererBounds;
        RuntimeHeadSourceIncludeMaterials = runtimeHeadSourceIncludeMaterials;
        RuntimeHeadSourceMaxTransformDepth = runtimeHeadSourceMaxTransformDepth;
        EnableVoiceDiagnostics = enableVoiceDiagnostics;
        VoiceDiagnosticsKey = voiceDiagnosticsKey;
        LogLocalVoiceStateOnKey = logLocalVoiceStateOnKey;
        LogRemoteVoiceStatesOnKey = logRemoteVoiceStatesOnKey;
        IncludeVoiceAudioSourceDetails = includeVoiceAudioSourceDetails;
        IncludeWalkieVoiceDiagnostics = includeWalkieVoiceDiagnostics;
        EnableFloatingHeadVisuals = enableFloatingHeadVisuals;
        EnablePlaceholderVisuals = enablePlaceholderVisuals;
        UseRuntimeDetachedHeadVisuals = useRuntimeDetachedHeadVisuals;
        RuntimeDetachedHeadScale = runtimeDetachedHeadScale;
        RuntimeDetachedHeadPitchOffset = runtimeDetachedHeadPitchOffset;
        RuntimeDetachedHeadYawOffset = runtimeDetachedHeadYawOffset;
        RuntimeDetachedHeadRollOffset = runtimeDetachedHeadRollOffset;
        FallbackToPlaceholderWhenDetachedHeadUnavailable = fallbackToPlaceholderWhenDetachedHeadUnavailable;
        ShowRemoteSpectators = showRemoteSpectators;
        ShowOnlySpectatorsWatchingMe = showOnlySpectatorsWatchingMe;
        ShowDeadSpectatorsToAlivePlayers = showDeadSpectatorsToAlivePlayers;
        ShowDeadSpectatorsToDeadPlayers = showDeadSpectatorsToDeadPlayers;
        MaxFloatingHeadsVisible = maxFloatingHeadsVisible;
        VisualStyle = visualStyle;
        PlaceholderScale = placeholderScale;
        BillboardSize = billboardSize;
        BaseAlpha = baseAlpha;
        UseUnlitMaterial = useUnlitMaterial;
        EnableDepthTest = enableDepthTest;
        FloatingHeadRingRadius = floatingHeadRingRadius;
        FloatingHeadHeightOffset = floatingHeadHeightOffset;
        UseCameraVisiblePlacement = useCameraVisiblePlacement;
        CameraForwardOffset = cameraForwardOffset;
        RemotePoseSmoothTime = remotePoseSmoothTime;
        KeepRemotePoseInView = keepRemotePoseInView;
        RemotePoseVisibleProxyDistance = remotePoseVisibleProxyDistance;
        EnableScreenFallbackVisual = enableScreenFallbackVisual;
        ScreenFallbackSize = screenFallbackSize;
        PresenceLostGraceSeconds = presenceLostGraceSeconds;
        FloatingHeadFaceCamera = floatingHeadFaceCamera;
        PulseWhenSpeaking = pulseWhenSpeaking;
        SpeakingScaleMultiplier = speakingScaleMultiplier;
        SpeakingPulseSpeed = speakingPulseSpeed;
        MinimumSpeakingVoiceLevel = minimumSpeakingVoiceLevel;
        SpeakingPulseAmount = speakingPulseAmount;
        VoiceAttackSmoothTime = voiceAttackSmoothTime;
        VoiceReleaseSmoothTime = voiceReleaseSmoothTime;
        SilenceScaleMultiplier = silenceScaleMultiplier;
        AmplitudeSmoothing = amplitudeSmoothing;
        DestroyOnPresenceLost = destroyOnPresenceLost;
        DebugVisualLifecycle = debugVisualLifecycle;
        ShowNameTags = showNameTags;
        NameTagScale = nameTagScale;
        NameTagHeightOffset = nameTagHeightOffset;
        NameTagMaxDistance = nameTagMaxDistance;
        NameTagUseGamePlayerNames = nameTagUseGamePlayerNames;
        NameTagUseFallbackIds = nameTagUseFallbackIds;
        DebugNameTagLifecycle = debugNameTagLifecycle;
    }

    /// <summary>
    /// Enables the spectator feature module.
    /// </summary>
    public ConfigEntry<bool> EnableSpectatorModule { get; }

    /// <summary>
    /// Enables all enhanced spectator behavior.
    /// </summary>
    public ConfigEntry<bool> EnableEnhancedSpectator { get; }

    /// <summary>
    /// Enables local spectator freecam behavior.
    /// </summary>
    public ConfigEntry<bool> EnableFreecam { get; }

    /// <summary>
    /// Enables freecam automatically after entering vanilla spectator state.
    /// </summary>
    public ConfigEntry<bool> FreecamDefaultOn { get; }

    /// <summary>
    /// Limits camera distance from the current target anchor.
    /// </summary>
    public ConfigEntry<float> FreecamRadius { get; }

    /// <summary>
    /// Controls freecam movement speed.
    /// </summary>
    public ConfigEntry<float> FreecamMoveSpeed { get; }

    /// <summary>
    /// Controls fast movement speed multiplier.
    /// </summary>
    public ConfigEntry<float> FreecamFastMoveMultiplier { get; }

    /// <summary>
    /// Controls slow movement speed multiplier.
    /// </summary>
    public ConfigEntry<float> FreecamSlowMoveMultiplier { get; }

    /// <summary>
    /// Controls mouse look sensitivity.
    /// </summary>
    public ConfigEntry<float> FreecamLookSensitivity { get; }

    /// <summary>
    /// Controls camera smoothing time. Set to zero to disable smoothing.
    /// </summary>
    public ConfigEntry<float> FreecamSmoothTime { get; }

    /// <summary>
    /// Enables clamping camera offset to the configured radius.
    /// </summary>
    public ConfigEntry<bool> ClampCameraToRadius { get; }

    /// <summary>
    /// Recenters freecam when vanilla target selection changes.
    /// </summary>
    public ConfigEntry<bool> RecenterOnTargetSwitch { get; }

    /// <summary>
    /// Disables enhanced freecam during vanilla game-over camera override.
    /// </summary>
    public ConfigEntry<bool> DisableDuringGameOverOverride { get; }

    /// <summary>
    /// Toggles enhanced freecam while spectating.
    /// </summary>
    public ConfigEntry<KeyCode> ToggleFreecamKey { get; }

    /// <summary>
    /// Recenters enhanced freecam around the current target.
    /// </summary>
    public ConfigEntry<KeyCode> RecenterKey { get; }

    /// <summary>
    /// Disables enhanced freecam and returns to vanilla spectator camera.
    /// </summary>
    public ConfigEntry<KeyCode> ResetToVanillaViewKey { get; }

    /// <summary>
    /// Fast movement modifier key.
    /// </summary>
    public ConfigEntry<KeyCode> FastMoveKey { get; }

    /// <summary>
    /// Slow movement modifier key.
    /// </summary>
    public ConfigEntry<KeyCode> SlowMoveKey { get; }

    /// <summary>
    /// Upward movement key.
    /// </summary>
    public ConfigEntry<KeyCode> AscendKey { get; }

    /// <summary>
    /// Downward movement key.
    /// </summary>
    public ConfigEntry<KeyCode> DescendKey { get; }

    /// <summary>
    /// Enables verbose debug logging.
    /// </summary>
    public ConfigEntry<bool> EnableDebugLogging { get; }

    /// <summary>
    /// Enables Enhanced Spectator networking modules.
    /// </summary>
    public ConfigEntry<bool> EnableNetworking { get; }

    /// <summary>
    /// Enables the mod capability handshake.
    /// </summary>
    public ConfigEntry<bool> EnableCapabilityHandshake { get; }

    /// <summary>
    /// Enables spectator target synchronization.
    /// </summary>
    public ConfigEntry<bool> EnableSpectatorTargetSync { get; }

    /// <summary>
    /// Enables spectator camera pose synchronization.
    /// </summary>
    public ConfigEntry<bool> EnableSpectatorPoseSync { get; }

    /// <summary>
    /// Enables host-mediated relay for client-origin spectator state.
    /// </summary>
    public ConfigEntry<bool> EnableHostRelay { get; }

    /// <summary>
    /// Controls the minimum interval between spectator pose messages.
    /// </summary>
    public ConfigEntry<float> SpectatorPoseSyncInterval { get; }

    /// <summary>
    /// Enables visual-only voice activity synchronization for floating-head scaling.
    /// </summary>
    public ConfigEntry<bool> EnableVoiceActivitySync { get; }

    /// <summary>
    /// Controls the minimum interval between voice activity visual messages.
    /// </summary>
    public ConfigEntry<float> VoiceActivitySyncInterval { get; }

    /// <summary>
    /// Controls how long received voice activity can drive visuals without a refresh.
    /// </summary>
    public ConfigEntry<float> VoiceActivityStaleSeconds { get; }

    /// <summary>
    /// Enables verbose voice activity sync diagnostics.
    /// </summary>
    public ConfigEntry<bool> DebugVoiceActivitySync { get; }

    /// <summary>
    /// Enables the experimental spectator-to-target voice route.
    /// </summary>
    public ConfigEntry<bool> EnableSpectatorVoiceToTarget { get; }

    /// <summary>
    /// Controls which compatible modded players can hear routed dead spectator voice.
    /// </summary>
    public ConfigEntry<SpectatorVoiceAudienceMode> SpectatorVoiceAudienceMode { get; }

    /// <summary>
    /// Controls routed spectator voice playback volume.
    /// </summary>
    public ConfigEntry<float> SpectatorVoiceToTargetVolume { get; }

    /// <summary>
    /// Uses synced spectator camera pose for routed voice position.
    /// </summary>
    public ConfigEntry<bool> SpectatorVoiceUseRemotePosePosition { get; }

    /// <summary>
    /// Enables local volume attenuation by distance from the synced spectator pose.
    /// </summary>
    public ConfigEntry<bool> SpectatorVoiceEnableDistanceAttenuation { get; }

    /// <summary>
    /// Keeps full routed spectator voice volume within this distance.
    /// </summary>
    public ConfigEntry<float> SpectatorVoiceMinDistance { get; }

    /// <summary>
    /// Reaches the minimum routed spectator voice volume at this distance.
    /// </summary>
    public ConfigEntry<float> SpectatorVoiceMaxDistance { get; }

    /// <summary>
    /// Controls the routed spectator voice distance attenuation curve.
    /// </summary>
    public ConfigEntry<float> SpectatorVoiceRolloffPower { get; }

    /// <summary>
    /// Controls the minimum routed spectator voice volume multiplier at maximum distance.
    /// </summary>
    public ConfigEntry<float> SpectatorVoiceMinimumVolume { get; }

    /// <summary>
    /// Keeps routed voice audible in 2D when synced spectator pose data is temporarily unavailable.
    /// </summary>
    public ConfigEntry<bool> SpectatorVoiceFallbackTo2DWhenPoseMissing { get; }

    /// <summary>
    /// Enables spectator voice routing diagnostics.
    /// </summary>
    public ConfigEntry<bool> DebugSpectatorVoiceRouting { get; }

    /// <summary>
    /// Repairs late vanilla connected-player state for modded peers after identity sync.
    /// </summary>
    public ConfigEntry<bool> RepairVanillaConnectedPlayerState { get; }

    /// <summary>
    /// Applies synced mod peer names to vanilla local player scripts when repairing state.
    /// </summary>
    public ConfigEntry<bool> RepairVanillaPlayerNames { get; }

    /// <summary>
    /// Enables verbose diagnostics for vanilla player state repair.
    /// </summary>
    public ConfigEntry<bool> DebugPlayerStateRepair { get; }

    /// <summary>
    /// Enables verbose networking diagnostics.
    /// </summary>
    public ConfigEntry<bool> DebugNetworkMessages { get; }

    /// <summary>
    /// Enables high-frequency spectator pose network diagnostics.
    /// </summary>
    public ConfigEntry<bool> DebugPoseMessages { get; }

    /// <summary>
    /// Enables debug-only remote spectator presence inference.
    /// </summary>
    public ConfigEntry<bool> EnableSpectatorPresenceDebug { get; }

    /// <summary>
    /// Enables debug logs when remote spectator presence changes.
    /// </summary>
    public ConfigEntry<bool> DebugLogPresenceChanges { get; }

    /// <summary>
    /// Enables key-triggered runtime player model inspection.
    /// </summary>
    public ConfigEntry<bool> EnableModelInspection { get; }

    /// <summary>
    /// Logs local player model information when the inspection key is pressed.
    /// </summary>
    public ConfigEntry<bool> LogLocalPlayerModelOnKey { get; }

    /// <summary>
    /// Logs remote player model information when the inspection key is pressed.
    /// </summary>
    public ConfigEntry<bool> LogRemotePlayerModelsOnKey { get; }

    /// <summary>
    /// Triggers one model inspection log pass.
    /// </summary>
    public ConfigEntry<KeyCode> ModelInspectionKey { get; }

    /// <summary>
    /// Includes renderer world bounds in inspection logs.
    /// </summary>
    public ConfigEntry<bool> IncludeRendererBounds { get; }

    /// <summary>
    /// Includes material names in inspection logs.
    /// </summary>
    public ConfigEntry<bool> IncludeMaterials { get; }

    /// <summary>
    /// Limits transform hierarchy traversal depth.
    /// </summary>
    public ConfigEntry<int> MaxTransformDepth { get; }

    /// <summary>
    /// Enables key-triggered runtime detached-head source inspection.
    /// </summary>
    public ConfigEntry<bool> EnableRuntimeHeadSourceInspection { get; }

    /// <summary>
    /// Triggers one runtime detached-head source inspection pass.
    /// </summary>
    public ConfigEntry<KeyCode> RuntimeHeadSourceInspectionKey { get; }

    /// <summary>
    /// Includes detached-head renderer world bounds in inspection logs.
    /// </summary>
    public ConfigEntry<bool> RuntimeHeadSourceIncludeRendererBounds { get; }

    /// <summary>
    /// Includes detached-head material names in inspection logs.
    /// </summary>
    public ConfigEntry<bool> RuntimeHeadSourceIncludeMaterials { get; }

    /// <summary>
    /// Limits detached-head hierarchy traversal depth.
    /// </summary>
    public ConfigEntry<int> RuntimeHeadSourceMaxTransformDepth { get; }

    /// <summary>
    /// Enables key-triggered read-only voice diagnostics.
    /// </summary>
    public ConfigEntry<bool> EnableVoiceDiagnostics { get; }

    /// <summary>
    /// Triggers one voice diagnostics log pass.
    /// </summary>
    public ConfigEntry<KeyCode> VoiceDiagnosticsKey { get; }

    /// <summary>
    /// Logs local player voice state when the diagnostics key is pressed.
    /// </summary>
    public ConfigEntry<bool> LogLocalVoiceStateOnKey { get; }

    /// <summary>
    /// Logs remote player voice states when the diagnostics key is pressed.
    /// </summary>
    public ConfigEntry<bool> LogRemoteVoiceStatesOnKey { get; }

    /// <summary>
    /// Includes mapped AudioSource details in voice diagnostics logs.
    /// </summary>
    public ConfigEntry<bool> IncludeVoiceAudioSourceDetails { get; }

    /// <summary>
    /// Includes walkie-talkie voice flags in voice diagnostics logs.
    /// </summary>
    public ConfigEntry<bool> IncludeWalkieVoiceDiagnostics { get; }

    /// <summary>
    /// Enables local floating-head placeholder visuals.
    /// </summary>
    public ConfigEntry<bool> EnableFloatingHeadVisuals { get; }

    /// <summary>
    /// Enables runtime-created placeholder visuals.
    /// </summary>
    public ConfigEntry<bool> EnablePlaceholderVisuals { get; }

    /// <summary>
    /// Enables runtime detached-head clones when a confirmed source exists.
    /// </summary>
    public ConfigEntry<bool> UseRuntimeDetachedHeadVisuals { get; }

    /// <summary>
    /// Controls runtime detached-head clone scale.
    /// </summary>
    public ConfigEntry<float> RuntimeDetachedHeadScale { get; }

    /// <summary>
    /// Pitch correction applied to runtime detached-head visuals.
    /// </summary>
    public ConfigEntry<float> RuntimeDetachedHeadPitchOffset { get; }

    /// <summary>
    /// Yaw correction applied to runtime detached-head visuals.
    /// </summary>
    public ConfigEntry<float> RuntimeDetachedHeadYawOffset { get; }

    /// <summary>
    /// Roll correction applied to runtime detached-head visuals.
    /// </summary>
    public ConfigEntry<float> RuntimeDetachedHeadRollOffset { get; }

    /// <summary>
    /// Falls back to placeholder visuals when detached-head source is unavailable.
    /// </summary>
    public ConfigEntry<bool> FallbackToPlaceholderWhenDetachedHeadUnavailable { get; }

    /// <summary>
    /// Shows remote modded players while they are spectating, even when they are not watching the local player.
    /// </summary>
    public ConfigEntry<bool> ShowRemoteSpectators { get; }

    /// <summary>
    /// Restricts placeholder visuals to remote spectators whose current target is the local player.
    /// </summary>
    public ConfigEntry<bool> ShowOnlySpectatorsWatchingMe { get; }

    /// <summary>
    /// Allows living local players to see remote spectator placeholders.
    /// </summary>
    public ConfigEntry<bool> ShowDeadSpectatorsToAlivePlayers { get; }

    /// <summary>
    /// Allows dead or spectating local players to see remote spectator placeholders.
    /// </summary>
    public ConfigEntry<bool> ShowDeadSpectatorsToDeadPlayers { get; }

    /// <summary>
    /// Limits the number of remote spectator placeholders shown at once.
    /// </summary>
    public ConfigEntry<int> MaxFloatingHeadsVisible { get; }

    /// <summary>
    /// Controls the runtime-only placeholder visual style.
    /// </summary>
    public ConfigEntry<FloatingHeadVisualStyle> VisualStyle { get; }

    /// <summary>
    /// Controls placeholder sphere scale.
    /// </summary>
    public ConfigEntry<float> PlaceholderScale { get; }

    /// <summary>
    /// Controls billboard and ring marker size.
    /// </summary>
    public ConfigEntry<float> BillboardSize { get; }

    /// <summary>
    /// Controls placeholder material alpha where the runtime shader supports it.
    /// </summary>
    public ConfigEntry<float> BaseAlpha { get; }

    /// <summary>
    /// Prefers an unlit runtime material for placeholders.
    /// </summary>
    public ConfigEntry<bool> UseUnlitMaterial { get; }

    /// <summary>
    /// Controls whether placeholder material should use normal depth testing when supported.
    /// </summary>
    public ConfigEntry<bool> EnableDepthTest { get; }

    /// <summary>
    /// Controls horizontal ring radius around the local head anchor.
    /// </summary>
    public ConfigEntry<float> FloatingHeadRingRadius { get; }

    /// <summary>
    /// Controls vertical offset above the local head anchor.
    /// </summary>
    public ConfigEntry<float> FloatingHeadHeightOffset { get; }

    /// <summary>
    /// Biases placeholders into the local active camera view.
    /// </summary>
    public ConfigEntry<bool> UseCameraVisiblePlacement { get; }

    /// <summary>
    /// Controls how far placeholders are pushed toward the active camera forward direction.
    /// </summary>
    public ConfigEntry<float> CameraForwardOffset { get; }

    /// <summary>
    /// Smooths remote spectator placeholder movement.
    /// </summary>
    public ConfigEntry<float> RemotePoseSmoothTime { get; }

    /// <summary>
    /// Keeps remote spectator pose markers visible when the true pose is outside the local camera view.
    /// </summary>
    public ConfigEntry<bool> KeepRemotePoseInView { get; }

    /// <summary>
    /// Controls the camera-forward distance used for out-of-view remote pose proxy markers.
    /// </summary>
    public ConfigEntry<float> RemotePoseVisibleProxyDistance { get; }

    /// <summary>
    /// Draws a runtime IMGUI marker at the placeholder screen position for render-pipeline fallback.
    /// </summary>
    public ConfigEntry<bool> EnableScreenFallbackVisual { get; }

    /// <summary>
    /// Controls the screen fallback marker size in pixels.
    /// </summary>
    public ConfigEntry<float> ScreenFallbackSize { get; }

    /// <summary>
    /// Keeps placeholder visuals alive briefly through transient empty presence frames.
    /// </summary>
    public ConfigEntry<float> PresenceLostGraceSeconds { get; }

    /// <summary>
    /// Rotates placeholder visuals toward the local camera.
    /// </summary>
    public ConfigEntry<bool> FloatingHeadFaceCamera { get; }

    /// <summary>
    /// Enables local placeholder scale pulse from available voice activity.
    /// </summary>
    public ConfigEntry<bool> PulseWhenSpeaking { get; }

    /// <summary>
    /// Scale multiplier for speaking spectators.
    /// </summary>
    public ConfigEntry<float> SpeakingScaleMultiplier { get; }

    /// <summary>
    /// Pulse speed for speaking spectators.
    /// </summary>
    public ConfigEntry<float> SpeakingPulseSpeed { get; }

    /// <summary>
    /// Voice level used when speaking is true but amplitude is unavailable.
    /// </summary>
    public ConfigEntry<float> MinimumSpeakingVoiceLevel { get; }

    /// <summary>
    /// Extra scale pulse amount for speaking spectators.
    /// </summary>
    public ConfigEntry<float> SpeakingPulseAmount { get; }

    /// <summary>
    /// Smooth time used when speaking starts.
    /// </summary>
    public ConfigEntry<float> VoiceAttackSmoothTime { get; }

    /// <summary>
    /// Smooth time used when speaking stops.
    /// </summary>
    public ConfigEntry<float> VoiceReleaseSmoothTime { get; }

    /// <summary>
    /// Scale multiplier for silent or unknown voice state.
    /// </summary>
    public ConfigEntry<float> SilenceScaleMultiplier { get; }

    /// <summary>
    /// Smooth time for voice activity amplitude used by placeholders.
    /// </summary>
    public ConfigEntry<float> AmplitudeSmoothing { get; }

    /// <summary>
    /// Destroys placeholder visuals when remote presence is lost.
    /// </summary>
    public ConfigEntry<bool> DestroyOnPresenceLost { get; }

    /// <summary>
    /// Enables verbose placeholder visual lifecycle logs.
    /// </summary>
    public ConfigEntry<bool> DebugVisualLifecycle { get; }

    /// <summary>
    /// Enables runtime-only name tags above floating-head placeholders.
    /// </summary>
    public ConfigEntry<bool> ShowNameTags { get; }

    /// <summary>
    /// Controls the world-space text character size for name tags.
    /// </summary>
    public ConfigEntry<float> NameTagScale { get; }

    /// <summary>
    /// Controls the vertical offset above the floating-head placeholder.
    /// </summary>
    public ConfigEntry<float> NameTagHeightOffset { get; }

    /// <summary>
    /// Hides name tags beyond this camera distance. Set to zero to disable distance culling.
    /// </summary>
    public ConfigEntry<float> NameTagMaxDistance { get; }

    /// <summary>
    /// Uses confirmed in-game player usernames when available.
    /// </summary>
    public ConfigEntry<bool> NameTagUseGamePlayerNames { get; }

    /// <summary>
    /// Uses fallback client and slot ids when a game player name is unavailable.
    /// </summary>
    public ConfigEntry<bool> NameTagUseFallbackIds { get; }

    /// <summary>
    /// Enables verbose name tag lifecycle diagnostics.
    /// </summary>
    public ConfigEntry<bool> DebugNameTagLifecycle { get; }

    /// <summary>
    /// Binds all configuration entries from the provided BepInEx config file.
    /// </summary>
    public static EnhancedSpectatorConfig Bind(ConfigFile config)
    {
        ConfigEntry<bool> enableSpectatorModule = config.Bind(
            "Features",
            "EnableSpectatorModule",
            true,
            "Loads the spectator feature module.");

        ConfigEntry<bool> enableEnhancedSpectator = config.Bind(
            "Spectator.Freecam",
            "EnableEnhancedSpectator",
            true,
            "Enables all enhanced spectator behavior.");

        ConfigEntry<bool> enableFreecam = config.Bind(
            "Spectator.Freecam",
            "EnableFreecam",
            true,
            "Enables local spectator freecam behavior.");

        ConfigEntry<bool> freecamDefaultOn = config.Bind(
            "Spectator.Freecam",
            "FreecamDefaultOn",
            true,
            "Automatically enables freecam after entering vanilla spectator state.");

        ConfigEntry<float> freecamRadius = config.Bind(
            "Spectator.Freecam",
            "FreecamRadius",
            8.0f,
            "Maximum freecam offset radius from the current target anchor.");

        ConfigEntry<float> freecamMoveSpeed = config.Bind(
            "Spectator.Freecam",
            "FreecamMoveSpeed",
            4.0f,
            "Base freecam movement speed in units per second.");

        ConfigEntry<float> freecamFastMoveMultiplier = config.Bind(
            "Spectator.Freecam",
            "FreecamFastMoveMultiplier",
            2.5f,
            "Movement multiplier while the fast movement key is held.");

        ConfigEntry<float> freecamSlowMoveMultiplier = config.Bind(
            "Spectator.Freecam",
            "FreecamSlowMoveMultiplier",
            0.35f,
            "Movement multiplier while the slow movement key is held.");

        ConfigEntry<float> freecamLookSensitivity = config.Bind(
            "Spectator.Freecam",
            "FreecamLookSensitivity",
            1.0f,
            "Mouse look sensitivity multiplier.");

        ConfigEntry<float> freecamSmoothTime = config.Bind(
            "Spectator.Freecam",
            "FreecamSmoothTime",
            0.04f,
            "Smooth damp time for camera position. Set to 0 to disable smoothing.");

        ConfigEntry<bool> clampCameraToRadius = config.Bind(
            "Spectator.Freecam",
            "ClampCameraToRadius",
            true,
            "Clamps freecam offset to FreecamRadius.");

        ConfigEntry<bool> recenterOnTargetSwitch = config.Bind(
            "Spectator.Freecam",
            "RecenterOnTargetSwitch",
            true,
            "Recenters freecam when vanilla switches the spectated target.");

        ConfigEntry<bool> disableDuringGameOverOverride = config.Bind(
            "Spectator.Freecam",
            "DisableDuringGameOverOverride",
            true,
            "Disables enhanced freecam while vanilla game-over spectator camera override is active.");

        ConfigEntry<KeyCode> toggleFreecamKey = config.Bind(
            "Spectator.Freecam.Keys",
            "ToggleFreecamKey",
            KeyCode.F6,
            "Toggles enhanced freecam while spectating.");

        ConfigEntry<KeyCode> recenterKey = config.Bind(
            "Spectator.Freecam.Keys",
            "RecenterKey",
            KeyCode.R,
            "Recenters enhanced freecam around the current spectated target.");

        ConfigEntry<KeyCode> resetToVanillaViewKey = config.Bind(
            "Spectator.Freecam.Keys",
            "ResetToVanillaViewKey",
            KeyCode.F7,
            "Disables enhanced freecam and returns to vanilla spectator camera until toggled again.");

        ConfigEntry<KeyCode> fastMoveKey = config.Bind(
            "Spectator.Freecam.Keys",
            "FastMoveKey",
            KeyCode.LeftShift,
            "Fast movement modifier key.");

        ConfigEntry<KeyCode> slowMoveKey = config.Bind(
            "Spectator.Freecam.Keys",
            "SlowMoveKey",
            KeyCode.LeftAlt,
            "Slow movement modifier key.");

        ConfigEntry<KeyCode> ascendKey = config.Bind(
            "Spectator.Freecam.Keys",
            "AscendKey",
            KeyCode.Space,
            "Moves the freecam upward while held.");

        ConfigEntry<KeyCode> descendKey = config.Bind(
            "Spectator.Freecam.Keys",
            "DescendKey",
            KeyCode.LeftControl,
            "Moves the freecam downward while held.");

        ConfigEntry<bool> enableDebugLogging = config.Bind(
            "Logging",
            "EnableDebugLogging",
            false,
            "Enables verbose Enhanced Spectator debug logs.");

        ConfigEntry<bool> enableNetworking = config.Bind(
            "Networking",
            "EnableNetworking",
            true,
            "Enables Enhanced Spectator mod-owned networking modules.");

        ConfigEntry<bool> enableCapabilityHandshake = config.Bind(
            "Networking",
            "EnableCapabilityHandshake",
            true,
            "Enables mod capability handshake messages over Unity Netcode custom messaging.");

        ConfigEntry<bool> enableSpectatorTargetSync = config.Bind(
            "Networking",
            "EnableSpectatorTargetSync",
            true,
            "Enables handshake-gated spectator target state synchronization.");

        ConfigEntry<bool> enableSpectatorPoseSync = config.Bind(
            "Networking",
            "EnableSpectatorPoseSync",
            true,
            "Enables handshake-gated spectator camera pose synchronization for placeholder visuals.");

        ConfigEntry<bool> enableHostRelay = config.Bind(
            "Networking",
            "EnableHostRelay",
            true,
            "Enables host-mediated relay of compatible client spectator target and pose state to other modded clients. Required for Client A -> Client B visibility in three-player rooms.");

        ConfigEntry<float> spectatorPoseSyncInterval = config.Bind(
            "Networking",
            "SpectatorPoseSyncInterval",
            0.1f,
            "Minimum seconds between spectator camera pose messages. Higher values reduce traffic; lower values track movement more closely.");

        ConfigEntry<bool> enableVoiceActivitySync = config.Bind(
            "Networking",
            "EnableVoiceActivitySync",
            true,
            "Enables visual-only voice activity synchronization so remote floating heads can scale from the speaker's local microphone amplitude. This does not forward voice audio.");

        ConfigEntry<float> voiceActivitySyncInterval = config.Bind(
            "Networking",
            "VoiceActivitySyncInterval",
            0.066f,
            "Minimum seconds between voice activity visual messages. Lower values react faster but send more network metadata.");

        ConfigEntry<float> voiceActivityStaleSeconds = config.Bind(
            "Networking",
            "VoiceActivityStaleSeconds",
            0.5f,
            "Seconds before a received voice activity visual state is considered stale. This prevents dropped silence packets from leaving a remote head enlarged.");

        ConfigEntry<bool> debugVoiceActivitySync = config.Bind(
            "Networking",
            "DebugVoiceActivitySync",
            false,
            "Logs voice activity sync send/receive/relay diagnostics. Keep disabled during normal testing.");

        ConfigEntry<bool> enableSpectatorVoiceToTarget = config.Bind(
            "VoiceRouting",
            "EnableSpectatorVoiceToTarget",
            true,
            "Enables modded players to hear remote dead spectators according to SpectatorVoiceAudienceMode. Only applies between peers that advertised Enhanced Spectator voice-routing support.");

        ConfigEntry<SpectatorVoiceAudienceMode> spectatorVoiceAudienceMode = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceAudienceMode",
            EnhancedSpectator.Config.SpectatorVoiceAudienceMode.AllModdedPlayers,
            "Controls who can hear routed dead spectator voice: WatchedTargetOnly, AllModdedPlayers, AliveModdedPlayersOnly, or DeadModdedPlayersOnly.");

        ConfigEntry<float> spectatorVoiceToTargetVolume = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceToTargetVolume",
            1.0f,
            "Local playback volume for routed spectator voice. This writes only local Dissonance playback volume for eligible dead spectators.");

        ConfigEntry<bool> spectatorVoiceUseRemotePosePosition = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceUseRemotePosePosition",
            true,
            "Positions routed spectator voice at the synced spectator camera pose when available. Disable to force safer 2D local playback.");

        ConfigEntry<bool> spectatorVoiceEnableDistanceAttenuation = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceEnableDistanceAttenuation",
            true,
            "Reduces routed spectator voice volume by distance from the synced spectator camera pose. Requires SpectatorVoiceUseRemotePosePosition=true and pose sync.");

        ConfigEntry<float> spectatorVoiceMinDistance = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceMinDistance",
            2.0f,
            "Distance in meters that keeps routed spectator voice at full configured volume.");

        ConfigEntry<float> spectatorVoiceMaxDistance = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceMaxDistance",
            18.0f,
            "Distance in meters where routed spectator voice reaches SpectatorVoiceMinimumVolume.");

        ConfigEntry<float> spectatorVoiceRolloffPower = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceRolloffPower",
            1.25f,
            "Distance attenuation curve. 1 is linear; higher values keep near voices louder and fade more near the max distance.");

        ConfigEntry<float> spectatorVoiceMinimumVolume = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceMinimumVolume",
            0.0f,
            "Minimum routed spectator voice volume multiplier at or beyond SpectatorVoiceMaxDistance.");

        ConfigEntry<bool> spectatorVoiceFallbackTo2DWhenPoseMissing = config.Bind(
            "VoiceRouting",
            "SpectatorVoiceFallbackTo2DWhenPoseMissing",
            false,
            "Falls back to 2D routed spectator voice when synced pose data is temporarily unavailable instead of dropping voice entirely. Disabled by default so relayed listeners do not hear stale global voice when pose sync is missing.");

        ConfigEntry<bool> debugSpectatorVoiceRouting = config.Bind(
            "VoiceRouting",
            "DebugSpectatorVoiceRouting",
            false,
            "Logs spectator voice route enable/clear diagnostics. Requires Logging.EnableDebugLogging=true.");

        ConfigEntry<bool> repairVanillaConnectedPlayerState = config.Bind(
            "Networking",
            "RepairVanillaConnectedPlayerState",
            true,
            "Repairs late vanilla connected-player controlled flags using vanilla ClientPlayerList. This also runs in local-only sessions when the host is unmodded, fixing cases where another connected client is missing from the ESC player list or vanilla spectator target list.");

        ConfigEntry<bool> repairVanillaPlayerNames = config.Bind(
            "Networking",
            "RepairVanillaPlayerNames",
            true,
            "Applies synced Enhanced Spectator peer names, or a vanilla Steam lobby fallback when no mod peer identity is available, to repaired player scripts and the ESC player list when vanilla still reports generic Player # names.");

        ConfigEntry<bool> debugPlayerStateRepair = config.Bind(
            "Networking",
            "DebugPlayerStateRepair",
            false,
            "Logs each vanilla connected-player state repair. Requires Logging.EnableDebugLogging for debug output.");

        ConfigEntry<bool> debugNetworkMessages = config.Bind(
            "Networking",
            "DebugNetworkMessages",
            false,
            "Enables verbose network message diagnostics.");

        ConfigEntry<bool> debugPoseMessages = config.Bind(
            "Networking",
            "DebugPoseMessages",
            false,
            "Logs high-frequency spectator pose observe/send/receive diagnostics. Keep disabled during normal testing.");

        ConfigEntry<bool> enableSpectatorPresenceDebug = config.Bind(
            "Presence",
            "EnableSpectatorPresenceDebug",
            true,
            "Enables debug-only inference of remote spectators watching the local player.");

        ConfigEntry<bool> debugLogPresenceChanges = config.Bind(
            "Presence",
            "DebugLogPresenceChanges",
            false,
            "Logs when a remote spectator starts or stops watching the local player. Requires Logging.EnableDebugLogging.");

        ConfigEntry<bool> enableModelInspection = config.Bind(
            "ModelInspection",
            "EnableModelInspection",
            false,
            "Enables key-triggered runtime player model hierarchy inspection.");

        ConfigEntry<bool> logLocalPlayerModelOnKey = config.Bind(
            "ModelInspection",
            "LogLocalPlayerModelOnKey",
            true,
            "Logs local player model information when the inspection key is pressed.");

        ConfigEntry<bool> logRemotePlayerModelsOnKey = config.Bind(
            "ModelInspection",
            "LogRemotePlayerModelsOnKey",
            true,
            "Logs remote player model information when the inspection key is pressed.");

        ConfigEntry<KeyCode> modelInspectionKey = config.Bind(
            "ModelInspection",
            "InspectionKey",
            KeyCode.F8,
            "Runs one runtime player model hierarchy inspection pass.");

        ConfigEntry<bool> includeRendererBounds = config.Bind(
            "ModelInspection",
            "IncludeRendererBounds",
            true,
            "Includes renderer world bounds in inspection logs.");

        ConfigEntry<bool> includeMaterials = config.Bind(
            "ModelInspection",
            "IncludeMaterials",
            false,
            "Includes material names in inspection logs. Disabled by default to keep logs smaller.");

        ConfigEntry<int> maxTransformDepth = config.Bind(
            "ModelInspection",
            "MaxTransformDepth",
            8,
            "Maximum transform depth scanned below each player root.");

        ConfigEntry<bool> enableRuntimeHeadSourceInspection = config.Bind(
            "HeadSourceInspection",
            "EnableRuntimeHeadSourceInspection",
            false,
            "Enables key-triggered runtime inspection of dead-body detached-head source candidates.");

        ConfigEntry<KeyCode> runtimeHeadSourceInspectionKey = config.Bind(
            "HeadSourceInspection",
            "InspectionKey",
            KeyCode.F10,
            "Runs one runtime detached-head source inspection pass.");

        ConfigEntry<bool> runtimeHeadSourceIncludeRendererBounds = config.Bind(
            "HeadSourceInspection",
            "IncludeRendererBounds",
            true,
            "Includes detached-head renderer world bounds in inspection logs.");

        ConfigEntry<bool> runtimeHeadSourceIncludeMaterials = config.Bind(
            "HeadSourceInspection",
            "IncludeMaterials",
            false,
            "Includes detached-head material names in inspection logs. Disabled by default to keep logs smaller.");

        ConfigEntry<int> runtimeHeadSourceMaxTransformDepth = config.Bind(
            "HeadSourceInspection",
            "MaxTransformDepth",
            6,
            "Maximum transform depth scanned below each detached-head object.");

        ConfigEntry<bool> enableVoiceDiagnostics = config.Bind(
            "VoiceDiagnostics",
            "EnableVoiceDiagnostics",
            false,
            "Enables key-triggered read-only voice diagnostics. This does not route, forward, or modify voice.");

        ConfigEntry<KeyCode> voiceDiagnosticsKey = config.Bind(
            "VoiceDiagnostics",
            "InspectionKey",
            KeyCode.F11,
            "Runs one voice diagnostics log pass.");

        ConfigEntry<bool> logLocalVoiceStateOnKey = config.Bind(
            "VoiceDiagnostics",
            "LogLocalVoiceStateOnKey",
            true,
            "Logs local player voice state when the diagnostics key is pressed.");

        ConfigEntry<bool> logRemoteVoiceStatesOnKey = config.Bind(
            "VoiceDiagnostics",
            "LogRemoteVoiceStatesOnKey",
            true,
            "Logs remote player voice states when the diagnostics key is pressed.");

        ConfigEntry<bool> includeVoiceAudioSourceDetails = config.Bind(
            "VoiceDiagnostics",
            "IncludeAudioSourceDetails",
            true,
            "Includes mapped AudioSource playback details in voice diagnostics logs.");

        ConfigEntry<bool> includeWalkieVoiceDiagnostics = config.Bind(
            "VoiceDiagnostics",
            "IncludeWalkieDiagnostics",
            true,
            "Includes walkie-talkie voice flags in voice diagnostics logs.");

        ConfigEntry<bool> enableFloatingHeadVisuals = config.Bind(
            "FloatingHead",
            "EnableFloatingHeadVisuals",
            true,
            "Enables local placeholder visuals for remote modded spectators watching the local player.");

        ConfigEntry<bool> enablePlaceholderVisuals = config.Bind(
            "FloatingHead",
            "EnablePlaceholderVisuals",
            true,
            "Creates simple runtime placeholder visuals instead of real head mesh clones.");

        ConfigEntry<bool> useRuntimeDetachedHeadVisuals = config.Bind(
            "FloatingHead",
            "UseRuntimeDetachedHeadVisuals",
            true,
            "Uses the loaded ghost-girl ragdoll detached-head template as a runtime-only marker source when available. Placeholder visuals remain the fallback when the runtime source is unavailable.");

        ConfigEntry<float> runtimeDetachedHeadScale = config.Bind(
            "FloatingHead",
            "RuntimeDetachedHeadScale",
            0.35f,
            "World scale multiplier applied to runtime detached-head visual clones.");

        ConfigEntry<float> runtimeDetachedHeadPitchOffset = config.Bind(
            "FloatingHead",
            "RuntimeDetachedHeadPitchOffset",
            FloatingHeadRotationRules.DefaultRuntimeDetachedHeadPitchOffsetDegrees,
            "Pitch correction in degrees applied after the remote spectator camera rotation for runtime detached-head visuals. Default matches the calibrated detached-head template orientation.");

        ConfigEntry<float> runtimeDetachedHeadYawOffset = config.Bind(
            "FloatingHead",
            "RuntimeDetachedHeadYawOffset",
            FloatingHeadRotationRules.DefaultRuntimeDetachedHeadYawOffsetDegrees,
            "Yaw correction in degrees applied after the remote spectator camera rotation for runtime detached-head visuals. Default matches the calibrated detached-head template orientation.");

        ConfigEntry<float> runtimeDetachedHeadRollOffset = config.Bind(
            "FloatingHead",
            "RuntimeDetachedHeadRollOffset",
            FloatingHeadRotationRules.DefaultRuntimeDetachedHeadRollOffsetDegrees,
            "Roll correction in degrees applied after the remote spectator camera rotation for runtime detached-head visuals. Default matches the calibrated detached-head template orientation.");

        ConfigEntry<bool> fallbackToPlaceholderWhenDetachedHeadUnavailable = config.Bind(
            "FloatingHead",
            "FallbackToPlaceholderWhenDetachedHeadUnavailable",
            true,
            "Falls back to placeholder visuals when runtime detached-head source data is unavailable.");

        ConfigEntry<bool> showRemoteSpectators = config.Bind(
            "FloatingHead",
            "ShowRemoteSpectators",
            true,
            "Shows remote modded players whenever they are in spectator state and a remote pose is available.");

        ConfigEntry<bool> showOnlySpectatorsWatchingMe = config.Bind(
            "FloatingHead",
            "ShowOnlySpectatorsWatchingMe",
            false,
            "When enabled, only shows remote spectators whose current target is the local player. When disabled, all remote spectators can be shown.");

        ConfigEntry<bool> showDeadSpectatorsToAlivePlayers = config.Bind(
            "FloatingHead",
            "ShowDeadSpectatorsToAlivePlayers",
            true,
            "Allows living local players to see remote spectator placeholders.");

        ConfigEntry<bool> showDeadSpectatorsToDeadPlayers = config.Bind(
            "FloatingHead",
            "ShowDeadSpectatorsToDeadPlayers",
            true,
            "Allows dead or spectating local players to see remote spectator placeholders.");

        ConfigEntry<int> maxFloatingHeadsVisible = config.Bind(
            "FloatingHead",
            "MaxFloatingHeadsVisible",
            8,
            "Maximum number of remote spectator placeholders visible at once. Set to 0 to hide all placeholders.");

        ConfigEntry<FloatingHeadVisualStyle> visualStyle = config.Bind(
            "FloatingHead",
            "VisualStyle",
            FloatingHeadVisualStyle.Sphere,
            "Runtime-only placeholder style: Sphere, Billboard, or Ring. Sphere is the default world-space marker style.");

        ConfigEntry<float> placeholderScale = config.Bind(
            "FloatingHead",
            "PlaceholderScale",
            0.18f,
            "World scale for each floating-head placeholder sphere.");

        ConfigEntry<float> billboardSize = config.Bind(
            "FloatingHead",
            "BillboardSize",
            0.22f,
            "World size for billboard and ring placeholder styles.");

        ConfigEntry<float> baseAlpha = config.Bind(
            "FloatingHead",
            "BaseAlpha",
            1.0f,
            "Placeholder material alpha where the runtime shader supports transparency.");

        ConfigEntry<bool> useUnlitMaterial = config.Bind(
            "FloatingHead",
            "UseUnlitMaterial",
            true,
            "Prefers an unlit runtime material for placeholder visibility.");

        ConfigEntry<bool> enableDepthTest = config.Bind(
            "FloatingHead",
            "EnableDepthTest",
            true,
            "Keeps normal depth testing for placeholders when the runtime shader supports it.");

        ConfigEntry<float> floatingHeadRingRadius = config.Bind(
            "FloatingHead",
            "RingRadius",
            0.45f,
            "Horizontal radius around the local head anchor used to distribute multiple placeholders.");

        ConfigEntry<float> floatingHeadHeightOffset = config.Bind(
            "FloatingHead",
            "HeightOffset",
            0.25f,
            "Vertical offset above the local head anchor for placeholder visuals.");

        ConfigEntry<bool> useCameraVisiblePlacement = config.Bind(
            "FloatingHead",
            "UseCameraVisiblePlacement",
            false,
            "Places fallback placeholders in front of the active camera. Diagnostic only; disabled by default so remote freecam poses stay in world space.");

        ConfigEntry<float> cameraForwardOffset = config.Bind(
            "FloatingHead",
            "CameraForwardOffset",
            1.15f,
            "Forward distance from the active camera when camera-visible placement is enabled.");

        ConfigEntry<float> remotePoseSmoothTime = config.Bind(
            "FloatingHead",
            "RemotePoseSmoothTime",
            0.08f,
            "Smooth damp time for remote spectator placeholder movement. Set to 0 to snap to received poses.");

        ConfigEntry<bool> keepRemotePoseInView = config.Bind(
            "FloatingHead",
            "KeepRemotePoseInView",
            false,
            "Projects remote spectator pose markers into the local camera view edge when the true remote pose is behind or outside the current view. Diagnostic only; disabled by default for strict world-space freecam following.");

        ConfigEntry<float> remotePoseVisibleProxyDistance = config.Bind(
            "FloatingHead",
            "RemotePoseVisibleProxyDistance",
            1.35f,
            "Camera-forward distance for visible proxy placement when a remote spectator pose is outside the local camera view.");

        ConfigEntry<bool> enableScreenFallbackVisual = config.Bind(
            "FloatingHead",
            "EnableScreenFallbackVisual",
            false,
            "Draws a runtime IMGUI fallback marker at the placeholder screen position when 3D marker rendering is unreliable. Intended for diagnostics only.");

        ConfigEntry<float> screenFallbackSize = config.Bind(
            "FloatingHead",
            "ScreenFallbackSize",
            48f,
            "Screen fallback marker diameter in pixels.");

        ConfigEntry<float> presenceLostGraceSeconds = config.Bind(
            "FloatingHead",
            "PresenceLostGraceSeconds",
            0.3f,
            "Keeps existing placeholder visuals alive briefly through transient empty presence frames. Disconnect and shutdown still clear immediately.");

        ConfigEntry<bool> floatingHeadFaceCamera = config.Bind(
            "FloatingHead",
            "FaceCamera",
            true,
            "Rotates placeholder visuals toward the local camera when possible.");

        ConfigEntry<bool> pulseWhenSpeaking = config.Bind(
            "FloatingHead",
            "PulseWhenSpeaking",
            true,
            "Pulses placeholder scale when local voice activity data says the remote spectator is speaking.");

        ConfigEntry<float> speakingScaleMultiplier = config.Bind(
            "FloatingHead",
            "SpeakingScaleMultiplier",
            1.65f,
            "Maximum visual scale multiplier while the represented spectator is speaking at high observed amplitude.");

        ConfigEntry<float> speakingPulseSpeed = config.Bind(
            "FloatingHead",
            "SpeakingPulseSpeed",
            8.0f,
            "Speed of the speaking visual pulse.");

        ConfigEntry<float> minimumSpeakingVoiceLevel = config.Bind(
            "FloatingHead",
            "MinimumSpeakingVoiceLevel",
            FloatingHeadVoiceScaleRules.DefaultMinimumSpeakingVoiceLevel,
            "Normalized fallback voice level used only when IsSpeaking is true and amplitude is unavailable or zero. Positive amplitude values are preserved so quiet syllables do not stay fully enlarged.");

        ConfigEntry<float> speakingPulseAmount = config.Bind(
            "FloatingHead",
            "SpeakingPulseAmount",
            FloatingHeadVoiceScaleRules.DefaultSpeakingPulseAmount,
            "Extra scale pulse amount applied while the represented spectator is speaking.");

        ConfigEntry<float> voiceAttackSmoothTime = config.Bind(
            "FloatingHead",
            "VoiceAttackSmoothTime",
            FloatingHeadVoiceScaleRules.DefaultVoiceAttackSmoothTime,
            "Smooth time used when voice activity starts. Lower values make the head enlarge faster.");

        ConfigEntry<float> voiceReleaseSmoothTime = config.Bind(
            "FloatingHead",
            "VoiceReleaseSmoothTime",
            FloatingHeadVoiceScaleRules.DefaultVoiceReleaseSmoothTime,
            "Smooth time used when voice activity stops. Lower values make the head shrink back faster.");

        ConfigEntry<float> silenceScaleMultiplier = config.Bind(
            "FloatingHead",
            "SilenceScaleMultiplier",
            1.0f,
            "Visual scale multiplier while silent or when no voice activity data is available.");

        ConfigEntry<float> amplitudeSmoothing = config.Bind(
            "FloatingHead",
            "AmplitudeSmoothing",
            0.08f,
            "Legacy voice activity smooth time retained for config compatibility. Current visual response uses VoiceAttackSmoothTime and VoiceReleaseSmoothTime.");

        ConfigEntry<bool> destroyOnPresenceLost = config.Bind(
            "FloatingHead",
            "DestroyOnPresenceLost",
            true,
            "Destroys placeholder visuals when remote spectator presence is lost.");

        ConfigEntry<bool> debugVisualLifecycle = config.Bind(
            "FloatingHead",
            "DebugVisualLifecycle",
            false,
            "Logs placeholder visual creation, destruction, and anchor loss diagnostics.");

        ConfigEntry<bool> showNameTags = config.Bind(
            "NameTag",
            "ShowNameTags",
            true,
            "Shows runtime-only fallback identity labels above floating-head placeholders.");

        ConfigEntry<float> nameTagScale = config.Bind(
            "NameTag",
            "NameTagScale",
            0.035f,
            "World-space character size for floating-head name tags.");

        ConfigEntry<float> nameTagHeightOffset = config.Bind(
            "NameTag",
            "NameTagHeightOffset",
            0.78f,
            "Vertical world offset above each floating-head placeholder.");

        ConfigEntry<float> nameTagMaxDistance = config.Bind(
            "NameTag",
            "NameTagMaxDistance",
            35f,
            "Maximum camera distance for rendering name tags. Set to 0 to disable distance culling.");

        ConfigEntry<bool> nameTagUseGamePlayerNames = config.Bind(
            "NameTag",
            "NameTagUseGamePlayerNames",
            true,
            "Uses the confirmed PlayerControllerB.playerUsername display name when available.");

        ConfigEntry<bool> nameTagUseFallbackIds = config.Bind(
            "NameTag",
            "NameTagUseFallbackIds",
            true,
            "Falls back to Client/Slot identifiers when the in-game player name is unavailable.");

        ConfigEntry<bool> debugNameTagLifecycle = config.Bind(
            "NameTag",
            "DebugNameTagLifecycle",
            false,
            "Reserved for verbose name tag diagnostics.");

        return new EnhancedSpectatorConfig(
            enableSpectatorModule,
            enableEnhancedSpectator,
            enableFreecam,
            freecamDefaultOn,
            freecamRadius,
            freecamMoveSpeed,
            freecamFastMoveMultiplier,
            freecamSlowMoveMultiplier,
            freecamLookSensitivity,
            freecamSmoothTime,
            clampCameraToRadius,
            recenterOnTargetSwitch,
            disableDuringGameOverOverride,
            toggleFreecamKey,
            recenterKey,
            resetToVanillaViewKey,
            fastMoveKey,
            slowMoveKey,
            ascendKey,
            descendKey,
            enableDebugLogging,
            enableNetworking,
            enableCapabilityHandshake,
            enableSpectatorTargetSync,
            enableSpectatorPoseSync,
            enableHostRelay,
            spectatorPoseSyncInterval,
            enableVoiceActivitySync,
            voiceActivitySyncInterval,
            voiceActivityStaleSeconds,
            debugVoiceActivitySync,
            enableSpectatorVoiceToTarget,
            spectatorVoiceAudienceMode,
            spectatorVoiceToTargetVolume,
            spectatorVoiceUseRemotePosePosition,
            spectatorVoiceEnableDistanceAttenuation,
            spectatorVoiceMinDistance,
            spectatorVoiceMaxDistance,
            spectatorVoiceRolloffPower,
            spectatorVoiceMinimumVolume,
            spectatorVoiceFallbackTo2DWhenPoseMissing,
            debugSpectatorVoiceRouting,
            repairVanillaConnectedPlayerState,
            repairVanillaPlayerNames,
            debugPlayerStateRepair,
            debugNetworkMessages,
            debugPoseMessages,
            enableSpectatorPresenceDebug,
            debugLogPresenceChanges,
            enableModelInspection,
            logLocalPlayerModelOnKey,
            logRemotePlayerModelsOnKey,
            modelInspectionKey,
            includeRendererBounds,
            includeMaterials,
            maxTransformDepth,
            enableRuntimeHeadSourceInspection,
            runtimeHeadSourceInspectionKey,
            runtimeHeadSourceIncludeRendererBounds,
            runtimeHeadSourceIncludeMaterials,
            runtimeHeadSourceMaxTransformDepth,
            enableVoiceDiagnostics,
            voiceDiagnosticsKey,
            logLocalVoiceStateOnKey,
            logRemoteVoiceStatesOnKey,
            includeVoiceAudioSourceDetails,
            includeWalkieVoiceDiagnostics,
            enableFloatingHeadVisuals,
            enablePlaceholderVisuals,
            useRuntimeDetachedHeadVisuals,
            runtimeDetachedHeadScale,
            runtimeDetachedHeadPitchOffset,
            runtimeDetachedHeadYawOffset,
            runtimeDetachedHeadRollOffset,
            fallbackToPlaceholderWhenDetachedHeadUnavailable,
            showRemoteSpectators,
            showOnlySpectatorsWatchingMe,
            showDeadSpectatorsToAlivePlayers,
            showDeadSpectatorsToDeadPlayers,
            maxFloatingHeadsVisible,
            visualStyle,
            placeholderScale,
            billboardSize,
            baseAlpha,
            useUnlitMaterial,
            enableDepthTest,
            floatingHeadRingRadius,
            floatingHeadHeightOffset,
            useCameraVisiblePlacement,
            cameraForwardOffset,
            remotePoseSmoothTime,
            keepRemotePoseInView,
            remotePoseVisibleProxyDistance,
            enableScreenFallbackVisual,
            screenFallbackSize,
            presenceLostGraceSeconds,
            floatingHeadFaceCamera,
            pulseWhenSpeaking,
            speakingScaleMultiplier,
            speakingPulseSpeed,
            minimumSpeakingVoiceLevel,
            speakingPulseAmount,
            voiceAttackSmoothTime,
            voiceReleaseSmoothTime,
            silenceScaleMultiplier,
            amplitudeSmoothing,
            destroyOnPresenceLost,
            debugVisualLifecycle,
            showNameTags,
            nameTagScale,
            nameTagHeightOffset,
            nameTagMaxDistance,
            nameTagUseGamePlayerNames,
            nameTagUseFallbackIds,
            debugNameTagLifecycle);
    }
}
