using CommunityToolkit.Mvvm.ComponentModel;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly HeroService _heroService;

    public PlayerResources Resources => _heroService.Resources;

    public HomeViewModel(HeroService heroService)
    {
        _heroService = heroService;
    }
}

