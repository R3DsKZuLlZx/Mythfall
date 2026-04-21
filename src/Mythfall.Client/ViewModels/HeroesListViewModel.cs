using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

public partial class HeroesListViewModel : ObservableObject
{
    private readonly HeroService _heroService;
    private readonly List<Hero> _allHeroes;

    public ObservableCollection<Hero> Heroes { get; } = [];

    public PlayerResources Resources => _heroService.Resources;

    [ObservableProperty]
    private Faction? selectedFaction;

    public bool IsAllSelected => SelectedFaction is null;
    public bool IsOrderSelected => SelectedFaction == Faction.Order;
    public bool IsChaosSelected => SelectedFaction == Faction.Chaos;
    public bool IsArcaneSelected => SelectedFaction == Faction.Arcane;
    public bool IsFurySelected => SelectedFaction == Faction.Fury;

    public HeroesListViewModel(HeroService heroService)
    {
        _heroService = heroService;
        _allHeroes = _heroService.GetHeroes();
        ApplyFilter();
    }

    partial void OnSelectedFactionChanged(Faction? value)
    {
        ApplyFilter();
        OnPropertyChanged(nameof(IsAllSelected));
        OnPropertyChanged(nameof(IsOrderSelected));
        OnPropertyChanged(nameof(IsChaosSelected));
        OnPropertyChanged(nameof(IsArcaneSelected));
        OnPropertyChanged(nameof(IsFurySelected));
    }

    private void ApplyFilter()
    {
        Heroes.Clear();
        var filtered = SelectedFaction is null
            ? _allHeroes
            : _allHeroes.Where(h => h.Faction == SelectedFaction).ToList();

        foreach (var hero in filtered)
            Heroes.Add(hero);
    }

    [RelayCommand]
    private void FilterByFaction(string? factionStr)
    {
        if (factionStr is not null && Enum.TryParse<Faction>(factionStr, out var faction))
            SelectedFaction = SelectedFaction == faction ? null : faction;
        else
            SelectedFaction = null;
    }

    [RelayCommand]
    private async Task SelectHero(Hero hero)
    {
        var parameters = new Dictionary<string, object> { { "Hero", hero } };
        await Shell.Current.GoToAsync("HeroDetailPage", parameters);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task NavigateTo(string route)
    {
        if (route == "HomePage")
            await Shell.Current.GoToAsync("//HomePage");
    }
}
