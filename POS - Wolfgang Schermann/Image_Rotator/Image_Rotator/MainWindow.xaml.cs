using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace Image_Rotator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string select;
        string destination;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Select_Folder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            select = dialog.SelectedPath;
        }

        private void Destination_Folder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            destination = dialog.SelectedPath;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            FileStream stream = new FileStream(destination, FileMode.Create);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.FlipHorizontal = (bool) GetValue(HFlip);
            encoder.FlipVertical = false;
            encoder.QualityLevel = 30;
            encoder.Rotation = Rotation.Rotate90;
            encoder.Frames.Add(BitmapFrame.Create(new Uri(select, UriKind.Relative)));
            encoder.Save(stream);
            stream.Close();
        }
    }
}
