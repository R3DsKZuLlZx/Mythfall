using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mythfall.Client.Models;
using Mythfall.Client.Services;

namespace Mythfall.Client.ViewModels;

[QueryProperty(nameof(StageNumber), "StageNumber")]
[QueryProperty(nameof(SelectedHeroes), "SelectedHeroes")]
public partial class CombatViewModel : ObservableObject
{
    private readonly CampaignService _campaignService;
    private readonly CombatService _combatService;
    private readonly HeroService _heroService;

    private List<CombatLogEntry> _fullLog = [];
    private int _logIndex;
    private CancellationTokenSource? _animationCts;

    [ObservableProperty]
    private int stageNumber;

    [ObservableProperty]
    private string stageName = string.Empty;

    // Set via navigation parameter
    public List<Hero> SelectedHeroes { get; set; } = [];

    public ObservableCollection<CombatHero> PlayerTeam { get; } = [];
    public ObservableCollection<CombatHero> EnemyTeam { get; } = [];
    public ObservableCollection<CombatLogEntry> CombatLog { get; } = [];

    [ObservableProperty]
    private bool isFightRunning;

    [ObservableProperty]
    private bool isFightOver;

    [ObservableProperty]
    private bool playerWon;

    [ObservableProperty]
    private string resultTitle = string.Empty;

    [ObservableProperty]
    private string resultMessage = string.Empty;

    [ObservableProperty]
    private int goldReward;

    [ObservableProperty]
    private int expReward;

    [ObservableProperty]
    private int specialExpReward;

    [ObservableProperty]
    private bool hasNextStage;

    public CombatViewModel(CampaignService campaignService, CombatService combatService, HeroService heroService)
    {
        _campaignService = campaignService;
        _combatService = combatService;
        _heroService = heroService;
    }

    partial void OnStageNumberChanged(int value)
    {
        var stage = _campaignService.GetStage(value);
        if (stage is not null)
            StageName = $"Stage {stage.StageNumber}: {stage.Name}";
    }

    [RelayCommand]
    private async Task StartFight()
    {
        if (IsFightRunning) return;

        var stage = _campaignService.GetStage(StageNumber);
        if (stage is null) return;

        // Build combat teams
        PlayerTeam.Clear();
        EnemyTeam.Clear();
        CombatLog.Clear();

        foreach (var hero in SelectedHeroes)
            PlayerTeam.Add(CombatHero.FromHero(hero, true));

        foreach (var enemy in stage.EnemyTeam)
            EnemyTeam.Add(CombatHero.FromHero(enemy, false));

        // Pre-simulate the entire fight
        _fullLog = _combatService.SimulateCombat(
            PlayerTeam.ToList(),
            EnemyTeam.ToList());
        _logIndex = 0;

        // Reset combat heroes to full HP for animated playback
        foreach (var h in PlayerTeam) { h.CurrentHp = h.MaxHp; h.IsDefeated = false; h.LastDamageText = string.Empty; }
        foreach (var h in EnemyTeam) { h.CurrentHp = h.MaxHp; h.IsDefeated = false; h.LastDamageText = string.Empty; }

        IsFightRunning = true;
        IsFightOver = false;

        // Animate the log entries one by one
        _animationCts = new CancellationTokenSource();
        await AnimateCombat(_animationCts.Token);
    }

    private async Task AnimateCombat(CancellationToken ct)
    {
        // Replay each log entry with a delay for visual effect
        var playerList = PlayerTeam.ToList();
        var enemyList = EnemyTeam.ToList();

        // Reset HP for replay
        foreach (var h in playerList) { h.CurrentHp = h.MaxHp; h.IsDefeated = false; }
        foreach (var h in enemyList) { h.CurrentHp = h.MaxHp; h.IsDefeated = false; }

        foreach (var entry in _fullLog)
        {
            if (ct.IsCancellationRequested) break;

            // Find the defender in the observable collections and apply damage
            var allHeroes = PlayerTeam.Concat(EnemyTeam);
            var defender = allHeroes.FirstOrDefault(h => h.Name == entry.DefenderName && !h.IsDefeated);
            defender?.TakeDamage(entry.Damage);

            CombatLog.Add(entry);

            try { await Task.Delay(600, ct); }
            catch (TaskCanceledException) { break; }
        }

        // Determine winner
        var playerAlive = PlayerTeam.Any(h => !h.IsDefeated);
        PlayerWon = playerAlive;
        IsFightRunning = false;
        IsFightOver = true;

        if (PlayerWon)
        {
            var stage = _campaignService.GetStage(StageNumber);
            if (stage is not null && !stage.IsCompleted)
            {
                _campaignService.CompleteStage(StageNumber);
                GoldReward = stage.GoldReward;
                ExpReward = stage.ExpReward;
                SpecialExpReward = stage.SpecialExpReward;

                _heroService.Resources.Gold += stage.GoldReward;
                _heroService.Resources.Experience += stage.ExpReward;
                _heroService.Resources.SpecialExperience += stage.SpecialExpReward;
            }
            else
            {
                // Already completed — give reduced rewards
                GoldReward = (stage?.GoldReward ?? 0) / 2;
                ExpReward = (stage?.ExpReward ?? 0) / 2;
                SpecialExpReward = 0;
                _heroService.Resources.Gold += GoldReward;
                _heroService.Resources.Experience += ExpReward;
            }

            var next = _campaignService.GetStage(StageNumber + 1);
            HasNextStage = next is not null && !next.IsCompleted;
            ResultTitle = "⚔️ Victory!";
            ResultMessage = "Your heroes have triumphed!";
        }
        else
        {
            GoldReward = 0;
            ExpReward = 0;
            SpecialExpReward = 0;
            HasNextStage = false;
            ResultTitle = "💀 Defeat";
            ResultMessage = "Your heroes have fallen...";
        }
    }

    [RelayCommand]
    private async Task NextStage()
    {
        // Pop back to campaign map, then push a fresh team select for the next stage
        var nextStage = StageNumber + 1;
        await Shell.Current.GoToAsync("../..");
        var parameters = new Dictionary<string, object>
        {
            { "StageNumber", nextStage }
        };
        await Shell.Current.GoToAsync("TeamSelectPage", parameters);
    }

    [RelayCommand]
    private async Task Retry()
    {
        // Pop back to campaign map, then push a fresh team select for the same stage
        var stage = StageNumber;
        await Shell.Current.GoToAsync("../..");
        var parameters = new Dictionary<string, object>
        {
            { "StageNumber", stage }
        };
        await Shell.Current.GoToAsync("TeamSelectPage", parameters);
    }

    [RelayCommand]
    private async Task ReturnToMap()
    {
        _animationCts?.Cancel();
        await Shell.Current.GoToAsync("..");
    }
}

