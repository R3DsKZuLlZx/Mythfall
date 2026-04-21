using CommunityToolkit.Mvvm.ComponentModel;

namespace Mythfall.Client.Models;

public partial class Hero : ObservableObject
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string IconImage { get; set; } = "dotnet_bot.png";
    public string FullImage { get; set; } = "dotnet_bot.png";
    public Faction Faction { get; set; } = Faction.Order;

    [ObservableProperty]
    private HeroRank rank = HeroRank.Rare;

    [ObservableProperty]
    private int level = 1;

    [ObservableProperty]
    private int baseHp = 100;

    [ObservableProperty]
    private int baseAttack = 20;

    [ObservableProperty]
    private int baseDefence = 10;

    public int MaxLevel => Rank switch
    {
        HeroRank.Rare => 20,
        HeroRank.Elite => 40,
        HeroRank.Epic => 60,
        HeroRank.Legendary => 80,
        _ => 20
    };

    public int HP => BaseHp + (Level - 1) * 5;
    public int Attack => BaseAttack + (Level - 1) * 2;
    public int Defence => BaseDefence + (Level - 1) * 1;

    public int GoldCostToLevel => Level * 10;
    public int ExpCostToLevel => Level * 5;
    public int SpecialExpCostToLevel => (Level % 5 == 0) ? 1 : 0;
    public bool NeedsSpecialExp => Level % 5 == 4; // next level is multiple of 5

    public string RankColor => Rank switch
    {
        HeroRank.Rare => "#4488cc",
        HeroRank.Elite => "#9944cc",
        HeroRank.Epic => "#cc8800",
        HeroRank.Legendary => "#ee3333",
        _ => "#4488cc"
    };

    public string FactionIcon => Faction switch
    {
        Faction.Order => "icon_order.png",
        Faction.Chaos => "icon_chaos.png",
        Faction.Arcane => "icon_arcane.png",
        Faction.Fury => "icon_fury.png",
        _ => "icon_order.png"
    };

    public string FactionColor => Faction switch
    {
        Faction.Order => "#4488ee",
        Faction.Chaos => "#ee4422",
        Faction.Arcane => "#aa44ee",
        Faction.Fury => "#ee8822",
        _ => "#4488ee"
    };

    public void RefreshStats()
    {
        OnPropertyChanged(nameof(HP));
        OnPropertyChanged(nameof(Attack));
        OnPropertyChanged(nameof(Defence));
        OnPropertyChanged(nameof(GoldCostToLevel));
        OnPropertyChanged(nameof(ExpCostToLevel));
        OnPropertyChanged(nameof(SpecialExpCostToLevel));
        OnPropertyChanged(nameof(NeedsSpecialExp));
        OnPropertyChanged(nameof(MaxLevel));
        OnPropertyChanged(nameof(RankColor));
    }
}
