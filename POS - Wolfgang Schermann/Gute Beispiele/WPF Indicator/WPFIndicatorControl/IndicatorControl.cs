using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFIndicatorControl
{
    public class IndicatorControl : Control
    {
        static IndicatorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IndicatorControl), new FrameworkPropertyMetadata(typeof(IndicatorControl)));
        }


        public static readonly DependencyProperty
            MinimumProperty = DependencyProperty.Register(
                    "Minimum",
                  typeof(int),
                     typeof(IndicatorControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                , new PropertyChangedCallback(OnMinChanged)));

        public int Minimum
        {
            get { return (int)base.GetValue(MinimumProperty); }
            set { base.SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty
            MaximumProperty = DependencyProperty.Register(
                    "Maximum",
                  typeof(int),
                     typeof(IndicatorControl),
            new FrameworkPropertyMetadata(100, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                , new PropertyChangedCallback(OnMaxChanged)));

        public int Maximum
        {
            get { return (int)base.GetValue(MaximumProperty); }
            set { base.SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty
            ValueProperty = DependencyProperty.Register(
                    "Value",
                  typeof(int),
                     typeof(IndicatorControl),
            new FrameworkPropertyMetadata(50, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                , new PropertyChangedCallback(OnValChanged)));

        public int Value
        {
            get { return (int)base.GetValue(ValueProperty); }
            set { base.SetValue(ValueProperty, value); }
        }

        private static void OnMinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IndicatorControl ind = (IndicatorControl)d;
            ind.minimumBlock.Text = ind.Minimum.ToString();
            ind.RotateAngle();
        }

        private static void OnMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IndicatorControl ind = (IndicatorControl)d;
            ind.maximumBlock.Text = ind.Maximum.ToString();
            ind.RotateAngle();
        }

        private static void OnValChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IndicatorControl ind = (IndicatorControl)d;
            ind.valueBlock.Text = ind.Value.ToString();
            ind.RotateAngle();
        }
        
        private void RotateAngle()
        {
            double angle = 287.0 / (Maximum - Minimum) * (Value - Minimum);
            nadelImage.RenderTransform = new RotateTransform(angle, 75, 75);
        }


        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }

        private Image messgerateImage, nadelImage;
        private TextBlock minimumBlock, maximumBlock, valueBlock;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            messgerateImage = (Image)this.Template.FindName("PART_MESSGERAET", this);
            messgerateImage.Source = loadBitmap(Resource1.Messgeraet);

            nadelImage = (Image)this.Template.FindName("PART_NADEL", this);
            nadelImage.Source = loadBitmap(Resource1.Nadel);

            minimumBlock = (TextBlock)this.Template.FindName("PART_MINBLOCK", this);
            minimumBlock.Text = Minimum.ToString();

            maximumBlock = (TextBlock)this.Template.FindName("PART_MAXBLOCK", this);
            maximumBlock.Text = Maximum.ToString();

            valueBlock = (TextBlock)this.Template.FindName("PART_VALBLOCK", this);
            valueBlock.Text = Value.ToString();
        }
    }
}
