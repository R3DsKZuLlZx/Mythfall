using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

public partial class ShopViewModel : ObservableObject
{
    private const int SpinCost = 100;
    private readonly HeroService _heroService;
    private readonly List<SpinHero> _spinPool;
    private readonly double _totalWeight;
    private readonly Random _random = new();

    public PlayerResources Resources => _heroService.Resources;

    [ObservableProperty]
    private SpinHero? lastWon;

    [ObservableProperty]
    private bool hasResult;

    [ObservableProperty]
    private bool isSpinning;

    [ObservableProperty]
    private string message = string.Empty;

    public bool CanSpin => !IsSpinning && Resources.Gold >= SpinCost;
    public bool ShowNotEnoughGold => !IsSpinning && Resources.Gold < SpinCost;

    public ShopViewModel(HeroService heroService)
    {
        _heroService = heroService;
        _spinPool = _heroService.GetSpinPool();
        _totalWeight = _spinPool.Sum(h => h.Weight);

        Resources.PropertyChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(CanSpin));
            OnPropertyChanged(nameof(ShowNotEnoughGold));
        };
    }

    [RelayCommand]
    private async Task Spin()
    {
        if (Resources.Gold < SpinCost)
        {
            Message = "Not enough gold!";
            return;
        }

        IsSpinning = true;
        HasResult = false;
        LastWon = null;
        Message = string.Empty;
        OnPropertyChanged(nameof(CanSpin));
        OnPropertyChanged(nameof(ShowNotEnoughGold));

        // Small delay for anticipation
        await Task.Delay(600);

        // Deduct gold
        Resources.Gold -= SpinCost;

        // Weighted random selection
        var roll = _random.NextDouble() * _totalWeight;
        double cumulative = 0;
        SpinHero? selected = null;

        foreach (var hero in _spinPool)
        {
            cumulative += hero.Weight;
            if (roll <= cumulative)
            {
                selected = hero;
                break;
            }
        }

        selected ??= _spinPool[^1]; // fallback

        // Add won hero to the player's roster
        _heroService.AddHero(selected.ToHero());

        LastWon = selected;
        HasResult = true;
        IsSpinning = false;
        OnPropertyChanged(nameof(CanSpin));
        OnPropertyChanged(nameof(ShowNotEnoughGold));

        Message = selected.Rank switch
        {
            HeroRank.Legendary => "LEGENDARY! Incredible luck!",
            HeroRank.Epic => "Epic pull! Nice!",
            HeroRank.Elite => "Elite hero unlocked!",
            _ => "New hero acquired!"
        };
    }

    [RelayCommand]
    private void DismissResult()
    {
        HasResult = false;
        LastWon = null;
        Message = string.Empty;
    }

    [RelayCommand]
    private async Task NavigateTo(string route)
    {
        if (route == "HomePage")
            await Shell.Current.GoToAsync("//HomePage");
        else if (route == "HeroesListPage")
            await Shell.Current.GoToAsync("HeroesListPage");
    }
}

