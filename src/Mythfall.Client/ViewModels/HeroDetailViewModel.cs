using System.Collections.ObjectModel;
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

    [ObservableProperty]
    private bool canRankUp;

    [ObservableProperty]
    private string rankUpMessage = string.Empty;

    [ObservableProperty]
    private int duplicateCount;

    [ObservableProperty]
    private bool isPickerVisible;

    [ObservableProperty]
    private bool isConfirmVisible;

    [ObservableProperty]
    private Hero? selectedSacrifice;

    public ObservableCollection<Hero> Duplicates { get; } = [];

    public string NextRankName => Hero is not null && Hero.Rank < HeroRank.Legendary
        ? (Hero.Rank + 1).ToString()
        : string.Empty;

    public PlayerResources Resources => _heroService.Resources;

    public HeroDetailViewModel(HeroService heroService)
    {
        _heroService = heroService;
    }

    partial void OnHeroChanged(Hero? value)
    {
        UpdateCanLevelUp();
        UpdateCanRankUp();
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

    private void UpdateCanRankUp()
    {
        if (Hero is null)
        {
            CanRankUp = false;
            RankUpMessage = string.Empty;
            DuplicateCount = 0;
            return;
        }

        if (Hero.Rank == HeroRank.Legendary)
        {
            CanRankUp = false;
            RankUpMessage = "Already at max rank!";
            DuplicateCount = 0;
            return;
        }

        var duplicates = _heroService.GetDuplicatesForRankUp(Hero);
        DuplicateCount = duplicates.Count;
        CanRankUp = duplicates.Count > 0;
        RankUpMessage = CanRankUp
            ? $"{duplicates.Count} duplicate(s) available"
            : "No duplicates available for rank up";
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
    private void ShowRankUpPicker()
    {
        if (Hero is null || !CanRankUp) return;

        Duplicates.Clear();
        foreach (var dup in _heroService.GetDuplicatesForRankUp(Hero))
            Duplicates.Add(dup);

        IsPickerVisible = true;
    }

    [RelayCommand]
    private void SelectSacrifice(Hero sacrifice)
    {
        SelectedSacrifice = sacrifice;
        IsPickerVisible = false;
        OnPropertyChanged(nameof(NextRankName));
        IsConfirmVisible = true;
    }

    [RelayCommand]
    private void ConfirmRankUp()
    {
        if (Hero is null || SelectedSacrifice is null) return;

        _heroService.RemoveHero(SelectedSacrifice);

        Hero.Rank = Hero.Rank + 1;
        Hero.RefreshStats();
        OnPropertyChanged(nameof(Hero));

        SelectedSacrifice = null;
        IsConfirmVisible = false;

        UpdateCanLevelUp();
        UpdateCanRankUp();
    }

    [RelayCommand]
    private void CancelRankUp()
    {
        IsPickerVisible = false;
        IsConfirmVisible = false;
        SelectedSacrifice = null;
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
