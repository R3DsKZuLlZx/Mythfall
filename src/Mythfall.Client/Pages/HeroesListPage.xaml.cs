using Mythfall.Client.ViewModels;

namespace Mythfall.Client.Pages;

public partial class HeroesListPage : ContentPage
{
    public HeroesListPage(HeroesListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HeroesListViewModel vm)
            vm.Refresh();
    }
}

