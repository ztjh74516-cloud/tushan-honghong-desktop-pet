namespace TushanHonghong.DesktopPet.Services;

public static class PetWindowService
{
    public static (double Left, double Top) ClampPosition(
        double left,
        double top,
        double petWidth,
        double petHeight,
        double workAreaLeft,
        double workAreaTop,
        double workAreaWidth,
        double workAreaHeight)
    {
        var maximumLeft = Math.Max(workAreaLeft, workAreaLeft + workAreaWidth - petWidth);
        var maximumTop = Math.Max(workAreaTop, workAreaTop + workAreaHeight - petHeight);

        return (
            Math.Clamp(left, workAreaLeft, maximumLeft),
            Math.Clamp(top, workAreaTop, maximumTop));
    }
}
