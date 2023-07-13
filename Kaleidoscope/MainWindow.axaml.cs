using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace Kaleidoscope
{
    public class MainWindow : Window
    {
        private static readonly DispatcherTimer Timer = new();
        private static readonly RenderContext Context = new() { Sprite = new Sprite(100) };
        private Slider Triple => this.FindControl<Slider>("Triple");
        private Slider Iterations => this.FindControl<Slider>("Iterations");
        private Slider Size => this.FindControl<Slider>("Size");
        private Device Device => this.FindControl<Device>("Device");

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            Timer.Interval = TimeSpan.FromMilliseconds(1000d / 50d);

            Timer.Tick += Update_Tick;

            Timer.Start();
        }

        private void Update_Tick(object? sender, EventArgs e)
        {
            Context.Sprite = Next(Context);

            Device?.Draw(Context.Sprite);
        }

        private static Random Random = new Random();

        private Sprite Next(RenderContext context)
        {
            /*
 let a = 22
 let b = 0
 let c = b
 for d = random

  let b = random
  let c = random
  let b = b && b <= a
  let c = c && c <= a

  plot a+b, a+c
  plot a+b, a-c
  plot a-b, a+c
  plot a-b, a-c

  plot a+c, a+b
  plot a+c, a-b
  plot a-c, a+b
  plot a-c, a-b
 next d
            */
            var a = (int)Size.Value;
            var sprite = new Sprite(a * 2);
            var iterations = Iterations.Value;
            var tripleIndex = (int)Triple.Value;
            var triple = _triples[tripleIndex];

            int b = 0;
            int c = 0;

            for (var i = 0; i < iterations; i++)
            {
                var color = tripleIndex != 12 ? Prng(triple)/16 : Random.Next(0, 16);

                b = tripleIndex != 12 ? Prng(triple) : Random.Next(0, 255);
                c = tripleIndex != 12 ? Prng(triple) : Random.Next(0, 255);

                if ((b + a) > sprite.Height - 2) continue;
                if ((c + a) > sprite.Height - 2) continue;

                sprite[a + b, a + c] = color;
                sprite[a + b, a - c] = color;
                sprite[a - b, a + c] = color;
                sprite[a - b, a - c] = color;

                sprite[a + c, a + b] = color;
                sprite[a + c, a - b] = color;
                sprite[a - c, a + b] = color;
                sprite[a - c, a - b] = color;
            }

            return sprite;
        }

        private List<Tuple<int, int, int>> _triples = new List<Tuple<int, int, int>>
        {
            Tuple.Create( 1, 1, 2 ),
            Tuple.Create( 1, 1, 3 ),
            Tuple.Create( 1, 7, 3 ),
            Tuple.Create( 1, 7, 6 ),
            Tuple.Create( 1, 7, 7 ),
            Tuple.Create( 2, 5, 5 ),
            Tuple.Create( 3, 1, 5 ),
            Tuple.Create( 3, 5, 4 ),
            Tuple.Create( 3, 5, 5 ),
            Tuple.Create( 3, 5, 7 ),
            Tuple.Create( 5, 3, 6 ),
            Tuple.Create( 5, 3, 7),
            Tuple.Create( 0,0,0),
        };

        private static byte _rnd = 1;
        private byte Prng(Tuple<int, int, int> triple)
        {
            byte r = (byte)(System.Numerics.BitOperations.RotateLeft(_rnd, triple.Item1));
            _rnd = (byte)(r ^ _rnd);

            r = (byte)(System.Numerics.BitOperations.RotateRight(_rnd, triple.Item2));
            _rnd = (byte)(r ^ _rnd);

            r = (byte)(System.Numerics.BitOperations.RotateLeft(_rnd, triple.Item3));
            _rnd = (byte)(r ^ _rnd);

            return _rnd;
        }
    }
}

