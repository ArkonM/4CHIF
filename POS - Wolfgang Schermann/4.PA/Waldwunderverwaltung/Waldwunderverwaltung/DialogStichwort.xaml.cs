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
    /// Interaktionslogik für DialogStichwort.xaml
    /// </summary>
    public partial class DialogStichwort : Window
    {
        private string stichwortName;
        public string StichwortName { get { return stichwortName; } set { stichwortName = value; } }
        public DialogStichwort()
        {
            InitializeComponent();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            StichwortName = StichwortBox.Text;
            DialogResult = true;
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void StichwortBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (StichwortBox.Text.Trim().Length > 0)
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
