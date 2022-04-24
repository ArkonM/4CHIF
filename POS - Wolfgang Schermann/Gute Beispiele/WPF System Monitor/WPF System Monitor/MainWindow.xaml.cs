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

namespace WPF_System_Monitor
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SystemData sysData;

        public MainWindow()
        {
            InitializeComponent();

            sysData = new SystemData();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Visibility_Click(object sender, RoutedEventArgs e)
        {
            MonitorPanel.Visibility = MonitorPanel.IsVisible ? Visibility.Hidden : MonitorPanel.Visibility = Visibility.Visible;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // this.Close();
            App.Current.Shutdown();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            RAMIndicator.Minimum = 0;
            RAMIndicator.Maximum = (int)(sysData.GetPhysicalMemoryMaximum() / Math.Pow(10, 9));

            CPUIndicator.Minimum = 0;
            CPUIndicator.Maximum = 100;

            /*Thread cpuThread = new Thread(MonitorCPU);
            Thread ramThread = new Thread(MonitorRAM);

            cpuThread.IsBackground = true;
            ramThread.IsBackground = true;

            cpuThread.Start();
            ramThread.Start();*/

            Thread thread = new Thread(Monitoring);
            thread.IsBackground = true;
            thread.Start();

        }

        private void Monitoring()
        {
            while (true)
            {
                try
                {
                    RAMIndicator.Dispatcher.Invoke(
                      System.Windows.Threading.DispatcherPriority.Normal
                      , new System.Windows.Threading.DispatcherOperationCallback(delegate
                      {
                          //eigentliche Änderungen
                          RAMIndicator.Value = (int)(sysData.GetPhysicalMemoryCurrent() / Math.Pow(10, 9));
                          //--
                          return null;
                      }), null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

                try
                {
                    CPUIndicator.Dispatcher.Invoke(
                      System.Windows.Threading.DispatcherPriority.Normal
                      , new System.Windows.Threading.DispatcherOperationCallback(delegate
                      {
                          //eigentliche Änderungen
                          CPUIndicator.Value = (int)sysData.GetProcessorPercent();
                          //--
                          return null;
                      }), null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                Thread.Sleep(1000);
            }
        }

       /* private void MonitorRAM()
        {

            while (true)
            {
                try
                {
                    RAMIndicator.Dispatcher.Invoke(
                      System.Windows.Threading.DispatcherPriority.Normal
                      , new System.Windows.Threading.DispatcherOperationCallback(delegate
                      {
                          //eigentliche Änderungen
                          RAMIndicator.Value = (int)(sysData.GetPhysicalMemoryCurrent() / Math.Pow(10, 9));
                          //--
                          return null;
                      }), null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                Thread.Sleep(1000);
            }
        }

        private void MonitorCPU()
        {
            while (true)
            {
                try
                {
                    CPUIndicator.Dispatcher.Invoke(
                      System.Windows.Threading.DispatcherPriority.Normal
                      , new System.Windows.Threading.DispatcherOperationCallback(delegate
                      {
                          //eigentliche Änderungen
                          CPUIndicator.Value = (int)sysData.GetProcessorPercent();
                          //--
                          return null;
                      }), null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                Thread.Sleep(1000);
            }
        }*/
    }
}
