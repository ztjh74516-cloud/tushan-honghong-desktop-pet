using System.Windows;
using System.Windows.Media.Imaging;

namespace TushanHonghong.DesktopPet.Services;

public static class FrameCropper
{
    public const int CellWidth = 192;
    public const int CellHeight = 208;

    public static CroppedBitmap Crop(BitmapSource atlas, int row, int frame)
    {
        ArgumentNullException.ThrowIfNull(atlas);
        return new CroppedBitmap(atlas, new Int32Rect(frame * CellWidth, row * CellHeight, CellWidth, CellHeight));
    }
}
