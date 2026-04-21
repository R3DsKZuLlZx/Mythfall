using CommunityToolkit.Mvvm.ComponentModel;

namespace Mythfall.Client.Models;

/// <summary>
/// A snapshot of a hero used during combat. Has mutable CurrentHp that depletes during the fight.
/// </summary>
public partial class CombatHero : ObservableObject
{
    public string Name { get; set; } = string.Empty;
    public string IconImage { get; set; } = "dotnet_bot.png";
    public Faction Faction { get; set; }
    public HeroRank Rank { get; set; }
    public int Level { get; set; }

    public int MaxHp { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }

    [ObservableProperty]
    private int currentHp;

    [ObservableProperty]
    private bool isDefeated;

    [ObservableProperty]
    private string lastDamageText = string.Empty;

    public bool IsPlayerSide { get; set; }

    public string RankColor => Rank switch
    {
        HeroRank.Rare => "#4488cc",
        HeroRank.Elite => "#9944cc",
        HeroRank.Epic => "#cc8800",
        HeroRank.Legendary => "#ee3333",
        _ => "#4488cc"
    };

    public double HpPercent => MaxHp > 0 ? (double)CurrentHp / MaxHp : 0;

    public void TakeDamage(int damage)
    {
        var actual = Math.Max(1, damage);
        CurrentHp = Math.Max(0, CurrentHp - actual);
        LastDamageText = $"-{actual}";
        OnPropertyChanged(nameof(HpPercent));

        if (CurrentHp <= 0)
            IsDefeated = true;
    }

    public static CombatHero FromHero(Hero hero, bool isPlayer) => new()
    {
        Name = hero.Name,
        IconImage = hero.IconImage,
        Faction = hero.Faction,
        Rank = hero.Rank,
        Level = hero.Level,
        MaxHp = hero.HP,
        Attack = hero.Attack,
        Defence = hero.Defence,
        CurrentHp = hero.HP,
        IsPlayerSide = isPlayer
    };
}

