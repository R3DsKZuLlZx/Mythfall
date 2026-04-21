using Mythfall.Client.ViewModels;
using Mythfall.Client.Services;

namespace Mythfall.Client.Pages;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnCampaignClicked(object? sender, EventArgs e)
    {
        // TODO: Navigate to Campaign page
        await DisplayAlertAsync("Campaign", "Campaign coming soon!", "OK");
    }

    private async void OnHeroesClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("HeroesListPage");
    }

    private async void OnShopClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ShopPage");
    }
}
