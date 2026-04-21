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
            Faction = Faction.Chaos,
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
            Faction = Faction.Arcane,
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
            Faction = Faction.Arcane,
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
            Faction = Faction.Order,
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
            Faction = Faction.Fury,
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
            Faction = Faction.Arcane,
            BaseHp = 110,
            BaseAttack = 18,
            BaseDefence = 20
        }
    ];

    public List<SpinHero> GetSpinPool() =>
    [
        // Common (high weight)
        new SpinHero
        {
            Id = "shadow_knight", Name = "Shadow Knight",
            IconImage = "dotnet_bot.png", FullImage = "dotnet_bot.png",
            Faction = Faction.Chaos, Rank = HeroRank.Rare,
            BaseHp = 120, BaseAttack = 25, BaseDefence = 15,
            Weight = 40
        },
        new SpinHero
        {
            Id = "iron_guardian", Name = "Iron Guardian",
            IconImage = "dotnet_bot.png", FullImage = "dotnet_bot.png",
            Faction = Faction.Order, Rank = HeroRank.Rare,
            BaseHp = 150, BaseAttack = 15, BaseDefence = 25,
            Weight = 40
        },
        // Uncommon
        new SpinHero
        {
            Id = "forest_druid", Name = "Forest Druid",
            IconImage = "dotnet_bot.png", FullImage = "dotnet_bot.png",
            Faction = Faction.Arcane, Rank = HeroRank.Elite,
            BaseHp = 90, BaseAttack = 30, BaseDefence = 8,
            Weight = 20
        },
        new SpinHero
        {
            Id = "frost_healer", Name = "Frost Healer",
            IconImage = "dotnet_bot.png", FullImage = "dotnet_bot.png",
            Faction = Faction.Arcane, Rank = HeroRank.Elite,
            BaseHp = 110, BaseAttack = 18, BaseDefence = 20,
            Weight = 20
        },
        // Rare
        new SpinHero
        {
            Id = "fire_mage", Name = "Fire Mage",
            IconImage = "dotnet_bot.png", FullImage = "dotnet_bot.png",
            Faction = Faction.Arcane, Rank = HeroRank.Epic,
            BaseHp = 80, BaseAttack = 40, BaseDefence = 6,
            Weight = 8
        },
        // Very Rare
        new SpinHero
        {
            Id = "storm_archer", Name = "Storm Archer",
            IconImage = "dotnet_bot.png", FullImage = "dotnet_bot.png",
            Faction = Faction.Fury, Rank = HeroRank.Legendary,
            BaseHp = 95, BaseAttack = 35, BaseDefence = 12,
            Weight = 2
        }
    ];
}
