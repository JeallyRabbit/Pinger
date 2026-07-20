using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Pinger.Helpers;
using Pinger.Models;
using Pinger.ViewModels;

namespace Pinger.Views;

public partial class EditDeviceWindow : Window
{
    private readonly Device? _device;
    private readonly MainWindowViewModel? _vm;

     public EditDeviceWindow()
    {
        InitializeComponent();
    }
    public EditDeviceWindow(Device device, MainWindowViewModel vm) : this()
    {
        _device = device;
        _vm = vm;

        NameTextBox.Text = device.Name;
        IpTextBox.Text = device.Ip;
    }

    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (_device is null || _vm is null)
        {
            Close(false);
            return;
        }

        string newName = NameTextBox.Text?.Trim() ?? "";
        string newIp = IpTextBox.Text?.Trim() ?? "";

        ErrorTextBlock.Text = "";

        if (string.IsNullOrWhiteSpace(newName))
        {
            ErrorTextBlock.Text = "Device name cannot be empty.";
            return;
        }

        if (string.IsNullOrWhiteSpace(newIp))
        {
            ErrorTextBlock.Text = "IP address cannot be empty.";
            return;
        }

        if (!DeviceValidator.IsValidIPv4(newIp))
        {
            ErrorTextBlock.Text = "IP address has wrong format.";
            return;
        }

        bool nameAlreadyExists = _vm.Devices.Any(otherDevice =>
            otherDevice != _device &&
            string.Equals(otherDevice.Name, newName, StringComparison.OrdinalIgnoreCase));

        if (nameAlreadyExists)
        {
            ErrorTextBlock.Text = "Device name already exists.";
            return;
        }

        bool ipAlreadyExists = _vm.Devices.Any(otherDevice =>
            otherDevice != _device &&
            string.Equals(otherDevice.Ip, newIp, StringComparison.OrdinalIgnoreCase));

        if (ipAlreadyExists)
        {
            ErrorTextBlock.Text = "IP address already exists.";
            return;
        }

        
        _device.Name = newName;
        _device.Ip = newIp;

        Close(true);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}