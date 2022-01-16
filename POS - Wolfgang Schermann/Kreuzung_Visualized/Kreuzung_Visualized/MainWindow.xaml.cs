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

namespace Kreuzung_Visualized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Richtung { Norden, Osten, Sueden, Westen }
        int NdSd = 0;

        ManualResetEvent NordSued = new ManualResetEvent(true);
        AutoResetEvent OstWest = new AutoResetEvent(true);

        Random r = new Random();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void carDrive(Object id)
        {
            Richtung richtung = (Richtung) r.Next(4);
            switch (richtung)
            {
                case Richtung.Norden:
                    addToListBox(ListNorden, "Auto: " + id);
                    break;

                case Richtung.Osten:
                    addToListBox(ListOsten, "Auto: " + id);
                    break;

                case Richtung.Sueden:
                    addToListBox(ListSueden, "Auto: " + id);
                    break;

                case Richtung.Westen:
                    addToListBox(ListWesten, "Auto: " + id);
                    break;
            }
            Thread.Sleep(1000 + r.Next(9000));
            cross(id, richtung);
        }


        private void cross(object id, Richtung richtung)
        {
            if(richtung == Richtung.Norden || richtung == Richtung.Sueden)
            {
                lock (this)
                {
                    NdSd++;
                }
                OstWest.Reset();
                NordSued.WaitOne();
            } 
            
            else
            {
                OstWest.WaitOne();
                NordSued.Reset();
            }
            
                switch (richtung)
                {
                    case Richtung.Norden:
                        removeFromListBox(ListNorden, "Auto: " + id);
                        break;

                    case Richtung.Osten:
                        removeFromListBox(ListOsten, "Auto: " + id);
                        break;

                    case Richtung.Sueden:
                        removeFromListBox(ListSueden, "Auto: " + id);
                        break;

                    case Richtung.Westen:
                        removeFromListBox(ListWesten, "Auto: " + id);
                        break;
                }
                addToListBox(CrossRoad, "Auto: " + id + " : " + richtung);
                Thread.Sleep(1000);
                removeFromListBox(CrossRoad, "Auto: " + id + " : " + richtung);

            lock (this)
            {
                if (richtung == Richtung.Norden || richtung == Richtung.Sueden)
                {
                    NdSd--;
                    if (NdSd == 0)
                    {
                        NordSued.Reset();
                        OstWest.Set();
                    }
                }
                else
                {
                    if(NdSd > 0)
                    {
                        NordSued.Set();
                        OstWest.Reset();
                    } else
                    {
                        OstWest.Set();
                    }
                }
            }
        }

        private void addToListBox(ListBox listbox, string text)
        {
            try
            {
                listbox.Dispatcher.BeginInvoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      //eigentliche Änderungen
                      listbox.Items.Add(text);
                      listbox.UpdateLayout();
                      //--
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }


        private void removeFromListBox(ListBox listbox, string text)
        {
            try
            {
                listbox.Dispatcher.BeginInvoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      //eigentliche Änderungen
                      listbox.Items.Remove(text);
                      listbox.UpdateLayout();
                      //--
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i=0; i<50; i++)
            {
                Thread t = new Thread(carDrive);
                t.Start(i);
            }
        }
    }
}
