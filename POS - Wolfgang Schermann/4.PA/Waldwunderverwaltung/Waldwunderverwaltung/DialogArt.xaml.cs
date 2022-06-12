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
using System.Windows.Shapes;

namespace Waldwunderverwaltung
{
    /// <summary>
    /// Interaktionslogik für DialogArt.xaml
    /// </summary>
    public partial class DialogArt : Window
    {
        private string artName;
        public string ArtName { get { return artName; } set { artName = value; } }
        public DialogArt()
        {
            InitializeComponent();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            ArtName = ArtBox.Text;
            DialogResult = true;
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ArtBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (ArtBox.Text.Trim().Length > 0)
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
