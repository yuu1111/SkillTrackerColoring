# SkillTrackerColoring

A BepInEx mod for [Elin](https://store.steampowered.com/app/2135150/Elin/)

Colors skill names in the Skill Tracker widget based on their corresponding primary attributes.

---

## Features

- Color-codes skills by their primary attribute
- Customizable colors via BepInEx Config
- Automatically adapts to skill name changes from game updates

## Default Colors

| Attribute | Color |
|-----------|-------|
| Strength (STR) | Red |
| Endurance (END) | Brown |
| Dexterity (DEX) | Green |
| Perception (PER) | Light Blue |
| Learning (LER) | Blue |
| Will (WIL) | Blue-Purple |
| Magic (MAG) | Red-Purple |
| Charisma (CHA) | Orange |

## Installation

Just subscribe to [SkillTrackerColoring](https://steamcommunity.com/sharedfiles/filedetails/?id=3362885911) from Steam Workshop

## Color Customization

After launching the game, you can modify colors in:

```
BepInEx/config/com.github.yuu1111.skilltrackercoloring.cfg
```

Colors are specified in `#RRGGBB` format.

## Development and verification

Release builds are verified against the installed Elin Stable 23.355 assemblies and BepInEx 6.x. BepInEx is not automatically upgraded, and build outputs are not deployed to the game. Invalid color settings fall back to the attribute default.

```sh
dotnet build SkillTrackerColoring.csproj -c Release
dotnet run --project tests/SkillTrackerColoring.Tests.csproj -c Release
```

The dependency-free .NET 8 tests exercise the formatter. In-game Harmony patching and rendering still require manual verification.

## Changelog

See [Changelog.md](resouce/Changelog.md)

## License

[MIT License](LICENCE)
