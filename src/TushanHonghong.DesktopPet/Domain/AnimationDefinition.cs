namespace TushanHonghong.DesktopPet.Domain;

public sealed record AnimationDefinition(
    string Name,
    int Row,
    IReadOnlyList<int> Frames,
    int FrameDurationMilliseconds,
    bool Loops);
