using Mythfall.Client.ViewModels;

namespace Mythfall.Client.Pages;

public partial class HeroDetailPage : ContentPage
{
    public HeroDetailPage(HeroDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

