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

namespace Achterbahn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int sitze;
        public static int besucher;
        public static int fahrten;



        public MainWindow()
        {
            InitializeComponent();
        }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            besucher = Int32.Parse(BesucherZahl.Text);
            sitze = Int32.Parse(SitzPlatze.Text);
            fahrten = Int32.Parse(FahrtenZahl.Text);

            for (int i = 0; i < besucher; i++)
            {

            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

        }

        Thread CreateThread()
        {
            ThreadStart worker = new ThreadStart(achterbahn);
            Thread t = new Thread(worker);
            return t;
        }


        private void achterbahn()
        {

        }
    }
}
