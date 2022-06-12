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
using WaldwunderUebung.model;

namespace WaldwunderUebung
{
    /// <summary>
    /// Interaction logic for AddWaldwunder.xaml
    /// </summary>
    public enum bundesland
    {
        Niederoesterreich,
        Burgenland,
        Tirol,
        Vorarlberg,
        Steiermark,
        Kaernten,
        Oberoesterreich,
        Salzburg,
        Wien
    }


    public partial class AddWaldwunder : Window
    {

        DataContext db;
        SqliteConnection connection;
        //Tables holen
        Table<Waldwunder> tableWaldwunder;

        public AddWaldwunder()
        {
            InitializeComponent();
            //Datenbankverbindung starten
            String cs = "Data Source=Waldwunder.db";
            connection = new SqliteConnection(cs);
            db = new DataContext(connection);
            //-------
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Prüfen ob ausgewählt
            if(String.IsNullOrEmpty(TX_Name.Text) || String.IsNullOrEmpty(TX_Desc.Text) || String.IsNullOrEmpty(TX_Lati.Text) || String.IsNullOrEmpty(TX_Long.Text) || String.IsNullOrEmpty(TX_Type.Text))
            {
                MessageBox.Show("Alle Felder müssen ausgefüllt sein!");
                return;
            }

            //Waldwunder Objekt anlegen
            Waldwunder newWaldwunder = new Waldwunder();
            newWaldwunder.name = TX_Name.Text;
            newWaldwunder.description = TX_Desc.Text;
            newWaldwunder.province = CB_Prov.SelectedItem.ToString();
            newWaldwunder.latitude = float.Parse(TX_Lati.Text);
            newWaldwunder.longitude = float.Parse(TX_Long.Text);
            newWaldwunder.type = TX_Type.Text;


            //Objekt zur Tabelle hinzufügen
            tableWaldwunder = db.GetTable<Waldwunder>();
            tableWaldwunder.InsertOnSubmit(newWaldwunder);


            db.SubmitChanges();

            db = new DataContext(connection);
            TX_Name.Text = "";
            TX_Desc.Text = "";
            TX_Lati.Text = "";
            TX_Long.Text = "";
            TX_Type.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Combobox befüllen
            for(int i=0; i < 9; i++)
            {
                CB_Prov.Items.Add((bundesland)i);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            window.refreshList();
        }
    }
}
