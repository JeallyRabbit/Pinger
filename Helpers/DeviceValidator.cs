using System.Globalization;
using Pinger.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Pinger.Helpers;


public static class DeviceValidator
{
    public static bool IsValidIPv4(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        string[] parts = input.Trim().Split('.');

        if (parts.Length != 4)
            return false;

        foreach (string part in parts)
        {
            if (part.Length == 0)
                return false;

            if (!byte.TryParse(
                    part,
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out _))
            {
                return false;
            }
        }

        return true;
    }

    public static bool isUint(string a="")
    {
        if(uint.TryParse(a, out uint number))
        {
            return true;
        }
        return false;
    }

    public static bool IsDeviceNameAlreadySaved(IEnumerable<Device> devices,string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        string trimmedName = name.Trim();

        return devices.Any(device =>
            string.Equals(device.Name, trimmedName, StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsDeviceIpAlreadySaved(IEnumerable<Device> devices, string? ip)
    {
        if (string.IsNullOrWhiteSpace(ip))
            return false;

        string trimmedIp = ip.Trim();

        return devices.Any(device =>
            string.Equals(device.Ip, trimmedIp, StringComparison.OrdinalIgnoreCase));
    }


}