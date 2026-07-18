using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TushanHonghong.DesktopPet.Services;

namespace TushanHonghong.DesktopPet;

public partial class MainWindow : Window
{
    private readonly AnimationPlayer _player;
    private readonly DispatcherTimer _timer;
    private readonly TimeSpan _frameDuration;
    private TimeSpan _elapsedSinceFrameAdvance;
    private readonly BitmapImage _atlas;

    public bool IsPositionLocked { get; private set; }

    public MainWindow()
    {
        InitializeComponent();
        var manifestPath = Path.Combine(AppContext.BaseDirectory, "Assets", "base", "animations.json");
        var idleAnimation = AssetLoader.LoadDefinitions(manifestPath).Single(definition => definition.Name == "idle");
        _player = new AnimationPlayer(idleAnimation.Frames, idleAnimation.Loops);
        _frameDuration = TimeSpan.FromMilliseconds(idleAnimation.FrameDurationMilliseconds);
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1d / 30d) };
        var assetPath = Path.Combine(AppContext.BaseDirectory, "Assets", "base", "spritesheet.png");
        _atlas = new BitmapImage();
        _atlas.BeginInit();
        _atlas.UriSource = new Uri(assetPath, UriKind.Absolute);
        _atlas.CacheOption = BitmapCacheOption.OnLoad;
        _atlas.EndInit();
        _atlas.Freeze();
        _timer.Tick += (_, _) =>
        {
            _elapsedSinceFrameAdvance += _timer.Interval;
            if (_elapsedSinceFrameAdvance < _frameDuration)
            {
                return;
            }

            _elapsedSinceFrameAdvance -= _frameDuration;
            _player.Advance();
            ShowCurrentFrame();
        };
        Loaded += (_, _) =>
        {
            ShowCurrentFrame();
            if (idleAnimation.Frames.Count > 1)
            {
                _timer.Start();
            }
        };
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
