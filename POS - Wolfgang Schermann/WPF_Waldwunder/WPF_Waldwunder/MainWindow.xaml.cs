using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
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

namespace WPF_Waldwunder
{

    [Table(Name = "Waldwunder")]
    public class Waldwunder
    {
        [Column(IsPrimaryKey = true)] public int? id { get; set; }
        [Column] public string name { get; set; }
        [Column] public string description { get; set; }
        [Column] public string province { get; set; }
        [Column] public float latitude{ get; set; }
        [Column] public float longitude { get; set; }
        [Column] public string type { get; set; }
        [Column] public int votes { get; set; }
    }

    [Table(Name = "Bilder")]
    public class Bilder
    {
        [Column(IsPrimaryKey = true)] public int? id { get; set; }
        [Column] public string name { get; set; }
        [Column] public Waldwunder wonder { get; set; }
    }


    public partial class MainWindow : Window
    {

        static string cs = "Data Source=SQL_Test.db";

        static SqliteConnection connection = new SqliteConnection(cs);
        static DataContext db = new DataContext(connection);

        Table<Waldwunder> Waldwunders = db.GetTable<Waldwunder>();
        Table<Bilder> Bilder = db.GetTable<Bilder>();

        WaldwunderList waldwunderList = new WaldwunderList();

        public MainWindow()
        {
            InitializeComponent();


            for (int i = 0; i < 9; i++)
            {
                CBX_Bundesland.Items.Add((BundeslandEnum)i);
            }

            search();
        }

        private void search()
        {
            List<Waldwunder> wunderList = new List<Waldwunder>();
            var query = from w in Waldwunders select w;
            wunderList = query.ToList();
            waldwunderList.waldwunderList = new System.Collections.ObjectModel.ObservableCollection<Waldwunder>(wunderList);
            LB_DBresult.ItemsSource = waldwunderList.waldwunderList;
        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            Waldwunder wunder = new Waldwunder();
            wunder.name = TXB_Name.Text;
            wunder.description = TXB_Descr.Text;
            wunder.province = CBX_Bundesland.Text;
            wunder.latitude =  float.Parse(TXB_Latit.Text);
            wunder.longitude = float.Parse(TXB_Longit.Text);
            wunder.type = TXB_Type.Text;

            Waldwunders.InsertOnSubmit(wunder);
        }
    }
}
