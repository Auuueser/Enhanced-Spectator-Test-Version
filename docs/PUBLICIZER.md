# Publicizer

Enhanced Spectator uses `BepInEx.AssemblyPublicizer.MSBuild` for direct access to confirmed game members when needed.

## Why

Runtime reflection is avoided in production code. Confirmed non-public game members should be accessed through publicized game assemblies and direct member access instead of reflection helpers.

## Build Configuration

The project defines:

```xml
<GameDir>D:\Steam\steamapps\common\Lethal Company</GameDir>
<ManagedDir>$(GameDir)\Lethal Company_Data\Managed</ManagedDir>
```

Game assembly references use `Condition="Exists(...)"` so package restore can still run on machines without the local game path.

`Assembly-CSharp.dll` is marked with `Publicize="true"`.

## Override GameDir

```powershell
dotnet build -p:GameDir="D:\Steam\steamapps\common\Lethal Company"
```

Use your own local install path when different.

## Restrictions

Production code must not use reflection helpers to read or write Lethal Company members. Do not use `AccessTools`, `Traverse`, `BindingFlags`, `GetField`, `GetMethod`, or reflection metadata APIs for game-member access.
