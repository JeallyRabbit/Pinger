using System;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace Pinger.Services;

public static class ThemeService
{
    private static IStyle? currentTheme;

    public static void ApplyTheme(string themeName)
    {
        Application? app = Application.Current;

        if (app is null)
            return;

        if (currentTheme is not null)
        {
            app.Styles.Remove(currentTheme);
            currentTheme = null;
        }

        string themeFile = themeName switch
        {
            "Catppuccin" => "CatppuccinTheme.axaml",
            "GruvBox" => "GruvboxTheme.axaml",
            "RosePine" => "RosePineTheme.axaml",
            _ => "GruvboxTheme.axaml"
        };

        currentTheme = new StyleInclude(new Uri("avares://Pinger/"))
        {
            Source = new Uri($"avares://Pinger/Styles/{themeFile}")
        };

        app.Styles.Add(currentTheme);
    }
}