using Avalonia.Media;

namespace ParametricToy
{
    public static class Config
    {
        public const int Axis = 8;
        public const int Trails = 12;

        // square looks best
        public const int Width = 24;
        public const int Height = 24;

        public const double TimeIncrement = 1;
        public const double MaxSize = 2;
        public const double MaxPhase = 0.01;
        public const double MaxSpeed = 4;
        
        public static readonly Color[] Palette  = 
        {
            Color.FromRgb(0,0,0),
            Color.FromRgb(255, 0,   255),
            Color.FromRgb(170, 0   ,255),
            Color.FromRgb(84 , 0   ,255),
            Color.FromRgb(4  , 0   ,255),
            Color.FromRgb(0  , 80  ,255),
            Color.FromRgb(0  , 165 ,255),
            Color.FromRgb(0  , 250 ,255),
            Color.FromRgb(0  , 255 ,174),
            Color.FromRgb(0  , 255 ,89),
            Color.FromRgb(0  , 255 ,4),
            Color.FromRgb(80 , 255 ,0),
            Color.FromRgb(165, 255 ,0),
            Color.FromRgb(250, 255 ,0),
            Color.FromRgb(255, 174 ,0),
            Color.FromRgb(255, 89  ,0),
            Color.FromRgb(255, 4   ,0),
        };
    }
}