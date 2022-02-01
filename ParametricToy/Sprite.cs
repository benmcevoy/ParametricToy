using Avalonia.Media;

namespace ParametricToy
{
    public class Sprite
    {
        private const int W = Config.Width;
        private const int H = Config.Height;

        public Sprite() => _pixels = new int[H * W];

        public static readonly Color[] Palette = Config.Palette;

        private readonly int[] _pixels;

        public int this[int x, int y]
        {
            get => _pixels[y * Width + x];
            set => _pixels[y * Width + x] = value;
        }

        public const int Width = W;
        public const int Height = H;
        public const int PaletteLength = 16;
        public const double CenterX = W / 2d;
        public const double CenterY = H / 2d;
    }
}