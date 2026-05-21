namespace EnhancedSpectator.Networking;

/// <summary>
/// Evaluates peer capability compatibility for current spectator networking features.
/// </summary>
public static class ModPeerCapabilityRules
{
    /// <summary>
    /// Gets whether a peer can participate in the current spectator target and pose sync protocol.
    /// </summary>
    public static bool SupportsCurrentSpectatorTargetSync(ModPeerCapability capability)
    {
        return capability.ProtocolVersion == ModNetworkConstants.ProtocolVersion
            && capability.SupportsCapabilityHandshake
            && capability.SupportsSpectatorTargetSync
            && capability.HandshakeComplete;
    }

    /// <summary>
    /// Gets whether a peer can receive visual-only voice activity sync messages.
    /// </summary>
    public static bool SupportsCurrentVoiceActivitySync(ModPeerCapability capability)
    {
        return SupportsCurrentSpectatorTargetSync(capability)
            && capability.SupportsVoiceActivitySync;
    }

    /// <summary>
    /// Gets whether a peer opts into spectator-to-target voice playback.
    /// </summary>
    public static bool SupportsCurrentSpectatorVoiceToTarget(ModPeerCapability capability)
    {
        return SupportsCurrentSpectatorTargetSync(capability)
            && capability.SupportsSpectatorVoiceToTarget;
    }
}
