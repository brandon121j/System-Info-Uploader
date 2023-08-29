using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace SystemInfoGrabber.Services;

public class NetworkInfoService
{
    private const string Na = "N/A";

    public string GetMacAddress() => NetworkInterface.GetAllNetworkInterfaces()
        .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up)
        ?.GetPhysicalAddress().ToString() ?? Na;

    public string GetIpAddress()
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        return hostEntry.AddressList
            .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            ?.ToString() ?? Na;
    }
}