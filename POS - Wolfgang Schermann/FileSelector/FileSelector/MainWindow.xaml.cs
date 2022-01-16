using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<String> startFolder;
        private Worker obj;
        private Storyboard r;

        public MainWindow()
        {
            InitializeComponent();
            startFolder = new List<String>();
            r = (Storyboard)FindResource("loadingRotation");
        }

        private void PathSelect_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            startFolder.Add(dialog.SelectedPath);
            PathBox.Items.Add(dialog.SelectedPath);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (startFolder.Count == 0)
                {
                    System.Windows.MessageBox.Show("Bitte Ausgangsordner auswählen");
                    return;
                }
            }
            catch (Exception)
            {
                throw; 
            }

            obj = new Worker();
            AsyncCallback Call = new AsyncCallback(search);
            obj.Start(startFolder, Call, null);
            Image1.Visibility = Visibility.Visible;
            r.Begin(this, true); 
        }

        public void search(IAsyncResult res)
        {
            List<String> list = obj.EndScan(res);
            try
            {
                this.Dispatcher.BeginInvoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      foreach (string i in list)
                      {
                          ResultBox.Items.Add(i);
                      }
                      r.Stop(this);
                      Image1.Visibility = Visibility.Hidden;
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void PathRemove_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            startFolder.Remove(dialog.SelectedPath);
            PathBox.Items.Remove(dialog.SelectedPath);
            for(int i = 0; i < ResultBox.Items.Count;)
            {
                ResultBox.Items.RemoveAt(i);
            }
           
        }
    }
}
