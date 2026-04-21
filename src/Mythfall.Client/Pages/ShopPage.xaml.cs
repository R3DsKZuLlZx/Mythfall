using Mythfall.Client.ViewModels;

namespace Mythfall.Client.Pages;

public partial class ShopPage : ContentPage
{
    public ShopPage(ShopViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

