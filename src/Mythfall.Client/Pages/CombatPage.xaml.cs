using Mythfall.Client.ViewModels;

namespace Mythfall.Client.Pages;

public partial class CombatPage : ContentPage
{
    public CombatPage(CombatViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CombatViewModel vm)
            await vm.StartFightCommand.ExecuteAsync(null);
    }
}
