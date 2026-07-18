namespace TushanHonghong.DesktopPet.Domain;

public sealed record PetSettings(double Left, double Top, bool IsPositionLocked, bool IsHidden)
{
    public static PetSettings Default { get; } = new(double.NaN, double.NaN, false, false);
}
