using Microsoft.Extensions.Logging;
using Mythfall.Client.Pages;
using Mythfall.Client.Services;
using Mythfall.Client.ViewModels;

namespace Mythfall.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddSingleton<HeroService>();
        builder.Services.AddSingleton<CampaignService>();
        builder.Services.AddTransient<CombatService>();

        // ViewModels
        builder.Services.AddTransient<HeroesListViewModel>();
        builder.Services.AddTransient<HeroDetailViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<ShopViewModel>();
        builder.Services.AddTransient<CampaignMapViewModel>();
        builder.Services.AddTransient<TeamSelectViewModel>();
        builder.Services.AddTransient<CombatViewModel>();

        // Pages
        builder.Services.AddTransient<HeroesListPage>();
        builder.Services.AddTransient<HeroDetailPage>();
        builder.Services.AddTransient<ShopPage>();
        builder.Services.AddTransient<CampaignMapPage>();
        builder.Services.AddTransient<TeamSelectPage>();
        builder.Services.AddTransient<CombatPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
