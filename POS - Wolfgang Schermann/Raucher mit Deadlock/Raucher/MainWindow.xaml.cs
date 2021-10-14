using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Raucher
{
    public enum Zutaten { Tabak, Papier, Streichhölzer, Nichts };

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread[] threads;
        private KettenRaucher[] raucher;
        static public Haendler haendler;
        static public Random random;

        public static void UpdateText(TextBox box, String text, SolidColorBrush color)
        {
            try
            {
                box.Dispatcher.BeginInvoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      box.Text = text;
                      box.Background = color;
                      box.UpdateLayout();
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            random = new Random();

            threads = new Thread[4];
            raucher = new KettenRaucher[3];
            raucher[0] = new KettenRaucher(Zutaten.Papier, textBox3, "Malboro Man (P)");
            raucher[1] = new KettenRaucher(Zutaten.Streichhölzer, textBox4, "Joe Camel (S)");
            raucher[2] = new KettenRaucher(Zutaten.Tabak, textBox5, "Humphrey Bogart (T)");
            haendler = new Haendler(textBox1, textBox2, listBox);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = false;
            button2.IsEnabled = true;

            threads[0] = haendler.NeuerThread();
            for(int i = 0; i < 3; i++)
                threads[i + 1] = raucher[i].NeuerThread();

            for (int i = 0; i < 4; i++)
            {
                threads[i].Start();
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                threads[i].Abort();
            }

            button1.IsEnabled = true;
            button2.IsEnabled = false;
        }
    }
}
