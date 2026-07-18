using TushanHonghong.DesktopPet.Domain;

namespace TushanHonghong.DesktopPet.Application;

public sealed class PetApplicationController
{
    public PetMainState State { get; private set; } = PetMainState.Idle;

    public bool IsPositionLocked { get; private set; }

    public void Hide() => State = PetMainState.Hidden;

    public void Restore() => State = PetMainState.Idle;

    public void TogglePositionLock() => IsPositionLocked = !IsPositionLocked;
}
