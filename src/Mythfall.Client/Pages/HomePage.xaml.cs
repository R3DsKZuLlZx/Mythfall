namespace Mythfall.Client.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnCampaignClicked(object? sender, EventArgs e)
    {
        // TODO: Navigate to Campaign page
        await DisplayAlertAsync("Campaign", "Campaign coming soon!", "OK");
    }

    private async void OnHeroesClicked(object? sender, EventArgs e)
    {
        // TODO: Navigate to Heroes page
        await DisplayAlertAsync("Heroes", "Heroes roster coming soon!", "OK");
    }

    private async void OnShopClicked(object? sender, EventArgs e)
    {
        // TODO: Navigate to Shop page
        await DisplayAlertAsync("Shop", "Shop coming soon!", "OK");
    }
}

