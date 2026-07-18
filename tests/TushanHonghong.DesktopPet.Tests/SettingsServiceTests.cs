using System.Reflection;
using TushanHonghong.DesktopPet.Domain;
using TushanHonghong.DesktopPet.Services;

namespace TushanHonghong.DesktopPet.Tests;

public sealed class SettingsServiceTests
{
    [Fact]
    public void ApplicationAssembly_ExposesSettingsService()
    {
        var applicationAssembly = Assembly.Load("TushanHonghong.DesktopPet");

        Assert.NotNull(applicationAssembly.GetType("TushanHonghong.DesktopPet.Services.SettingsService"));
    }

    [Fact]
    public async Task SaveThenLoad_PreservesWindowSettings()
    {
        var temporaryDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var service = new SettingsService(temporaryDirectory);
        var expected = new PetSettings(100, 200, true, false);

        await service.SaveAsync(expected);

        Assert.Equal(expected, await service.LoadAsync());
    }
}
