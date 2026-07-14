using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Pinger.Models;
using Pinger.Helpers;
using System.Diagnostics;

namespace Pinger.Services;

public class SettingsService
{
    private readonly string _settingsPath;
    


    
    public SettingsService()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        string folder = Path.Combine(appData, "Pinger");

        Directory.CreateDirectory(folder);

        _settingsPath = Path.Combine(folder, "settings.json");
       
    }

    public async Task<AppSettings> LoadAsync()
    {
        if (!File.Exists(_settingsPath))
            return new AppSettings();

        string json = await File.ReadAllTextAsync(_settingsPath);

        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
    }

    public async Task SaveAsync(AppSettings settings)
    {
        string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(_settingsPath, json);

        

    }
}