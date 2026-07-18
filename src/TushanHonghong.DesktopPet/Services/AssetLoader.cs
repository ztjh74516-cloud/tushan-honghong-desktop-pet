using System.IO;
using TushanHonghong.DesktopPet.Domain;

namespace TushanHonghong.DesktopPet.Services;

public static class AssetLoader
{
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

        if (definition.Frames.Any(frame => frame is < 0 or > 7))
        {
            throw new InvalidDataException("Animation frames must be between 0 and 7.");
        }

        if (definition.FrameDurationMilliseconds <= 0)
        {
            throw new InvalidDataException("Animation frame duration must be positive.");
        }
    }
}
