# Usage

Enhanced Spectator adds local spectator freecam, compatible-player spectator visibility, floating spectator visuals, name tags, voice-activity visual feedback, and configurable dead-spectator voice routing.

Vanilla Lethal Company still owns death state, spectator target selection, connection approval, and round lifecycle.

## Controls

| Action | Default |
| --- | --- |
| Toggle enhanced freecam | `F6` |
| Recenter near current target | `R` |
| Return to vanilla spectator view | `F7` |
| Move | `W` / `A` / `S` / `D` |
| Move up / down | `Space` / `LeftControl` |
| Look | Mouse |
| Fast movement | Hold `LeftShift` |
| Slow movement | Hold `LeftAlt` |

Forward and backward movement follows the camera look direction.

## Common Configuration

The config file is generated after first launch:

```text
BepInEx/config/Auuueser.EnhancedSpectator.cfg
```

Common options:

| Entry | Default | Purpose |
| --- | --- | --- |
| `Spectator.Freecam.EnableFreecam` | `true` | Enables local spectator freecam. |
| `Spectator.Freecam.FreecamRadius` | `8.0` | Maximum offset from the current target anchor. |
| `Spectator.Freecam.FreecamMoveSpeed` | `4.0` | Base movement speed. |
| `Networking.EnableNetworking` | `true` | Enables mod-owned networking. |
| `Networking.EnableHostRelay` | `true` | Enables host relay for compatible client-to-client visibility. |
| `FloatingHead.EnableFloatingHeadVisuals` | `true` | Enables local spectator visual markers. |
| `FloatingHead.UseRuntimeDetachedHeadVisuals` | `true` | Enables runtime detached-head visuals when the runtime source is available. Placeholder visuals remain the fallback when unavailable. |
| `FloatingHead.ShowRemoteSpectators` | `true` | Shows compatible remote spectators with synced pose data. |
| `NameTag.ShowNameTags` | `true` | Shows name tags above spectator visuals. |
| `VoiceRouting.EnableSpectatorVoiceToTarget` | `true` | Enables compatible dead-spectator voice routing. |
| `VoiceRouting.SpectatorVoiceAudienceMode` | `AllModdedPlayers` | Selects who can hear routed spectator voice. |
| `VoiceRouting.SpectatorVoiceUseRemotePosePosition` | `true` | Places routed voice at the synced spectator pose. |
| `VoiceRouting.SpectatorVoiceEnableDistanceAttenuation` | `true` | Applies distance attenuation to routed spectator voice. |
| `Logging.EnableDebugLogging` | `false` | Enables verbose diagnostic logs. |

Keep debug options disabled for normal play.

## Multiplayer Behavior

- Local freecam works independently.
- Floating-head visuals, name tags, voice-activity scaling, and routed spectator voice require compatible Enhanced Spectator peers.
- Cross-client spectator visibility requires a compatible modded host with host relay enabled.
- Unmodded hosts and unmodded peers safely fall back to local-only behavior.

## Known Limits

- This is a public test release.
- Runtime detached-head visuals are runtime-only and do not export, save, or package game assets.
- The mod does not create new voice channels or ship audio assets.
- Positional spectator voice depends on synced spectator pose data.
- Other camera, spectator, or voice mods may conflict when they modify the same runtime systems.
