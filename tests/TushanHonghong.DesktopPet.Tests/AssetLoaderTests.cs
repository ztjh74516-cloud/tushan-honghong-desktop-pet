using System.Reflection;
using System.Windows.Media.Imaging;
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

    [Fact]
    public void WpfDecoder_PreservesTransparencyForAtlasCorner()
    {
        var assetPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "src", "TushanHonghong.DesktopPet", "Assets", "base", "spritesheet.png");
        var atlas = new BitmapImage(new Uri(assetPath, UriKind.Absolute));
        var pixels = new byte[4];

        atlas.CopyPixels(new System.Windows.Int32Rect(0, 0, 1, 1), pixels, 4, 0);

        Assert.Equal(0, pixels[3]);
    }
}
