namespace EnhancedSpectator.Networking;

/// <summary>
/// Describes the current lifecycle state of the mod-owned network transport.
/// </summary>
public enum NetworkLifecycleState
{
    /// <summary>
    /// Networking is not available in the current process state.
    /// </summary>
    Unavailable,

    /// <summary>
    /// Networking is intentionally disabled or degraded without affecting local freecam.
    /// </summary>
    LocalOnly,

    /// <summary>
    /// Named message handlers are registered and ready.
    /// </summary>
    TransportRegistered,

    /// <summary>
    /// The networking service has been disposed.
    /// </summary>
    Disposed,
}
