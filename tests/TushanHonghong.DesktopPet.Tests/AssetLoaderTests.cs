using System.Reflection;
using TushanHonghong.DesktopPet.Domain;
using TushanHonghong.DesktopPet.Services;

namespace TushanHonghong.DesktopPet.Tests;

public sealed class AssetLoaderTests
{
    [Fact]
    public void ApplicationAssembly_ExposesAnimationDefinition()
    {
        var applicationAssembly = Assembly.Load("TushanHonghong.DesktopPet");

        Assert.NotNull(applicationAssembly.GetType("TushanHonghong.DesktopPet.Domain.AnimationDefinition"));
    }

    [Fact]
    public void ApplicationAssembly_ExposesAssetLoader()
    {
        var applicationAssembly = Assembly.Load("TushanHonghong.DesktopPet");

        Assert.NotNull(applicationAssembly.GetType("TushanHonghong.DesktopPet.Services.AssetLoader"));
    }

    [Fact]
    public void AssetLoader_ExposesAnimationValidation()
    {
        var loaderType = Assembly.Load("TushanHonghong.DesktopPet")
            .GetType("TushanHonghong.DesktopPet.Services.AssetLoader");

        Assert.NotNull(loaderType!.GetMethod("Validate"));
    }

    [Fact]
    public void Validate_RejectsFrameOutsideEightColumns()
    {
        var definition = new AnimationDefinition("idle", 0, [0, 8], 100, true);

        Assert.Throws<InvalidDataException>(() => AssetLoader.Validate(definition));
    }
}
