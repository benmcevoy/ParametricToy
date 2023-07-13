using System;
using System.Diagnostics;

namespace ParametricToy
{
    public class TrailsRenderer
    {
        private const double TwoPi = Math.PI * 2d;
        private const int Axis = Config.Axis;
        private const int Trails = Config.Trails;
        private const double TimeIncrement = Config.TimeIncrement;

        public Sprite Render(RenderContext ctx)
        {
            var sprite = new Sprite();
            var t = ctx.Time;

            const double centerX = Sprite.CenterX;
            const double centerY = Sprite.CenterY;

            for (var i = 1; i < Trails; i++)
            {
                var x = t * ctx.Size;
                var y = centerY;
                var a = Math.Cos(t) * ctx.Phase;
                
                for (var j = 0; j < Axis; j++)
                {
                    var (x1, y1) = Rotate(a, x, y, centerX, centerY);
                    var x2 = Wrap(x1, Sprite.Width);
                    var y2 = Wrap(y1, Sprite.Height);
                    
                    sprite[x2, y2] = i % Sprite.PaletteLength;

                    a += TwoPi / Axis;
                }

                t -= TimeIncrement;
            }

            return sprite;
        }

        private static Tuple<double, double> Rotate(double angle, double x, double y, double centerX, double centerY)
        {
            var x1 = x - centerX;
            var y1 = y - centerY;

            var x2 = x1 * Math.Cos(angle) - y1 * Math.Sin(angle);
            var y2 = x1 * Math.Sin(angle) + y1 * Math.Cos(angle);

            return new Tuple<double, double>(x2 + centerX, y2 + centerY);
        }
        
        private static int Wrap(double value, double maxValue)
        {
            if (value < 0)
            {
                if (value % maxValue == 0) return 0;

                return (int)(value % maxValue + maxValue);
            }

            return value >= maxValue
                ? (int)(value % maxValue)
                : (int)(value);
        }
    }
}