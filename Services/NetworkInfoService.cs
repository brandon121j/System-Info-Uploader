using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System;

namespace SystemInfoGrabber.Services;

public class NetworkInfoService
{
    private const string Na = "N/A";

    public string GetMacAddress()
    {
        var macAddress = NetworkInterface.GetAllNetworkInterfaces()
            .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up)
            ?.GetPhysicalAddress().ToString() ?? Na;

        if (macAddress == Na)
        {
            return macAddress;
        }

        // Formatting the MAC address
        var formattedMac = new StringBuilder();

        for (var i = 0; i < macAddress.Length; i += 2)
        {
            formattedMac.Append(macAddress.AsSpan(i, 2));
            if (i < macAddress.Length - 2)
            {
                formattedMac.Append(':');
            }
        }

        return formattedMac.ToString();
    }

    public string GetIpAddress()
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        return hostEntry.AddressList
            .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            ?.ToString() ?? Na;
    }

    
}
