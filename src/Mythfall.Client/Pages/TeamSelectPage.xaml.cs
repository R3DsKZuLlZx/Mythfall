using Mythfall.Client.ViewModels;

namespace Mythfall.Client.Pages;

public partial class TeamSelectPage : ContentPage
{
    public TeamSelectPage(TeamSelectViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

