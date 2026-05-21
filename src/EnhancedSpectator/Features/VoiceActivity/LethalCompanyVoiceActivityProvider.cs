using System;
using EnhancedSpectator.GameInterop;

namespace EnhancedSpectator.Features.VoiceActivity;

/// <summary>
/// Reads local Dissonance voice activity mapped to Lethal Company player scripts.
/// </summary>
public sealed class LethalCompanyVoiceActivityProvider : IVoiceActivityProvider
{
    private readonly IGameVoiceActivityAdapter _adapter;

    /// <summary>
    /// Creates a voice activity provider using the Lethal Company game adapter.
    /// </summary>
    public LethalCompanyVoiceActivityProvider()
        : this(new LethalCompanyVoiceActivityAdapter())
    {
    }

    /// <summary>
    /// Creates a voice activity provider with an explicit game adapter.
    /// </summary>
    public LethalCompanyVoiceActivityProvider(IGameVoiceActivityAdapter adapter)
    {
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
    }

    /// <inheritdoc />
    public bool TryGetVoiceActivity(ulong clientId, ulong slotId, out VoiceActivityState state)
    {
        return _adapter.TryGetVoiceActivity(clientId, slotId, out state);
    }
}
