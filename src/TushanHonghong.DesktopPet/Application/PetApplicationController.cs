using TushanHonghong.DesktopPet.Domain;

namespace TushanHonghong.DesktopPet.Application;

public sealed class PetApplicationController
{
    public PetMainState State { get; private set; } = PetMainState.Idle;

    public void Hide() => State = PetMainState.Hidden;

    public void Restore() => State = PetMainState.Idle;
}
