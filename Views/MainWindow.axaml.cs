
using Avalonia.Controls;
using Avalonia.Interactivity;
using Pinger.ViewModels;
using Pinger.Models;
using Pinger.Helpers;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Pinger.Views;

public partial class MainWindow : Window
{

    private readonly NotificationHelper _notifications;

    public MainWindow()
    {
        InitializeComponent();
        _notifications = new NotificationHelper(this);


    }


    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
        {
            return;
        }
        await vm.SaveValuesAsync();
        _notifications.ShowSuccess("Saved!");
    }
    private void AddDevice_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            TextBox? deviceNameTextBox = this.FindControl<TextBox>("DeviceNameTextBox");
            TextBox? deviceIpTextBox = this.FindControl<TextBox>("DeviceIpTextBox");

            string Name = deviceNameTextBox.Text;
            string Ip = deviceIpTextBox.Text;



            if (Name == string.Empty || Ip == string.Empty)
            {
                _notifications.ShowError("Fill both \"Device Name\" and \"IP address\" !");
                return;
            }
            if (Helpers.DeviceValidator.IsValidIPv4(Ip) == false)
            {
                _notifications.ShowError("Ip in wrong format!");
                return;
            }
            if (Helpers.DeviceValidator.IsDeviceIpAlreadySaved(vm.Devices, Ip) == true)
            {
                _notifications.ShowError("Ip already added!");
                return;
            }
            if (Helpers.DeviceValidator.IsDeviceNameAlreadySaved(vm.Devices, Name) == true)
            {
                _notifications.ShowError("Name already added!");
                return;
            }





            vm.Devices.Add(new Models.Device
            {
                Name = deviceNameTextBox.Text ?? "",
                Ip = deviceIpTextBox.Text ?? "",
                FailCounter = 0
            });

            deviceNameTextBox.Text = "";
            deviceIpTextBox.Text = "";
        }
    }



    private async void DeviceCard_Remove(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
        {
            return;
        }


        if (sender is not Border border)
        {
            return;
        }

        if (border.DataContext is not Device device)
        {
            return;
        }

        ConfirmationWindow confWindow=new ConfirmationWindow(device.Name,device.Ip);
        bool result=await confWindow.ShowDialog<bool>(this);
        if(result)
        {
            vm.Devices.Remove(device);
        }
        
    }

    private async void DeviceCard_Update(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
        {
            return;
        }

        if (sender is not Border border)
        {
            return;
        }

        if (border.DataContext is not Device device)
        {
            return;
        }


        EditDeviceWindow editWindow = new(device,vm);

        bool result = await editWindow.ShowDialog<bool>(this);

        if (result)
        {
            _notifications.ShowSuccess("Device updated!");
            
        }

    }


    private async void Settings_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
        {
            return;
        }

        SettingsWindow settingsWindow=new SettingsWindow(vm);

        bool result=await settingsWindow.ShowDialog<bool>(this);
        
    }
}