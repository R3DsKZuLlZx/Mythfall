using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

[QueryProperty(nameof(Hero), "Hero")]
public partial class HeroDetailViewModel : ObservableObject
{
    private readonly HeroService _heroService;

    [ObservableProperty]
    private Hero? hero;

    [ObservableProperty]
    private string levelUpMessage = string.Empty;

    [ObservableProperty]
    private bool canLevelUp;

    public PlayerResources Resources => _heroService.Resources;

    public HeroDetailViewModel(HeroService heroService)
    {
        _heroService = heroService;
    }

    partial void OnHeroChanged(Hero? value)
    {
        UpdateCanLevelUp();
    }

    private void UpdateCanLevelUp()
    {
        if (Hero is null)
        {
            CanLevelUp = false;
            return;
        }

        if (Hero.Level >= Hero.MaxLevel)
        {
            LevelUpMessage = "Max level reached!";
            CanLevelUp = false;
            return;
        }

        var gold = Hero.GoldCostToLevel;
        var exp = Hero.ExpCostToLevel;
        var specialExp = Hero.NeedsSpecialExp ? 1 : 0;

        var canAfford = Resources.Gold >= gold
                     && Resources.Experience >= exp
                     && Resources.SpecialExperience >= specialExp;

        CanLevelUp = canAfford;
        LevelUpMessage = canAfford ? string.Empty : "Not enough resources!";
    }

    [RelayCommand]
    private void LevelUp()
    {
        if (Hero is null || !CanLevelUp) return;

        var gold = Hero.GoldCostToLevel;
        var exp = Hero.ExpCostToLevel;
        var specialExp = Hero.NeedsSpecialExp ? 1 : 0;

        Resources.Gold -= gold;
        Resources.Experience -= exp;
        Resources.SpecialExperience -= specialExp;

        Hero.Level++;
        Hero.RefreshStats();

        OnPropertyChanged(nameof(Hero));
        UpdateCanLevelUp();
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}

