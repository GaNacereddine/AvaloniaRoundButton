using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;

namespace RoundButton.Controls
{
    public class CircularActionButton : Button
    {
        public static readonly StyledProperty<Geometry> PathDataProperty = AvaloniaProperty.Register<CircularActionButton, Geometry>(nameof(PathData));

        public static readonly StyledProperty<CornerRadius> CalculatedRadiusProperty = AvaloniaProperty.Register<CircularActionButton, CornerRadius>(nameof(CalculatedRadius));

        public static readonly DirectProperty<CircularActionButton, bool> IsLoadingProperty =
            AvaloniaProperty.RegisterDirect<CircularActionButton, bool>(
                nameof(IsLoading),
                o => o.IsLoading,
                (o, v) => o.IsLoading = v,
                unsetValue: false,
                defaultBindingMode: BindingMode.TwoWay);
        
        public static readonly DirectProperty<CircularActionButton, SolidColorBrush> NormalBackgroundProperty =
            AvaloniaProperty.RegisterDirect<CircularActionButton, SolidColorBrush>(
                nameof(NormalBackground),
                o => o.NormalBackground,
                (o, v) => o.NormalBackground = v,
                unsetValue: new SolidColorBrush(Colors.Black),
                defaultBindingMode: BindingMode.TwoWay);
 
        public static readonly DirectProperty<CircularActionButton, SolidColorBrush> NormalForegroundProperty =
            AvaloniaProperty.RegisterDirect<CircularActionButton, SolidColorBrush>(
                nameof(NormalForeground),
                o => o.NormalForeground,
                (o, v) => o.NormalForeground = v,
                unsetValue: new SolidColorBrush(Colors.Black),
                defaultBindingMode: BindingMode.TwoWay);
 
        public static readonly DirectProperty<CircularActionButton, SolidColorBrush> LoadingBackgroundProperty =
            AvaloniaProperty.RegisterDirect<CircularActionButton, SolidColorBrush>(
                nameof(LoadingBackground),
                o => o.LoadingBackground,
                (o, v) => o.LoadingBackground = v,
                unsetValue: new SolidColorBrush(Colors.Black),
                defaultBindingMode: BindingMode.TwoWay);
 
        public static readonly DirectProperty<CircularActionButton, SolidColorBrush> LoadingForegroundProperty =
            AvaloniaProperty.RegisterDirect<CircularActionButton, SolidColorBrush>(
                nameof(LoadingForeground),
                o => o.LoadingForeground,
                (o, v) => o.LoadingForeground = v,
                unsetValue: new SolidColorBrush(Colors.Black),
                defaultBindingMode: BindingMode.TwoWay);
 
        public CircularActionButton()
        {
            CalculatedRadius = new CornerRadius(20);
            EffectiveViewportChanged += CircularSwitchButton_EffectiveViewportChanged;
        }

        protected override void OnClick()
        {
            if (!IsLoading)
            {
                base.OnClick();
            }
        }

        private void CircularSwitchButton_EffectiveViewportChanged(object? sender, global::Avalonia.Layout.EffectiveViewportChangedEventArgs e)
        {
            if (e.EffectiveViewport.Width > e.EffectiveViewport.Height)
            {
                SetCalculatedRadius(new CornerRadius(e.EffectiveViewport.Width / 2));
            }
            else
            {
                SetCalculatedRadius(new CornerRadius(e.EffectiveViewport.Height / 2));
            }
        }

        public Geometry PathData
        {
            get => GetValue(PathDataProperty);
            set => SetValue(PathDataProperty, value);
        }

        private SolidColorBrush _loadingBackground;
        public SolidColorBrush LoadingBackground
        {
            get => _loadingBackground;
            set => SetAndRaise(LoadingBackgroundProperty, ref _loadingBackground, value);
        }

        private SolidColorBrush _loadingForeground;
        public SolidColorBrush LoadingForeground
        {
            get => _loadingForeground;
            set => SetAndRaise(LoadingForegroundProperty, ref _loadingForeground, value);
        }

        private SolidColorBrush _normalBackground;
        public SolidColorBrush NormalBackground
        {
            get => _normalBackground;
            set => SetAndRaise(NormalBackgroundProperty, ref _normalBackground, value);
        }

        private SolidColorBrush _normalForeground;
        public SolidColorBrush NormalForeground
        {
            get => _normalForeground;
            set => SetAndRaise(NormalForegroundProperty, ref _normalForeground, value);
        }

        private void SetCalculatedRadius(CornerRadius value)
        {
            SetValue(CalculatedRadiusProperty, value);
        }

        public CornerRadius CalculatedRadius
        {
            get => GetValue(CalculatedRadiusProperty);
            set
            {
                // It was not meant to be used by outside consumers 
                // and private setters seemed to be not a good fit
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetAndRaise(IsLoadingProperty, ref _isLoading, value);
        }

    }
}
