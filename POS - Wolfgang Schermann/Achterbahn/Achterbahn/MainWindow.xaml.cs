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
                CreateThread();
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

        }

        void CreateThread()
        {
            ThreadStart worker = new ThreadStart(achterbahn);
            Thread t = new Thread(worker);
            t.Start();
        }


        private void achterbahn()
        {
            while (true)
            {

                if (fahrtende)
                {
                    return;
                }
                lock(faehrt)
                {
                    if (fahrtende)
                    {
                        return; //lock reset
                    }
                    Monitor.wait(ende);
                    //achterbahn fährt bei x personen und fährt y lang (Monitor.PulseAll(ende);)

                }
                sleep(750);

            }

        }

    }
}
