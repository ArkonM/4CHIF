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

namespace Indikator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Indikator.changeAngle(Slider.Value, minSlider.Value, maxSlider.Value);
        }

        private void minSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider.Minimum = minSlider.Value;
            maxSlider.Minimum = minSlider.Value + 10;
            Indikator.changeAngle(Slider.Value, minSlider.Value, maxSlider.Value);
        }

        private void maxSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider.Maximum = maxSlider.Value;
            Indikator.changeAngle(Slider.Value, minSlider.Value, maxSlider.Value);
        }
    }
}
