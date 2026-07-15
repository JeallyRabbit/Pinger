using System.Collections.Generic;

namespace Pinger.Models;

public class AppSettings
{
    public List<Device> Devices { get; set; } = new();

    public int MaxFails { get; set; } = 5;

    public string EmailSender { get; set; } = "";
    public string EmailReceiver { get; set; } = "";
    public string SMTPHost {get;set;}="";
    public int SMTPPort {get; set; }=25;
    public string ThemeChoice { get; set; } = "GruvBox";
}