namespace EnhancedSpectator.Features.VoiceActivity;

/// <summary>
/// Voice activity provider used when real voice state is unavailable.
/// </summary>
public sealed class NoopVoiceActivityProvider : IVoiceActivityProvider
{
    /// <summary>
    /// Gets the singleton no-op provider.
    /// </summary>
    public static NoopVoiceActivityProvider Instance { get; } = new NoopVoiceActivityProvider();

    private NoopVoiceActivityProvider()
    {
    }

    /// <inheritdoc />
    public bool TryGetVoiceActivity(ulong clientId, ulong slotId, out VoiceActivityState state)
    {
        _ = clientId;
        _ = slotId;
        state = VoiceActivityState.NoData;
        return false;
    }
}
