using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

[QueryProperty(nameof(StageNumber), "StageNumber")]
public partial class TeamSelectViewModel : ObservableObject
{
    private readonly HeroService _heroService;
    private readonly CampaignService _campaignService;

    [ObservableProperty]
    private int stageNumber;

    [ObservableProperty]
    private string stageName = string.Empty;

    [ObservableProperty]
    private int selectedCount;

    public ObservableCollection<SelectableHero> AvailableHeroes { get; } = [];
    public ObservableCollection<CombatHero> EnemyPreview { get; } = [];

    public bool CanStart => SelectedCount > 0;

    public TeamSelectViewModel(HeroService heroService, CampaignService campaignService)
    {
        _heroService = heroService;
        _campaignService = campaignService;
    }

    partial void OnStageNumberChanged(int value)
    {
        var stage = _campaignService.GetStage(value);
        if (stage is null) return;

        StageName = $"Stage {stage.StageNumber}: {stage.Name}";

        AvailableHeroes.Clear();
        foreach (var hero in _heroService.GetHeroes())
            AvailableHeroes.Add(new SelectableHero { Hero = hero });

        EnemyPreview.Clear();
        foreach (var enemy in stage.EnemyTeam)
            EnemyPreview.Add(CombatHero.FromHero(enemy, false));

        SelectedCount = 0;
        OnPropertyChanged(nameof(CanStart));
    }

    [RelayCommand]
    private void ToggleHero(SelectableHero selectable)
    {
        if (selectable.IsSelected)
        {
            selectable.IsSelected = false;
        }
        else
        {
            if (SelectedCount >= 5) return;
            selectable.IsSelected = true;
        }

        SelectedCount = AvailableHeroes.Count(h => h.IsSelected);
        OnPropertyChanged(nameof(CanStart));

        selectable.RefreshBorderColor();
    }

    [RelayCommand]
    private async Task StartBattle()
    {
        if (SelectedCount == 0) return;

        var selectedHeroes = AvailableHeroes
            .Where(h => h.IsSelected)
            .Select(h => h.Hero)
            .ToList();

        var parameters = new Dictionary<string, object>
        {
            { "StageNumber", StageNumber },
            { "SelectedHeroes", selectedHeroes }
        };
        await Shell.Current.GoToAsync("CombatPage", parameters);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}

