# Mythfall

## Overview
Mythfall is a hero/monster collection game built with .NET MAUI. Players unlock, collect, and power up heroes and monsters to build their ultimate roster.

## Game Concepts

### Heroes & Monsters
- Players unlock heroes and monsters through gameplay
- Each hero/monster can be leveled up to increase their stats
- Duplicate heroes/monsters are used to **rank up** units

### Ranking System
Units progress through four ranks by consuming duplicates:
1. **Rare** — Base rank when first obtained
2. **Elite** — First rank-up, requires duplicates
3. **Epic** — Second rank-up, requires more duplicates
4. **Legendary** — Final rank, maximum power

### Leveling
- Heroes/monsters gain experience to level up
- Each rank increases the maximum level cap
- Higher levels increase base stats

## Project Structure

```
Mythfall/
  src/
    Mythfall.Client/          # .NET MAUI client app
      Pages/                  # App pages (Splash, Home, etc.)
      Resources/              # Images, fonts, styles
      Platforms/              # Platform-specific code
```

## Tech Stack
- **.NET MAUI** — Cross-platform UI (Android, iOS, Windows, Mac)
- **C# / XAML** — Language and markup
- **Target:** net10.0-android, net10.0-ios, net10.0-maccatalyst, net10.0-windows

## Build & Run
```bash
# Build
dotnet build src/Mythfall.Client/Mythfall.Client.csproj

# Run on Windows
dotnet run --project src/Mythfall.Client/Mythfall.Client.csproj -f net10.0-windows10.0.19041.0

# Run on Android emulator
dotnet run --project src/Mythfall.Client/Mythfall.Client.csproj -f net10.0-android
```

## Conventions
- Pages go in `Pages/` folder under the client project
- Use Shell navigation for routing between pages
- Follow MVVM pattern as the app grows
- Keep platform-specific code in `Platforms/` folders

