namespace Mythfall.Client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("HeroesListPage", typeof(Pages.HeroesListPage));
        Routing.RegisterRoute("HeroDetailPage", typeof(Pages.HeroDetailPage));
        Routing.RegisterRoute("ShopPage", typeof(Pages.ShopPage));
        Routing.RegisterRoute("CampaignMapPage", typeof(Pages.CampaignMapPage));
        Routing.RegisterRoute("TeamSelectPage", typeof(Pages.TeamSelectPage));
        Routing.RegisterRoute("CombatPage", typeof(Pages.CombatPage));
    }
}
