using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
 
namespace WPFAlarmClock
{
    /// <summary>
    /// Interaktionslogik für DateTimeDlg.xaml
    /// </summary>
    public partial class DateTimeDlg : Window
    {
        private int minTime, sekTime;
 
        public int MinTime
        {
            get { return minTime; }
            set
            {
                minTime = value;
                this.minBox.Text = minTime.ToString();
            }
        }

        public int SekTime
        {
            get { return sekTime; }
            set
            {
                sekTime = value;
                this.sekBox.Text = sekTime.ToString();
            }
        }

        public DateTimeDlg()
        {
            InitializeComponent();
        }
 
        private void buttonOkay_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(this.minBox.Text, out minTime);
            Int32.TryParse(this.sekBox.Text, out sekTime);
            this.DialogResult = true;
            this.Close();
        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}