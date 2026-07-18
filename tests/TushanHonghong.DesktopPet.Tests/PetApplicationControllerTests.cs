using System.Reflection;
using TushanHonghong.DesktopPet.Application;
using TushanHonghong.DesktopPet.Domain;

namespace TushanHonghong.DesktopPet.Tests;

public sealed class PetApplicationControllerTests
{
    [Fact]
    public void ApplicationAssembly_ExposesPetApplicationController()
    {
        var applicationAssembly = Assembly.Load("TushanHonghong.DesktopPet");

        Assert.NotNull(applicationAssembly.GetType("TushanHonghong.DesktopPet.Application.PetApplicationController"));
    }

    [Fact]
    public void RestoreAfterHide_ReturnsToIdle()
    {
        var controller = new PetApplicationController();
        controller.Hide();

        controller.Restore();

        Assert.Equal(PetMainState.Idle, controller.State);
    }
}
