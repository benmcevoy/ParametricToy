using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace ParametricToy
{
    public class MainWindow : Window
    {
        private static readonly DispatcherTimer Timer = new();
        private static readonly TrailsRenderer TrailsRenderer = new ();
        private static double _time;
        private static readonly RenderContext Context = new() { Sprite = new Sprite() };
        
        private Slider Size => this.FindControl<Slider>("Size");
        private Slider Phase => this.FindControl<Slider>("Phase");
        private Slider Speed => this.FindControl<Slider>("Speed");
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
            
            Size.Maximum = Config.MaxSize;
            Phase.Maximum = Config.MaxPhase;
            Speed.Maximum = Config.MaxSpeed;

            Timer.Interval = TimeSpan.FromMilliseconds(1000d / 50d);
            Timer.Tick += Update_Tick;
            Timer.Start();
        }

        private void Update_Tick(object? sender, EventArgs e)
        {
            Context.Size = Size.Value;
            Context.Phase = Phase.Value;
            Context.Time = _time;
            Context.Sprite = TrailsRenderer.Render(Context);

            Device?.Draw(Context.Sprite);

            _time += Speed.Value;
        }
    }
}
