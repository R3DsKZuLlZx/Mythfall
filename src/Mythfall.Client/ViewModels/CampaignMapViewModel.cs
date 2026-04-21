using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

public partial class CampaignMapViewModel : ObservableObject
{
    private readonly CampaignService _campaignService;
    private readonly HeroService _heroService;

    public ObservableCollection<CampaignStage> Stages { get; } = [];
    public PlayerResources Resources => _heroService.Resources;

    public CampaignMapViewModel(CampaignService campaignService, HeroService heroService)
    {
        _campaignService = campaignService;
        _heroService = heroService;
        LoadStages();
    }

    public void LoadStages()
    {
        Stages.Clear();
        foreach (var stage in _campaignService.GetStages())
            Stages.Add(stage);
    }

    [RelayCommand]
    private async Task SelectStage(CampaignStage stage)
    {
        if (!stage.IsUnlocked) return;

        var parameters = new Dictionary<string, object>
        {
            { "StageNumber", stage.StageNumber }
        };
        await Shell.Current.GoToAsync("TeamSelectPage", parameters);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}

