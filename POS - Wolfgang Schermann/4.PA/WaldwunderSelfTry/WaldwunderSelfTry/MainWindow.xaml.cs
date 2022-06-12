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
using WaldwunderSelfTry.model;

namespace WaldwunderSelfTry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataContext db;
        ObservableCollection<Waldwunder> waldwunderObs;
        Table<Waldwunder> waldwunderTable;
        Table<Bilder> bilderTable;

        public MainWindow()
        {
            InitializeComponent();
            String cs = "Data Source=Waldwunder.db";
            SqliteConnection connection = new SqliteConnection(cs);
            db = new DataContext(connection);

            refresh();
        }

        private void Add_Wadlwunder_Click(object sender, RoutedEventArgs e)
        {
            Window waldD = new AddWaldwunderDialog();
            waldD.Show();
        }

        public void refresh()
        {
            waldwunderTable = db.GetTable<Waldwunder>();
            waldwunderObs = new ObservableCollection<Waldwunder>(waldwunderTable);
            LB_Waldwunder.ItemsSource = waldwunderObs;
        }
    }
}
