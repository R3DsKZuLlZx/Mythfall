using CommunityToolkit.Mvvm.ComponentModel;

namespace Mythfall.Client.Models;

public partial class CampaignStage : ObservableObject
{
    public int StageNumber { get; set; }
    public string Name { get; set; } = string.Empty;

    [ObservableProperty]
    private bool isCompleted;

    [ObservableProperty]
    private bool isUnlocked;

    /// <summary>
    /// The static enemy team for this stage.
    /// </summary>
    public List<Hero> EnemyTeam { get; set; } = [];

    // Rewards
    public int GoldReward { get; set; }
    public int ExpReward { get; set; }
    public int SpecialExpReward { get; set; }

    // Visual positioning on the map (0.0 - 1.0 normalized)
    public double MapX { get; set; }
    public double MapY { get; set; }

    public string StageIcon => IsCompleted ? "icon_stage_done" : IsUnlocked ? "icon_stage_open" : "icon_stage_locked";
}

