using System.Reflection;

namespace TushanHonghong.DesktopPet.Tests;

public sealed class TrayServiceTests
{
    [Fact]
    public void ApplicationAssembly_ExposesTrayService()
    {
        var applicationAssembly = Assembly.Load("TushanHonghong.DesktopPet");

        Assert.NotNull(applicationAssembly.GetType("TushanHonghong.DesktopPet.Services.TrayService"));
    }
}
