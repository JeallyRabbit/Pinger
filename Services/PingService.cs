using System;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Pinger.Services;

public class PingService
{
    public bool Ping(string ip, int timeoutMs = 1000)
    {
        try
        {
            using Ping ping = new();

            PingReply reply = ping.Send(ip, timeoutMs);
            Debug.WriteLine(ip.ToString()+" ping: "+ reply.Status.ToString());
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
}