namespace EnhancedSpectator.Features.FloatingHead;

/// <summary>
/// Pure rules for choosing remote spectator voice activity sources for visual scaling.
/// </summary>
public static class FloatingHeadVoiceActivityRules
{
    /// <summary>
    /// Gets whether visuals may fall back to locally observed playback state.
    /// </summary>
    public static bool ShouldUseLocalFallback(bool voiceActivitySyncEnabled, bool hasNetworkService)
    {
        return !voiceActivitySyncEnabled || !hasNetworkService;
    }
}
