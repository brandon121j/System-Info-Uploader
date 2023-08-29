using System;
using System.Linq;
using System.Management;
using System.Net;

namespace SystemInfoGrabber.Services;

public class SystemInfoService
{
    private const string Unknown = "Unknown";
    private const string NoSystemBattery = "NoSystemBattery";
    private const string Desktop = "Desktop";
    private const string Laptop = "Laptop";

    public string GetUsername() => Environment.UserName;

    public string GetComputerHostName() => Dns.GetHostName();

    public string GetDeviceType() =>
        System.Windows.Forms.SystemInformation.PowerStatus.BatteryChargeStatus.ToString() == NoSystemBattery
            ? Desktop
            : Laptop;

    public string GetOsInstallationDate()
    {
        using var searcher = new ManagementObjectSearcher("SELECT InstallDate FROM Win32_OperatingSystem");
        foreach (var mo in searcher.Get())
        {
            var property = mo.Properties["InstallDate"];

            var value = property.Value;

            var installDateString = value.ToString();
            if (string.IsNullOrEmpty(installDateString)) continue;

            var installDate = ManagementDateTimeConverter.ToDateTime(installDateString);
            return installDate.ToShortDateString(); // returns in format MM/dd/yyyy
        }

        return "Unknown";
    }

    public string GetTotalPhysicalMemoryInGb()
    {
        using var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
        foreach (var mo in searcher.Get())
        {
            var ramBytes = Convert.ToDouble(mo.Properties["TotalPhysicalMemory"].Value);
            const double bytesInAGigabyte = 1024 * 1024 * 1024;
            var ram = Math.Ceiling(ramBytes / bytesInAGigabyte);
            return $"{ram} GB";
        }

        return "Unknown";
    }

    public string GetOsVersion()
    {
        var osType = Environment.Is64BitOperatingSystem ? " 64-bit" : " 32-bit";
        var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get()
                .Cast<ManagementObject>()
            select x.GetPropertyValue("Caption")).FirstOrDefault();
        var osVersion = name != null ? name.ToString() : Unknown;
        osVersion = osVersion?.Replace("Microsoft", "").Trim();
        return $"{osVersion}{osType}";
    }
}