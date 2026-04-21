using CommunityToolkit.Mvvm.ComponentModel;

namespace Mythfall.Client.Models;

public partial class PlayerResources : ObservableObject
{
    [ObservableProperty]
    private int gold = 1245;

    [ObservableProperty]
    private int experience = 530;

    [ObservableProperty]
    private int specialExperience = 12;
}

