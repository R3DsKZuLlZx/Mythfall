namespace Mythfall.Client.Models;

public class CombatLogEntry
{
    public string AttackerName { get; set; } = string.Empty;
    public string DefenderName { get; set; } = string.Empty;
    public int Damage { get; set; }
    public bool DefenderDefeated { get; set; }
    public bool IsPlayerAttacking { get; set; }

    public string Text => DefenderDefeated
        ? $"{AttackerName} hits {DefenderName} for {Damage} — DEFEATED!"
        : $"{AttackerName} hits {DefenderName} for {Damage}";

    public string TextColor => IsPlayerAttacking ? "#88cc88" : "#ee8888";
}

