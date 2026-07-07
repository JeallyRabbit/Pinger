using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Pinger.Models;
using Pinger.Services;
using System.Diagnostics;

namespace Pinger.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    private readonly PingService _pingService = new();
    private readonly CancellationTokenSource _pingCancellationTokenSource = new();
    private readonly EmailNotificationService _emailNotificationService = new();

    public ObservableCollection<Device> Devices { get; } = new();


    public static int maxFails = 5;

    public MainWindowViewModel()
    {
        //testing
        Devices.Add(new Models.Device
            {
                Name="Test",
                Ip="192.168.4.235",
                FailCounter=0
            });
        
        _ = StartPingingAsync(_pingCancellationTokenSource.Token);
    }
    private async Task StartPingingAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (Device device in Devices.ToList())
            {
                Debug.WriteLine("Pinging device: " + device.Ip);

                bool pingResult =  _pingService.Ping(device.Ip);

                int currentFailCounter = await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (pingResult == false)
                    {
                        device.FailCounter++;
                    }
                    else
                    {
                        device.FailCounter = 0;
                    }

                    return device.FailCounter;
                });

                if (currentFailCounter % maxFails == 0 && currentFailCounter > 0)
                {
                    Debug.WriteLine($"Device {device.Name} failed {currentFailCounter} times.");

                    // Send email / notification here later
                    string subject=$"Device {device.Name} went offline";
                    string text=subject+" at: "+System.DateTime.Now.ToString();

                    await _emailNotificationService.SendAsync("aleks.karczewski@vcorrect.eu",subject,text);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }

    public void StopPinging()
    {
        _pingCancellationTokenSource.Cancel();
    }




}
