using System.Reflection;
using TushanHonghong.DesktopPet.Services;

namespace TushanHonghong.DesktopPet.Tests;

public sealed class AnimationPlayerTests
{
    [Fact]
    public void ApplicationAssembly_ExposesAnimationPlayer()
    {
        var applicationAssembly = Assembly.Load("TushanHonghong.DesktopPet");

        Assert.NotNull(applicationAssembly.GetType("TushanHonghong.DesktopPet.Services.AnimationPlayer"));
    }

    [Fact]
    public void Advance_LoopingAnimationWrapsToFirstFrame()
    {
        var player = new AnimationPlayer([0, 1, 2, 3, 4, 5, 6, 7], true);
        player.MoveToFrame(7);

        player.Advance();

        Assert.Equal(0, player.CurrentFrame);
    }
}
