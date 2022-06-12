using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Waldwunderverwaltung
{
    /// <summary>
    /// Interaktionslogik für Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {

        private string forestwonderName, description, province, type;
        private double latitude, longitude;
        private string[] filePaths;
        private const string imgPath = @"C:\Users\elvin\source\repos\Waldwunderverwaltung\Waldwunderverwaltung\images";


        public ObservableCollection<string> safeFileNames = new ObservableCollection<string>();

        public string ForestwonderName { get => forestwonderName; set => forestwonderName = value; }
        public string Description { get => description; set => description = value; }
        public string Province { get => province; set => province = value; }
        public string Type { get => type; set => type = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }


        public Dialog()
        {
            InitializeComponent();
            BilderLB.ItemsSource = safeFileNames;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            forestwonderName = WaldwundernameBox.Text;

            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                BeschreibungBox.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                BeschreibungBox.Document.ContentEnd
            );

            description = textRange.Text;
            province = BundeslandCBox.SelectionBoxItem.ToString();
            type = ArtBox.Text;
            latitude = (double)LatitudeNum.Value;
            longitude = (double)LongitudeNum.Value;

            string fileName = string.Empty;
            string ext = string.Empty;

            for (int i = 0; i < filePaths.Length; i++)
            {
                if (!System.IO.Path.GetDirectoryName(filePaths[i]).Equals(imgPath))
                {
                    fileName = System.IO.Path.GetFileName(filePaths[i]);

                    while (File.Exists(imgPath + @"\" + fileName))
                    {
                        ext = System.IO.Path.GetExtension(fileName);
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                        Int32.TryParse(fileName.Substring(fileName.Length - 1, 1), out int num);

                        num++;
                        fileName = fileName.Substring(0, fileName.Length - 1);
                        fileName += num.ToString() + ext;
                    }

                    File.Copy(filePaths[i], imgPath + @"\" + fileName);
                    safeFileNames[i] = fileName;
                }
            }

            DialogResult = true;
            Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void WaldwundernameBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CheckInput();
        }

        private void BilderAuswahlButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\elvin\source\repos\Waldwunderverwaltung\Waldwunderverwaltung\images";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach(var item in openFileDialog.SafeFileNames)
                {
                    safeFileNames.Add(item);
                }
                filePaths = openFileDialog.FileNames;
            }

            CheckInput();
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = BilderLB.SelectedItems;

            for (int i = 0; i < selectedItems.Count; i++)
            {
                int idx = safeFileNames.IndexOf((string)selectedItems[i]);
                safeFileNames.RemoveAt(idx);
                i--;
            }
            CheckInput();
        }

        private void BundeslandCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckInput();
        }

        private void BeschreibungBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CheckInput();
        }

        private void ArtBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CheckInput();
        }

        private void CheckInput()
        {

            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                BeschreibungBox.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                BeschreibungBox.Document.ContentEnd
            );

            if (WaldwundernameBox.Text.Trim().Length > 0 && ArtBox.Text.Trim().Length > 0
                 && BundeslandCBox.SelectedIndex > -1 && textRange.Text.Length > 0
                 && BilderLB.Items.Count > 0)
            {
                OKButton.IsEnabled = true;
            }
            else
            {
                OKButton.IsEnabled = false;
            }
        }


    }
}
