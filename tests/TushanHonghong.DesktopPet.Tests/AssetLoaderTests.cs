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

    [Fact]
    public void LoadDefinitions_ReadsIdleAnimationFromManifest()
    {
        var directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        var manifestPath = Path.Combine(directory, "animations.json");
        File.WriteAllText(manifestPath, """
            { "animations": [{ "name": "idle", "row": 0, "frames": [0, 1], "frameDurationMilliseconds": 100, "loops": true }] }
            """);

        var definition = Assert.Single(AssetLoader.LoadDefinitions(manifestPath));

        Assert.Equal("idle", definition.Name);
        Assert.Equal([0, 1], definition.Frames);
    }
}
