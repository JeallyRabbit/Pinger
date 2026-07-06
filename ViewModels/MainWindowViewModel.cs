using System.Collections.ObjectModel;
using Pinger.Models;

namespace Pinger.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    public ObservableCollection<Device> Devices { get; }=new();


    public MainWindowViewModel()
    {
        Devices.Add(new  Device());

        //Add getting them from file
    }

    
    

}
