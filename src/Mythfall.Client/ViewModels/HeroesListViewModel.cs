using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

public partial class HeroesListViewModel : ObservableObject
{
    private readonly HeroService _heroService;

    public ObservableCollection<Hero> Heroes { get; }

    public HeroesListViewModel(HeroService heroService)
    {
        _heroService = heroService;
        Heroes = new ObservableCollection<Hero>(_heroService.GetHeroes());
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
}

