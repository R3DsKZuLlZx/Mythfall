namespace Mythfall.Client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("HeroesListPage", typeof(Pages.HeroesListPage));
        Routing.RegisterRoute("HeroDetailPage", typeof(Pages.HeroDetailPage));
    }
}
