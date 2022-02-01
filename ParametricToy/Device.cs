using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;

namespace ParametricToy
{
    public class Device : Image
    {
        private WriteableBitmap? _bitmap;
        private int[]? _buffer;

        public void Draw(Sprite sprite)
        {
            const int width = Sprite.Width;
            const int height = Sprite.Height;

            EnsureBitmap(width, height);

            if (_buffer == null) return;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var c = Sprite.Palette[sprite[x, y]];

                    _buffer[y * width + x] = (int)c.ToUint32();
                }
            }

            using var bmp = _bitmap?.Lock();

            if (bmp != null)
                Marshal.Copy(_buffer, 0, bmp.Address, bmp.Size.Width * bmp.Size.Height);

            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
        }

        private void EnsureBitmap(double width, double height)
        {
            if (_bitmap != null) return;

            _bitmap = new WriteableBitmap(
                PixelSize.FromSize(new Size(width, height), 1d),
                new Vector(96d, 96d),
                PixelFormat.Bgra8888,
                AlphaFormat.Opaque);

            Source = _bitmap;

            _buffer = new int[Sprite.Width * Sprite.Height];
        }
    }
}