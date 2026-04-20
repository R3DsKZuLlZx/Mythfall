# Mythfall

Mythfall is a hero and monster collection game built with .NET MAUI. Players unlock, collect, and power up heroes and monsters to build their ultimate roster. The project is cross-platform and targets Android, iOS, Windows, and Mac.

## Features
- Unlock and collect unique heroes and monsters
- Level up units to increase their stats
- Use duplicate units to rank up from Rare → Elite → Epic → Legendary
- Modern .NET MAUI UI with animated splash screen

## Game Concepts
### Heroes & Monsters
- Unlockable through gameplay
- Level up to increase stats
- Duplicates are used for ranking up

### Ranking System
1. **Rare** — Base rank
2. **Elite** — First rank-up (requires duplicates)
3. **Epic** — Second rank-up (requires more duplicates)
4. **Legendary** — Final rank, maximum power

### Leveling
- Units gain experience to level up
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

## Getting Started
### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/)
- Visual Studio 2022+ with MAUI workload

### Build
```powershell
dotnet build src/Mythfall.Client/Mythfall.Client.csproj
```

### Run
- **Windows:**
  ```powershell
  dotnet run --project src/Mythfall.Client/Mythfall.Client.csproj -f net10.0-windows10.0.19041.0
  ```
- **Android Emulator:**
  ```powershell
  dotnet run --project src/Mythfall.Client/Mythfall.Client.csproj -f net10.0-android
  ```

## Conventions
- Pages go in `Pages/` folder under the client project
- Use Shell navigation for routing between pages
- Follow MVVM pattern as the app grows
- Keep platform-specific code in `Platforms/` folders

## License
MIT License

