using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WindowMediaPlayer.ViewModel;

namespace WindowMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes variables
        /// </summary>
        /// 

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        #region ScaleValue Depdency Property
        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                return mainWindow.OnCoerceScaleValue((double)value);
            else
                return value;
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                mainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0f;

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {

        }

        public double ScaleValue
        {
            get
            {
                return (double)GetValue(ScaleValueProperty);
            }
            set
            {
                SetValue(ScaleValueProperty, value);
            }
        }
        #endregion

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FindScaleValue();
        }

        private void FindScaleValue()
        {
            double yScale = ActualHeight / 500f;    // taken from the MainWindow specification
            double xScale = ActualWidth / 686;     // taken from the MainWindow specification
            double value = Math.Min(xScale, yScale);
            ScaleValue = (double)OnCoerceScaleValue(myMainWindow, value);
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ToolTipOpenedHandler(object sender, RoutedEventArgs e)
        {
            ToolTip toolTip = (ToolTip)sender;
            UIElement target = toolTip.PlacementTarget;
            Point adjust = target.TranslatePoint(new Point(8, 0), toolTip);
            if (adjust.Y > 0)
            {
                toolTip.Placement = PlacementMode.Top;
            }
            toolTip.Tag = new Thickness(adjust.X, -1.5, 0, -1.5);
        }
    }
}