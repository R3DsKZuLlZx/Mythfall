namespace Mythfall.Client.Models;

public class SpinHero
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string IconImage { get; set; } = "dotnet_bot.png";
    public string FullImage { get; set; } = "dotnet_bot.png";
    public Faction Faction { get; set; } = Faction.Order;
    public HeroRank Rank { get; set; } = HeroRank.Rare;
    public int BaseHp { get; set; } = 100;
    public int BaseAttack { get; set; } = 20;
    public int BaseDefence { get; set; } = 10;

    /// <summary>
    /// Relative weight for the spin. Higher = more common.
    /// e.g. weight 50 vs weight 5 means ~10x more likely.
    /// </summary>
    public double Weight { get; set; } = 1;

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

    /// <summary>
    /// Convert to a playable Hero instance (level 1).
    /// </summary>
    public Hero ToHero() => new()
    {
        Id = Id,
        Name = Name,
        IconImage = IconImage,
        FullImage = FullImage,
        Faction = Faction,
        Rank = Rank,
        Level = 1,
        BaseHp = BaseHp,
        BaseAttack = BaseAttack,
        BaseDefence = BaseDefence
    };
}

