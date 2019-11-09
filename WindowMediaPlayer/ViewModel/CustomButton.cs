using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WindowMediaPlayer.ViewModel
{
    public class CustomButton : Button
    {
        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(CustomButton), new FrameworkPropertyMetadata(null));

        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ImageSourceHoverProperty = DependencyProperty.Register("ImageSourceHover", typeof(ImageSource), typeof(CustomButton), new FrameworkPropertyMetadata(null));

        public ImageSource ImageSourceHover
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceHoverProperty);
            }
            set
            {
                SetValue(ImageSourceHoverProperty, value);
            }
        }

        public static readonly DependencyProperty ImageSourcePressedProperty = DependencyProperty.Register("ImageSourcePressed", typeof(ImageSource), typeof(CustomButton), new FrameworkPropertyMetadata(null));

        public ImageSource ImageSourcePressed
        {
            get
            {
                return (ImageSource)GetValue(ImageSourcePressedProperty);
            }
            set
            {
                SetValue(ImageSourcePressedProperty, value);
            }
        }

        public static readonly DependencyProperty ImageSourceDisableProperty = DependencyProperty.RegisterAttached("ImageSourceDisable", typeof(ImageSource), typeof(CustomButton), new FrameworkPropertyMetadata(null));

        public ImageSource ImageSourceDisable
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceDisableProperty);
            }
            set
            {
                SetValue(ImageSourceDisableProperty, value);
            }
        }
    }
}
