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

        // ViewModels
        builder.Services.AddTransient<HeroesListViewModel>();
        builder.Services.AddTransient<HeroDetailViewModel>();

        // Pages
        builder.Services.AddTransient<HeroesListPage>();
        builder.Services.AddTransient<HeroDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
