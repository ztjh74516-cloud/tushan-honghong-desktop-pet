using System.IO;
using System.Text.Json;
using TushanHonghong.DesktopPet.Domain;

namespace TushanHonghong.DesktopPet.Services;

public static class AssetLoader
{
    public static IReadOnlyList<AnimationDefinition> LoadDefinitions(string manifestPath)
    {
        if (string.IsNullOrWhiteSpace(manifestPath))
        {
            throw new ArgumentException("A manifest path is required.", nameof(manifestPath));
        }

        using var stream = File.OpenRead(manifestPath);
        var manifest = JsonSerializer.Deserialize<AnimationManifest>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidDataException("Animation manifest is empty.");

        var definitions = manifest.Animations ?? [];
        foreach (var definition in definitions)
        {
            Validate(definition);
        }

        return definitions;
    }

    public static void Validate(AnimationDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (string.IsNullOrWhiteSpace(definition.Name))
        {
            throw new InvalidDataException("Animation name is required.");
        }

        if (definition.Row is < 0 or > 10)
        {
            throw new InvalidDataException("Animation row must be between 0 and 10.");
        }

        if (definition.Frames.Count == 0)
        {
            throw new InvalidDataException("Animation must contain at least one frame.");
        }

        if (definition.Frames.Any(frame => frame is < 0 or > 15))
        {
            throw new InvalidDataException("Animation frames must be between 0 and 15.");
        }

        if (definition.FrameDurationMilliseconds <= 0)
        {
            throw new InvalidDataException("Animation frame duration must be positive.");
        }
    }

    private sealed record AnimationManifest(IReadOnlyList<AnimationDefinition>? Animations);
}
