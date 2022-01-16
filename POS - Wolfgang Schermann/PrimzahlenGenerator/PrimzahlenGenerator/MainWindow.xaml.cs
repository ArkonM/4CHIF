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
using System.Windows.Media.Animation;
using System.Diagnostics;

namespace PrimzahlenGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    

    public partial class MainWindow : Window
    {
        Storyboard r;
        PrimZahlenGen prim = new PrimZahlenGen();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchBut_Click(object sender, RoutedEventArgs e)
        {
            SearchBut.IsEnabled = false;
            EingabeBox.IsEnabled = false;
            int maxPoint;
            image1.Visibility = Visibility.Visible;
            r = (Storyboard)FindResource("loadingRotation");
            r.Begin(this, true);
            Int32.TryParse(EingabeBox.Text, out maxPoint);
            AsyncCallback callback = new AsyncCallback(CallbackMethod);
            prim.BeginPrim(maxPoint, callback, null);
        }

        public void CallbackMethod(IAsyncResult ar)
        {
            // Ergebnis der asynchronen Operation abholen
            int result = prim.EndPrim(ar);
            try
            {
                solution.Dispatcher.BeginInvoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      r.Stop(this);
                      solution.Content = "Ergebnis: " + result;
                      SearchBut.IsEnabled = true;
                      EingabeBox.IsEnabled = true;
                      image1.Visibility = Visibility.Hidden;
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}
