using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using Pinger.ViewModels;
using Avalonia.Controls.Notifications;
using Pinger.Models;

namespace Pinger.Views;

public partial class MainWindow : Window
{
    private readonly WindowNotificationManager _notificationManager;
    public MainWindow()
    {
        InitializeComponent();

        _notificationManager = new WindowNotificationManager(this)
        {
            Position = NotificationPosition.TopRight,
            MaxItems = 3
        };
    }

     private void ShowError(string message)
    {
        _notificationManager.Show(new Notification(
            "Error",
            message,
            NotificationType.Error,
            TimeSpan.FromSeconds(3)));
    }

    private void AddDevice_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            string Name=DeviceNameTextBox.Text;
            string Ip=DeviceIpTextBox.Text;

            bool onFieldEmpty=false;
            bool isIpInvalid=false;
            bool nameOrIpTaken=false;

            if(Name==string.Empty || Ip==string.Empty)
            {
                ShowError("Fill both \"Device Name\" and \"IP address\" !");
                return;
            }
            if(Helpers.DeviceValidator.IsValidIPv4(Ip)==false)
            {
                ShowError("Ip in wrong format!");
                return;
            }
            if(Helpers.DeviceValidator.IsDeviceIpAlreadySaved(vm.Devices,Ip)==true)
            {
                ShowError("Ip already added!");
                return;
            }
            if(Helpers.DeviceValidator.IsDeviceNameAlreadySaved(vm.Devices,Name)==true)
            {
                ShowError("Name already added!");
                return;
            }





            vm.Devices.Add(new Models.Device
            {
                Name=DeviceNameTextBox.Text ?? "",
                Ip=DeviceIpTextBox.Text ?? "",
                FailCounter=0
            });

            DeviceNameTextBox.Text = "";
            DeviceIpTextBox.Text = "";
        }
    }


}