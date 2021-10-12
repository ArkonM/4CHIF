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
using System.Diagnostics;
using System.Threading;


namespace Philosphen_Gabeln
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            /*
            Philosopher p0 = new Philosopher(0, Dimitri, 4, 0);
            Philosopher p1 = new Philosopher(1, Pavel, 0, 1);
            Philosopher p2 = new Philosopher(2, Schabans, 1, 2);
            Philosopher p3 = new Philosopher(3, Nogsch, 2, 3);
            Philosopher p4 = new Philosopher(4, Güzlü, 3, 4);
            
            Gabel g0 = new Gabel(0, 1);
            Gabel g1 = new Gabel(1, 2);
            Gabel g2 = new Gabel(2, 3);
            Gabel g3 = new Gabel(3, 4);
            Gabel g4 = new Gabel(4, 0);
            */
        }

        private void Güzlü_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Schabans_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Nogsch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Dimitri_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Pavel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Change()
        {
            try
            {
                Güzlü.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                      //eigentliche Änderungen
                      Güzlü.Text = "denkt";
                    Güzlü.Background = Brushes.DarkGray;
                    Güzlü.UpdateLayout();
                      //--
                      return null;
                }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        Thread CreateThread()
        {
            ThreadStart worker = new ThreadStart(Change);
            Thread t = new Thread(worker);
            return t;
        }
    }


    class Philosopher {

        private int id;
        private Label label;
        private int gabelL;
        private int gabelR;

        Philosopher(int id, Label label, int gabelR, int gabelL)  //System.Windows.Controls.Label
        {
            this.id = id;
            this.label = label;
            this.gabelR = gabelR;
            this.gabelL = gabelL;
        }
    }


    class Gabel {

        private int id;
        private int philoL;
        private int philoR;

        Gabel(int id, int philoR, int philoL)
        {
            this.id = id;
            this.philoL = philoR;
            this.philoR = philoL;
        }
    
    }
        
}
