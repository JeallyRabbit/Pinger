using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Pinger.Helpers;
using Pinger.Models;
using Pinger.ViewModels;

namespace Pinger.Views;

public partial class ConfirmationWindow : Window
{

    private readonly String _name;
    private readonly String _ip;
     public ConfirmationWindow()
    {
        InitializeComponent();
    }

    public ConfirmationWindow(String name="Name", String ip="Ip") : this()
    {
        NameTextBlock.Text=name;
        IpTextBlock.Text=ip;
    }

    private void Confirm_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}