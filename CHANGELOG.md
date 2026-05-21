# Changelog

## 0.1.1

### Visuals

- Runtime detached-head spectator visuals are now enabled by default.
- Placeholder sphere visuals remain available as the fallback when the runtime head source is unavailable.

## 0.1.0

Initial public test release.

### Spectator Freecam

- Client-local enhanced spectator freecam with configurable movement, recenter, reset, and speed controls.
- Camera movement around the current vanilla spectator target with radius limiting.
- Configurable toggle, recenter, reset, fast-move, and slow-move controls.

### Multiplayer Presence

- Modded-peer capability handshake.
- Spectator target sync and spectator pose sync.
- Host-mediated relay for compatible client-to-client spectator visibility.
- Remote and dead spectator visibility for compatible modded peers.
- Peer identity sync for spectator name tags.

### Visuals

- Runtime floating-head spectator visuals.
- Runtime name tags above spectator visuals.
- Runtime detached-head visual mode with placeholder fallback.
- Voice-activity driven head scale and pulse.

### Voice

- Configurable dead-spectator voice routing for compatible peers.
- Positional spectator voice based on synced spectator pose.
- Distance attenuation for routed spectator voice.

### Diagnostics

- Debug and diagnostic controls for networking, player model inspection, head-source inspection, voice diagnostics, and visual lifecycle checks.
