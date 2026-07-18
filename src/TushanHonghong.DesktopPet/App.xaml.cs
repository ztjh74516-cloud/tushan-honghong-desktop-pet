using System.Windows;
using TushanHonghong.DesktopPet.Application;
using TushanHonghong.DesktopPet.Domain;
using TushanHonghong.DesktopPet.Services;

namespace TushanHonghong.DesktopPet;

public partial class App : System.Windows.Application
{
    private readonly PetApplicationController _controller = new();
    private readonly SettingsService _settingsService = new();
    private MainWindow? _window;
    private TrayService? _trayService;

    protected override async void OnStartup(StartupEventArgs eventArgs)
    {
        base.OnStartup(eventArgs);
        var settings = await _settingsService.LoadAsync();
        _window = new MainWindow();
        ApplySettings(settings);
        _window.Show();
        _trayService = new TrayService(HidePet, RestorePet, ToggleLock, SummonPet, ReloadPet, Shutdown);
        _trayService.SetLockState(_controller.IsPositionLocked);
        if (settings.IsHidden)
        {
            HidePet();
        }
    }

    protected override void OnExit(ExitEventArgs eventArgs)
    {
        if (_window is not null)
        {
            _settingsService.SaveAsync(new PetSettings(
                _window.Left,
                _window.Top,
                _controller.IsPositionLocked,
                _controller.State == PetMainState.Hidden)).GetAwaiter().GetResult();
        }

        _trayService?.Dispose();
        base.OnExit(eventArgs);
    }

    private void ApplySettings(PetSettings settings)
    {
        if (_window is null)
        {
            return;
        }

        _window.SetPositionLocked(settings.IsPositionLocked);
        if (!double.IsNaN(settings.Left) && !double.IsNaN(settings.Top))
        {
            _window.SetPosition(settings.Left, settings.Top);
        }
    }

    private void HidePet()
    {
        _controller.Hide();
        _window?.HidePet();
    }

    private void RestorePet()
    {
        _controller.Restore();
        _window?.RestorePet();
    }

    private void ToggleLock()
    {
        _controller.TogglePositionLock();
        _window?.SetPositionLocked(_controller.IsPositionLocked);
        _trayService?.SetLockState(_controller.IsPositionLocked);
    }

    private void SummonPet()
    {
        RestorePet();
        _window?.SummonToCenter();
    }

    private void ReloadPet() => _window?.ReloadPet();
}
