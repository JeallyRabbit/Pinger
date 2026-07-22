using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Pinger.Helpers;
using Pinger.Models;
using Pinger.ViewModels;

namespace Pinger.Views;

public partial class SettingsWindow : Window
{
    private readonly MainWindowViewModel? _vm;

    private readonly NotificationHelper _notifications;

    public SettingsWindow()
    {
        InitializeComponent();
        
        _notifications = new NotificationHelper(this);
    }
    public SettingsWindow(MainWindowViewModel vm) : this()
    {
        _vm = vm;
        _notifications = new NotificationHelper(this);

        
        SmtpIPTextBox.Text = _vm.SmtpPort;
        SmtpPortTextBox.Text = _vm.SmtpPort;
        MailSenderTextBox.Text = _vm.EmailSender;
        MailReceiverTextBox.Text = _vm.EmailReceiver;
        PingTimeoutTextBox.Text = _vm.PingTimeout;
        PingIntervalTextBox.Text = _vm.PingInterval;
        PingMaxFailsTextBox.Text = _vm.MaxFails;
        NotificationIntervalTextBox.Text = _vm.NotificationInterval;

    }

    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (_vm is null)
        {
            Close(false);
            return;
        }
        bool noErrors = true;
        if (!DeviceValidator.IsValidIPv4(SmtpIPTextBox.Text))
        {
            _notifications.ShowError("Invalid SMTP Ip");
            noErrors = false;
        }

        if (!DeviceValidator.isUint(SmtpPortTextBox.Text))
        {

            _notifications.ShowError("Invalid SMTP Port");
            noErrors = false;
        }


        if (String.IsNullOrEmpty(MailSenderTextBox.Text) && String.IsNullOrEmpty(_vm.EmailSender))
        {
            _notifications.ShowError("No sender email provided");
            noErrors = false;
        }

        if (String.IsNullOrEmpty(MailReceiverTextBox.Text) && String.IsNullOrEmpty(_vm.EmailReceiver))
        {
            _notifications.ShowError("No receiver email provided");
            noErrors = false;
        }




        if (!DeviceValidator.isUint(PingTimeoutTextBox.Text))
        {

            _notifications.ShowError("Invalid Ping timeout");
            noErrors = false;
        }

        if (!DeviceValidator.isUint(PingIntervalTextBox.Text))
        {

            _notifications.ShowError("Invalid Ping interval ");
            noErrors = false;
        }

        if (!DeviceValidator.isUint(PingMaxFailsTextBox.Text))
        {

            _notifications.ShowError("Invalid ping fails to notification");
            noErrors = false;
        }

        if (!DeviceValidator.isUint(NotificationIntervalTextBox.Text))
        {

            _notifications.ShowError("Invalid notification interval");
            noErrors = false;
        }

        if (noErrors)
        {
            _vm.SmtpIP = SmtpIPTextBox.Text;
            _vm.SmtpPort = Int32.Parse(SmtpPortTextBox.Text);
            _vm.EmailSender = MailSenderTextBox.Text;
            _vm.EmailReceiver = MailReceiverTextBox.Text;
            _vm.PingTimeout = Int32.Parse(PingTimeoutTextBox.Text);
            _vm.PingInterval = Int32.Parse(PingIntervalTextBox.Text);
            _vm.MaxFails = Int32.Parse(PingMaxFailsTextBox.Text);
            _vm.NotificationInterval = Int32.Parse(NotificationIntervalTextBox.Text);



            await _vm.SaveValuesAsync();
            _notifications.ShowSuccess("Saved");
            Close(true);
        }

    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {


        Close(false);
    }
}