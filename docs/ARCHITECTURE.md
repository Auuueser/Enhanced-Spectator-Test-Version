# Architecture

Enhanced Spectator is organized around small modules with explicit lifecycle methods.

## Plugin

The BepInEx plugin entry point initializes logging, configuration, feature modules, runtime tick forwarding, and Harmony patch registration.

## Feature Modules

Feature modules own runtime behavior and are initialized through the feature bootstrapper.

Current feature areas:

- Spectator freecam.
- Spectator presence.
- Floating-head and name-tag visuals.
- Voice activity and voice routing.
- Model, head-source, and voice diagnostics.
- Connected-player state repair for known modded-peer lifecycle edge cases.

## Game Interop

Game API access is isolated in `GameInterop` adapters. Feature modules should consume adapter interfaces instead of scattering direct game-field access.

## Networking

Networking uses Unity Netcode custom messaging between compatible modded peers.

The network layer owns:

- Capability handshake.
- Spectator target state.
- Spectator pose state.
- Voice activity state.
- Peer identity state.
- Host-mediated relay where required for client-to-client visibility.

Networking does not directly create gameplay or visual objects.

## Visuals

Floating spectator visuals are runtime-only local objects. They are not `NetworkObject`s and are not saved as assets.

Name tags are runtime-only text objects attached to mod-owned visual roots.

## Voice

Voice routing is configurable and only applies between compatible modded peers. It uses existing game/Dissonance playback state when safe and restores local playback when routes are cleared.

The mod does not ship custom audio assets or open new voice channels.

## Configuration and Logging

Configuration entries live in the config layer. Logging goes through the shared logging helper and verbose diagnostics are gated behind config switches.
