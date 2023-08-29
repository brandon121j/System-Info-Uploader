using System;
using System.Text;

namespace SystemInfoGrabber.Services;

public class SystemInfoCoordinator
{
    public string GatherSystemInformation()
    {
        var adService = new ActiveDirectoryService();
        var sysService = new SystemInfoService();
        var netService = new NetworkInfoService();

        var userName = sysService.GetUsername();

        var sb = new StringBuilder();

        sb.AppendLine(adService.GetFullName(userName)); // Full Name
        sb.AppendLine(adService.GetEmail(userName)); // Email
        sb.AppendLine(netService.GetMacAddress()); // MAC Address
        sb.AppendLine(userName); // Username
        sb.AppendLine(sysService.GetComputerHostName()); // PC Name
        sb.AppendLine(netService.GetIpAddress()); // IP Address
        sb.AppendLine(sysService.GetDeviceType()); // Device Type
        sb.AppendLine(sysService.GetOsInstallationDate()); // OS Install Date
        sb.Append(sysService.GetTotalPhysicalMemoryInGb().Trim()); // RAM (without newline)
        sb.AppendLine(); // Add newline after RAM
        sb.Append(sysService.GetOsVersion()); // OS Version (without newline)

        var htmlFormattedSystemInfo = sb.ToString().Replace(Environment.NewLine, "<br>");

        return htmlFormattedSystemInfo;
    }
}