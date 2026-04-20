namespace Mythfall.Client.Pages;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Fade in splash content
        await SplashContent.FadeToAsync(1, 800, Easing.CubicOut);

        // Slight delay then show loading
        await Task.Delay(300);
        await LoadingSection.FadeToAsync(1, 500, Easing.CubicOut);

        // Simulate loading / give time to admire splash
        await Task.Delay(2000);

        // Fade out everything
        await Task.WhenAll(
            SplashContent.FadeToAsync(0, 400, Easing.CubicIn),
            LoadingSection.FadeToAsync(0, 400, Easing.CubicIn)
        );

        // Navigate to home
        if (Shell.Current is not null)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}

