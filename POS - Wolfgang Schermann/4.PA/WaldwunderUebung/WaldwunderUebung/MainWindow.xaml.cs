using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WaldwunderUebung.model;

namespace WaldwunderUebung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataContext db;
        ObservableCollection<Waldwunder> obsWaldwunder;

        Table<Waldwunder> tableWaldwunder;

        string search;

        public MainWindow()
        {
            InitializeComponent();
            //Datenbankverbindung starten
            String cs = "Data Source=Waldwunder.db";
            SqliteConnection connection = new SqliteConnection(cs);
            db = new DataContext(connection);
            //-------

            //Tables holen
            tableWaldwunder = db.GetTable<Waldwunder>();
            refreshList();
        }

        private void Add_Waldwunder_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new AddWaldwunder();
            addDialog.Show();
        }

        public void refreshList()
        {
            Table<Waldwunder> tableWaldwunder = db.GetTable<Waldwunder>();
            obsWaldwunder = new ObservableCollection<Waldwunder>(tableWaldwunder.ToList());
            LB_Waldwunder.ItemsSource = obsWaldwunder;
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            search = TB_Search.Text;
            refreshSearch();
        }

        private void refreshSearch()
        {
            if(obsWaldwunder == null)
            {
                return;
            }

            var query = from p in tableWaldwunder select p;
            try
            {
                if(search.Length >= 2 && !String.IsNullOrEmpty(search))
                {
                    query = from p in tableWaldwunder where p.name.Contains(search) || p.description.Contains(search) || p.type.Contains(search) select p;
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            obsWaldwunder = new ObservableCollection<Waldwunder>(query);
            LB_Waldwunder.ItemsSource = obsWaldwunder;
        }

        private void LB_Waldwunder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var info = new InfoDialog();
            info.loadInfo((Waldwunder)LB_Waldwunder.SelectedItem);
            info.Show();
        }
    }
}
