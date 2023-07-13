using Avalonia.Media;

namespace Kaleidoscope
{
    public class Sprite
    {
        public Sprite(int length)
        {
            Width = length;
            Height = length;
            _pixels = new int[length * length];
        }

        public static readonly Color[] Palette = Config.Palette;

        private readonly int[] _pixels;

        public int this[int x, int y]
        {
            get => _pixels[y * Width + x];
            set => _pixels[y * Width + x] = value;
        }

        public int Width;
        public int Height;
    }
}