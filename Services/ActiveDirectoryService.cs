using System.DirectoryServices.AccountManagement;

namespace SystemInfoGrabber.Services;

public class ActiveDirectoryService
{
    private const string Na = "N/A";

    private UserPrincipal GetUserPrincipal(string userName)
    {
        using var context = new PrincipalContext(ContextType.Domain);
        return UserPrincipal.FindByIdentity(context, userName);
    }

    public string GetFullName(string userName) => GetUserPrincipal(userName)?.DisplayName ?? Na;

    public string GetEmail(string userName) => GetUserPrincipal(userName)?.EmailAddress ?? Na;
}