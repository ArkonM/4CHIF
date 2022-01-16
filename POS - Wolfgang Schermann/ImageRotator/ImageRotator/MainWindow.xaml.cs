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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageRotator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string startFolder = "", destinyFolder = "";
        private int rotate;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AusgangsOrdnerButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            startFolder = dialog.SelectedPath;
           // startFolder = @"C:\Users\Elvin\source\repos\ImageRotator\ImageRotator\images";
            AusgangsOrdnerLabel.Content = startFolder;
        }

        private void ZielOrdnerButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            destinyFolder = dialog.SelectedPath;
           // destinyFolder = @"C:\Users\Elvin\source\repos\ImageRotator\ImageRotator\converted-images";
            ZielOrdnerLabel.Content = destinyFolder;
        }

        private void BearbeitenButton_Click(object sender, RoutedEventArgs e)
        {
            if (startFolder.Equals("") || destinyFolder.Equals(""))
            {
                System.Windows.MessageBox.Show("Bitte Ausgangs- und Zielordner auswählen");
                return;
            }

            Int32.TryParse(drehenComboBox.SelectionBoxItem.ToString(), out rotate);

            try
            {

                var jpgFiles = Directory.EnumerateFiles(startFolder, "*.jpg");

                StatusProgressBar.Maximum = jpgFiles.ToArray().Length;


                foreach (string currentFile in jpgFiles)
                {
                    string fileName = currentFile.Substring(startFolder.Length + 1);
                    DataFile data = new DataFile(fileName, startFolder, destinyFolder);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Worker), data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Worker(object a)
        {
            DataFile data = a as DataFile;

            Rotation toRotate = Rotation.Rotate0;

            if (rotate == 90) toRotate = Rotation.Rotate90;
            else if (rotate == 180) toRotate = Rotation.Rotate180;
            else if (rotate == 270) toRotate = Rotation.Rotate270;

            try
            {
                this.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      //eigentliche Änderungen
                      

                      FileStream stream = new FileStream(data.endPath + @"\" + data.FileName, FileMode.Create);
                      JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                      encoder.FlipHorizontal = (bool)HorizontalCheckbox.IsChecked;
                      encoder.FlipVertical = (bool)VerticalCheckbox.IsChecked;
                      encoder.QualityLevel = (int)QualitySlider.Value;
                      encoder.Rotation = toRotate;
                      encoder.Frames.Add(BitmapFrame.Create(new Uri(data.startPath + @"\" + data.FileName, UriKind.Relative)));
                      encoder.Save(stream);
                      stream.Close();
                      StatusProgressBar.Value++;
                      //--
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