using System;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;

namespace Pinger.Helpers;

public class NotificationHelper
{
    private readonly WindowNotificationManager _notificationManager;

    public NotificationHelper(Window window)
    {
        _notificationManager = new WindowNotificationManager(window)
        {
            Position = NotificationPosition.TopRight,
            MaxItems = 3
        };
    }

    public void ShowSuccess(string message)
    {
        Show("Success", message, NotificationType.Success);
    }

    public void ShowError(string message)
    {
        Show("Error", message, NotificationType.Error);
    }

    public void ShowInfo(string message)
    {
        Show("Info", message, NotificationType.Information);
    }

    public void ShowWarning(string message)
    {
        Show("Warning", message, NotificationType.Warning);
    }

    private void Show(string title, string message, NotificationType type)
    {
        Dispatcher.UIThread.Post(() =>
        {
            _notificationManager.Show(
                new Avalonia.Controls.Notifications.Notification(
                    title,
                    message,
                    type,
                    TimeSpan.FromSeconds(3)));
        });
    }
}