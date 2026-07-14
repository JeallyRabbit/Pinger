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
using Pinger.Helpers;

namespace Pinger.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    private readonly PingService _pingService = new();
    private readonly SettingsService _settingsService = new();
    private readonly CancellationTokenSource _pingCancellationTokenSource = new();

    private readonly EmailNotificationService _emailNotificationService = new();
    public ObservableCollection<Device> Devices { get; } = new();

    public static int maxFails = 5;


    [ObservableProperty]
    private string emailSender = "";
    [ObservableProperty]
    private string emailReceiver = "";
    [ObservableProperty]
    private string smtpHost = "";
    [ObservableProperty]
    private int smtpPort = 25;
    public MainWindowViewModel()
    {


        _ = LoadSavedValuesAsync();

        _ = StartPingingAsync(_pingCancellationTokenSource.Token);
    }
    private async Task StartPingingAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (Device device in Devices.ToList())
            {
                //Debug.WriteLine("Pinging device: " + device.Ip);

                bool pingResult = _pingService.Ping(device.Ip);

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

                if ((currentFailCounter % (maxFails * 10) == 0 || currentFailCounter == maxFails) && currentFailCounter > 0)
                {
                    //Debug.WriteLine($"Device {device.Name} failed {currentFailCounter} times.");

                    // Send email / notification here later
                    string subject = $"Device {device.Name} went offline";
                    string text = subject + " at: " + System.DateTime.Now.ToString();

                    await _emailNotificationService.SendAsync(EmailSender, EmailReceiver, subject, text, SmtpHost, SmtpPort);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }

    private async Task LoadSavedValuesAsync()
    {
        AppSettings settings = await _settingsService.LoadAsync();

        maxFails = settings.MaxFails;
        EmailSender = settings.EmailSender;
        EmailReceiver = settings.EmailReceiver;
        SmtpHost = settings.SMTPHost;
        SmtpPort = settings.SMTPPort;

        
        Devices.Clear();

        foreach (Device device in settings.Devices)
        {
            device.FailCounter = 0;
            Devices.Add(device);
        }
    }

    public async Task SaveValuesAsync()
    {
        AppSettings settings = new()
        {
            Devices = Devices.ToList(),
            MaxFails = maxFails,
            SMTPHost = SmtpHost,
            SMTPPort = SmtpPort,
            EmailReceiver = EmailReceiver,
            EmailSender = EmailSender
        };

        await _settingsService.SaveAsync(settings);
    }
    public void StopPinging()
    {
        _pingCancellationTokenSource.Cancel();
    }




}
