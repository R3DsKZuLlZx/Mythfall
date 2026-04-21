using CommunityToolkit.Mvvm.ComponentModel;

namespace Mythfall.Client.Models;

public partial class SelectableHero : ObservableObject
{
    public Hero Hero { get; set; } = null!;

    [ObservableProperty]
    private bool isSelected;

    public string BorderColor => IsSelected ? "#e8b830" : Hero.RankColor;

    public void RefreshBorderColor() => OnPropertyChanged(nameof(BorderColor));
}
