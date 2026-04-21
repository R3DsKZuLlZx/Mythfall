using Mythfall.Client.Models;

namespace Mythfall.Client.Services;

public class HeroService
{
    public PlayerResources Resources { get; } = new();

    public List<Hero> GetHeroes() =>
    [
        new Hero
        {
            Id = "shadow_knight",
            Name = "Shadow Knight",
            IconImage = "dotnet_bot.png",
            FullImage = "dotnet_bot.png",
            Rank = HeroRank.Rare,
            Level = 1,
            BaseHp = 120,
            BaseAttack = 25,
            BaseDefence = 15
        },
        new Hero
        {
            Id = "forest_druid",
            Name = "Forest Druid",
            IconImage = "dotnet_bot.png",
            FullImage = "dotnet_bot.png",
            Rank = HeroRank.Elite,
            Level = 5,
            BaseHp = 90,
            BaseAttack = 30,
            BaseDefence = 8
        },
        new Hero
        {
            Id = "fire_mage",
            Name = "Fire Mage",
            IconImage = "dotnet_bot.png",
            FullImage = "dotnet_bot.png",
            Rank = HeroRank.Epic,
            Level = 12,
            BaseHp = 80,
            BaseAttack = 40,
            BaseDefence = 6
        },
        new Hero
        {
            Id = "iron_guardian",
            Name = "Iron Guardian",
            IconImage = "dotnet_bot.png",
            FullImage = "dotnet_bot.png",
            Rank = HeroRank.Rare,
            Level = 3,
            BaseHp = 150,
            BaseAttack = 15,
            BaseDefence = 25
        },
        new Hero
        {
            Id = "storm_archer",
            Name = "Storm Archer",
            IconImage = "dotnet_bot.png",
            FullImage = "dotnet_bot.png",
            Rank = HeroRank.Legendary,
            Level = 20,
            BaseHp = 95,
            BaseAttack = 35,
            BaseDefence = 12
        },
        new Hero
        {
            Id = "frost_healer",
            Name = "Frost Healer",
            IconImage = "dotnet_bot.png",
            FullImage = "dotnet_bot.png",
            Rank = HeroRank.Elite,
            Level = 8,
            BaseHp = 110,
            BaseAttack = 18,
            BaseDefence = 20
        }
    ];
}

