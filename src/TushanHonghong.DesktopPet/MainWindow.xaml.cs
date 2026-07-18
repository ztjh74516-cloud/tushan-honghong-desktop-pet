using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TushanHonghong.DesktopPet.Services;

namespace TushanHonghong.DesktopPet;

public partial class MainWindow : Window
{
    private readonly AnimationPlayer _player = new([0, 1, 2, 3, 4, 5, 6, 7], true);
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromMilliseconds(100) };
    private readonly BitmapImage _atlas;

    public bool IsPositionLocked { get; private set; }

    public MainWindow()
    {
        InitializeComponent();
        var assetPath = Path.Combine(AppContext.BaseDirectory, "Assets", "base", "spritesheet.webp");
        _atlas = new BitmapImage();
        _atlas.BeginInit();
        _atlas.UriSource = new Uri(assetPath, UriKind.Absolute);
        _atlas.CacheOption = BitmapCacheOption.OnLoad;
        _atlas.EndInit();
        _atlas.Freeze();
        _timer.Tick += (_, _) => { _player.Advance(); ShowCurrentFrame(); };
        Loaded += (_, _) => { ShowCurrentFrame(); _timer.Start(); };
        Closed += (_, _) => _timer.Stop();
        MouseLeftButtonDown += OnMouseLeftButtonDown;
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
    {
        if (!IsPositionLocked && eventArgs.ButtonState == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    public void SetPositionLocked(bool isLocked) => IsPositionLocked = isLocked;

    public void HidePet() => Hide();

    public void RestorePet()
    {
        Show();
        Activate();
    }

    public void SummonToCenter()
    {
        var workArea = SystemParameters.WorkArea;
        SetPosition(workArea.Left + (workArea.Width - Width) / 2, workArea.Top + (workArea.Height - Height) / 2);
    }

    public void SetPosition(double left, double top)
    {
        var workArea = SystemParameters.WorkArea;
        var position = PetWindowService.ClampPosition(left, top, Width, Height, workArea.Left, workArea.Top, workArea.Width, workArea.Height);
        Left = position.Left;
        Top = position.Top;
    }

    public void ReloadPet()
    {
        _player.Reset();
        ShowCurrentFrame();
    }

    private void ShowCurrentFrame() => PetImage.Source = FrameCropper.Crop(_atlas, 0, _player.CurrentFrame);
}
