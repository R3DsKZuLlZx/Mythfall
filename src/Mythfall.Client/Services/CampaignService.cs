using Mythfall.Client.Models;

namespace Mythfall.Client.Services;

public class CampaignService
{
    private readonly List<CampaignStage> _stages;

    public CampaignService()
    {
        _stages = BuildStages();
    }

    public List<CampaignStage> GetStages() => _stages;

    public CampaignStage? GetStage(int stageNumber) =>
        _stages.FirstOrDefault(s => s.StageNumber == stageNumber);

    public void CompleteStage(int stageNumber)
    {
        var stage = GetStage(stageNumber);
        if (stage is null) return;

        stage.IsCompleted = true;

        // Unlock next stage
        var next = GetStage(stageNumber + 1);
        if (next is not null)
            next.IsUnlocked = true;
    }

    private static List<CampaignStage> BuildStages()
    {
        var stages = new List<CampaignStage>();

        for (var i = 1; i <= 10; i++)
        {
            var stage = new CampaignStage
            {
                StageNumber = i,
                Name = i switch
                {
                    1 => "Forest Edge",
                    2 => "Mossy Path",
                    3 => "Dark Hollow",
                    4 => "River Crossing",
                    5 => "Bandit Camp",
                    6 => "Cursed Grove",
                    7 => "Stone Bridge",
                    8 => "Shadow Pass",
                    9 => "Castle Gate",
                    10 => "Throne Room",
                    _ => $"Stage {i}"
                },
                IsUnlocked = i == 1,
                IsCompleted = false,
                GoldReward = 50 + i * 20,
                ExpReward = 20 + i * 10,
                SpecialExpReward = i % 5 == 0 ? 1 : 0,
                EnemyTeam = BuildEnemyTeam(i),
                // Zigzag path from bottom-left to top-right
                MapX = 0.15 + (i % 2 == 0 ? 0.55 : 0.0),
                MapY = 1.0 - (i / 11.0)
            };
            stages.Add(stage);
        }

        return stages;
    }

    private static List<Hero> BuildEnemyTeam(int stageNumber)
    {
        // Difficulty scales: more heroes, higher ranks and levels
        var team = new List<Hero>();
        var count = Math.Min(5, 1 + stageNumber / 2);
        var rank = stageNumber switch
        {
            <= 3 => HeroRank.Rare,
            <= 6 => HeroRank.Elite,
            <= 8 => HeroRank.Epic,
            _ => HeroRank.Legendary
        };
        var baseLevel = Math.Min(20, 1 + stageNumber * 2);

        var templates = new (string Id, string Name, Faction Faction, int Hp, int Atk, int Def)[]
        {
            ("enemy_goblin", "Goblin Scout", Faction.Chaos, 80, 18, 6),
            ("enemy_skeleton", "Skeleton Warrior", Faction.Chaos, 100, 22, 12),
            ("enemy_wolf", "Dire Wolf", Faction.Fury, 90, 26, 8),
            ("enemy_shaman", "Dark Shaman", Faction.Arcane, 75, 30, 5),
            ("enemy_knight", "Fallen Knight", Faction.Order, 130, 20, 20),
        };

        for (var i = 0; i < count; i++)
        {
            var t = templates[i % templates.Length];
            team.Add(new Hero
            {
                Id = t.Id,
                Name = t.Name,
                IconImage = "dotnet_bot.png",
                FullImage = "dotnet_bot.png",
                Faction = t.Faction,
                Rank = rank,
                Level = baseLevel,
                BaseHp = t.Hp,
                BaseAttack = t.Atk,
                BaseDefence = t.Def
            });
        }

        return team;
    }
}

