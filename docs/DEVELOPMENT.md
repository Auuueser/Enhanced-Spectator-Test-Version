# Development

## Requirements

- .NET SDK 8 or newer.
- Local Lethal Company install for game assembly references.
- BepInEx runtime for manual in-game testing.

## Build

```powershell
dotnet restore
dotnet build -p:GameDir="D:\Steam\steamapps\common\Lethal Company"
```

The `GameDir` value is an example. Use the path to your local Lethal Company installation.

## Tests

```powershell
dotnet run --project tests\EnhancedSpectator.Tests\EnhancedSpectator.Tests.csproj --no-restore
```

## Repository Safety

- Do not commit game DLLs.
- Do not commit decompiled game files.
- Do not commit Unity assets, prefabs, materials, meshes, bundles, or BepInEx runtime contents.
- Do not commit logs or local mod-manager profile files.
- Do not use reflection to access Lethal Company members.

Game-specific access should stay isolated in `GameInterop` adapters.
