using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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
using WaldwunderSelfTry.model;

namespace WaldwunderSelfTry
{
    /// <summary>
    /// Interaction logic for AddWaldwunderDialog.xaml
    /// </summary>
    
    public enum bundesland
    {
        Niederösterreich,
        Oberösterreich,
        Wien,
        Burgenland,
        Steiermark,
        Kärnten,
        Salzburg,
        Tirol,
        Vorarlberg
    }

    public partial class AddWaldwunderDialog : Window
    {

        DataContext db;
        SqliteConnection connection;
        Table<Waldwunder> wald;

        public AddWaldwunderDialog()
        {
            InitializeComponent();
            String cs = "Data Source=Waldwunder.db";
            connection = new SqliteConnection(cs);
            db = new DataContext(connection);
            wald = db.GetTable<Waldwunder>();
            fillCombobox();
        }

        private void BTN_Add_Waldwunder_Click(object sender, RoutedEventArgs e)
        {
            
            if(String.IsNullOrEmpty(TB_Name.Text) || String.IsNullOrEmpty(TB_Desc.Text) || String.IsNullOrEmpty(TB_Lati.Text) || String.IsNullOrEmpty(TB_Long.Text) || String.IsNullOrEmpty(TB_Type.Text))
            {
                MessageBox.Show("Bitte überall etwas eingeben!");
                return;
            }

            Waldwunder waldwunder = new Waldwunder();

            waldwunder.name = TB_Name.Text;
            waldwunder.description = TB_Desc.Text;
            waldwunder.province = CB_Prov.SelectedItem.ToString();
            waldwunder.latitude = float.Parse(TB_Lati.Text);
            waldwunder.longitude = float.Parse(TB_Long.Text);
            waldwunder.type = TB_Type.Text;

            wald.InsertOnSubmit(waldwunder);

            db.SubmitChanges();
            db = new DataContext(connection);
            Mainwindow_LB_Reload();
        }


        public void fillCombobox()
        {
            for(int i = 0; i < 9; i++)
            {
                CB_Prov.Items.Add((bundesland)i);
            }
        }


        private void Mainwindow_LB_Reload()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            window.refresh();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Mainwindow_LB_Reload();
        }

        private void BTN_Close_Window_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Mainwindow_LB_Reload();
        }
    }
}
