using Mythfall.Client.Models;

namespace Mythfall.Client.Services;

public class CombatService
{
    /// <summary>
    /// Pre-calculate the entire fight as a list of combat log entries.
    /// Each entry represents one hero attacking one enemy.
    /// </summary>
    public List<CombatLogEntry> SimulateCombat(
        List<CombatHero> playerTeam,
        List<CombatHero> enemyTeam)
    {
        var log = new List<CombatLogEntry>();
        var allFighters = new List<CombatHero>();
        allFighters.AddRange(playerTeam);
        allFighters.AddRange(enemyTeam);

        // Sort by attack descending for turn order (fastest hitters go first)
        allFighters = allFighters.OrderByDescending(h => h.Attack).ToList();

        var maxRounds = 100; // safety cap
        for (var round = 0; round < maxRounds; round++)
        {
            foreach (var attacker in allFighters)
            {
                if (attacker.IsDefeated) continue;

                // Pick target: first alive hero on the opposing side
                var targets = attacker.IsPlayerSide ? enemyTeam : playerTeam;
                var target = targets.FirstOrDefault(t => !t.IsDefeated);
                if (target is null) break;

                // Damage = attacker's attack - target's defence (min 1)
                var damage = Math.Max(1, attacker.Attack - target.Defence);
                target.TakeDamage(damage);

                log.Add(new CombatLogEntry
                {
                    AttackerName = attacker.Name,
                    DefenderName = target.Name,
                    Damage = damage,
                    DefenderDefeated = target.IsDefeated,
                    IsPlayerAttacking = attacker.IsPlayerSide
                });

                // Check if fight is over
                if (playerTeam.All(h => h.IsDefeated) || enemyTeam.All(h => h.IsDefeated))
                    return log;
            }
        }

        return log;
    }
}

