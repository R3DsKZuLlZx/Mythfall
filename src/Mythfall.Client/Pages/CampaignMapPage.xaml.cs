using Mythfall.Client.ViewModels;

namespace Mythfall.Client.Pages;

public partial class CampaignMapPage : ContentPage
{
    public CampaignMapPage(CampaignMapViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CampaignMapViewModel vm)
            vm.LoadStages();
    }
}

