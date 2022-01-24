using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PA2_4CHIF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateValue));
        }

        SinusSchwingung schwingung = new SinusSchwingung();

        private void UpdateValue(object ar)
        {
            double t = 0.0;
            while (true)
            {
                try
                {
                    this.Dispatcher.BeginInvoke(
                        System.Windows.Threading.DispatcherPriority.Normal
                        , new System.Windows.Threading.DispatcherOperationCallback(delegate
                        {
                            if (Frequenz.Text.Length != 0 && Amplitude.Text.Length != 0)
                            {
                                Wert.Content = schwingung.CalculateValue(t, Int32.Parse(Frequenz.Text), Int32.Parse(Amplitude.Text));
                            }
                            return null;
                        }), null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                t += 0.01;
                Thread.Sleep(10);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Label l = new Label();
            l.Content = "Platzhalter";
            Schwingungen.Children.Add(l);
        }

        private void Aktualisieren_Click(object sender, RoutedEventArgs e)
        {
            if (Frequenz.Text.Length != 0 && Amplitude.Text.Length != 0)
            {
                Wert.Content = schwingung.CalculateValue(t, Int32.Parse(Frequenz.Text), Int32.Parse(Amplitude.Text));
            }
        }
    }
}
