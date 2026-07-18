using System.Reflection;

namespace TushanHonghong.DesktopPet.Tests;

public sealed class PetWindowServiceTests
{
    [Fact]
    public void ClampPosition_KeepsPetInsideWorkArea()
    {
        var applicationAssembly = Assembly.Load("TushanHonghong.DesktopPet");
        var serviceType = applicationAssembly.GetType("TushanHonghong.DesktopPet.Services.PetWindowService");

        Assert.NotNull(serviceType);
        var clamp = serviceType!.GetMethod("ClampPosition");
        Assert.NotNull(clamp);

        var result = ((double Left, double Top))clamp!.Invoke(null, [-50d, 900d, 192d, 208d, 0d, 0d, 800d, 600d])!;

        Assert.Equal(0d, result.Left);
        Assert.Equal(392d, result.Top);
    }
}
