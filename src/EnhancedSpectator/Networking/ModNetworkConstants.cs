namespace EnhancedSpectator.Networking;

/// <summary>
/// Constants for Enhanced Spectator mod-owned networking.
/// </summary>
public static class ModNetworkConstants
{
    /// <summary>
    /// Current mod network protocol version.
    /// </summary>
    public const int ProtocolVersion = 1;

    /// <summary>
    /// Named-message id for capability handshake messages.
    /// </summary>
    public const string CapabilityMessageName = "EnhancedSpectator.Capability.V1";

    /// <summary>
    /// Named-message id for spectator target state messages.
    /// </summary>
    public const string SpectatorTargetMessageName = "EnhancedSpectator.SpectatorTarget.V1";

    /// <summary>
    /// Named-message id for spectator camera pose messages.
    /// </summary>
    public const string SpectatorPoseMessageName = "EnhancedSpectator.SpectatorPose.V1";

    /// <summary>
    /// Named-message id for peer display-name identity messages.
    /// </summary>
    public const string PeerIdentityMessageName = "EnhancedSpectator.PeerIdentity.V1";

    /// <summary>
    /// Named-message id for voice activity visual sync messages.
    /// </summary>
    public const string VoiceActivityMessageName = "EnhancedSpectator.VoiceActivity.V1";

    /// <summary>
    /// Minimum interval between target sync sends.
    /// </summary>
    public const double TargetSyncMinIntervalSeconds = 0.1d;

    /// <summary>
    /// Minimum interval between voice activity visual sync sends.
    /// </summary>
    public const double VoiceActivitySyncMinIntervalSeconds = 0.066d;
}
