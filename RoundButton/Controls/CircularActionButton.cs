using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;

namespace RoundButton.Controls
{
    public class CircularActionButton : Button
    {
        public static readonly StyledProperty<Geometry> PathDataProperty = AvaloniaProperty.Register<CircularActionButton, Geometry>(nameof(PathData));
        public static readonly StyledProperty<SolidColorBrush> LoadingBackgroundProperty = AvaloniaProperty.Register<CircularActionButton, SolidColorBrush>(nameof(LoadingBackground));
        public static readonly StyledProperty<SolidColorBrush> LoadingForegroundProperty = AvaloniaProperty.Register<CircularActionButton, SolidColorBrush>(nameof(LoadingForeground));

        public static readonly StyledProperty<CornerRadius> CalculatedRadiusProperty = AvaloniaProperty.Register<CircularActionButton, CornerRadius>(nameof(CalculatedRadius));

        public static readonly DirectProperty<CircularActionButton, bool> IsLoadingProperty =
            AvaloniaProperty.RegisterDirect<CircularActionButton, bool>(
                nameof(IsLoading),
                o => o.IsLoading,
                (o, v) => o.IsLoading = v,
                unsetValue: false,
                defaultBindingMode: BindingMode.TwoWay);
        static CircularActionButton()
        {
            AffectsRender<CircularActionButton>(PathDataProperty,
                                                BackgroundProperty,
                                                ForegroundProperty,
                                                LoadingBackgroundProperty,
                                                LoadingForegroundProperty);
        }

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

        public SolidColorBrush LoadingBackground
        {
            get => GetValue(LoadingBackgroundProperty);
            set => SetValue(LoadingBackgroundProperty, value);
        }

        public SolidColorBrush LoadingForeground
        {
            get => GetValue(LoadingForegroundProperty);
            set => SetValue(LoadingForegroundProperty, value);
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
