using CommunityToolkit.Mvvm.ComponentModel;

namespace Pinger.Models;

public partial class Device : ObservableObject
{
    [ObservableProperty]
    private string name = "Switch 0";

    [ObservableProperty]
    private string ip = "0.0.0.0";

    [ObservableProperty]
    private int failCounter;

    
}