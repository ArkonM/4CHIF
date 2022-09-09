using Microsoft.Data.Sqlite;
using PA4_Schneider_Armin.model;
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

namespace PA4_Schneider_Armin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        DataContext db;
        SqliteConnection connection;
        List<string> MediaTypeList = new List<string>();
        ObservableCollection<Media> obsMedia;

        Table<Media> mediaTable;


        public MainWindow()
        {
            InitializeComponent();
            //Datenbankverbindung starten
            String cs = "Data Source=Datenbank.db";
            connection = new SqliteConnection(cs);
            db = new DataContext(connection);
            //-------

            //Tables holen
            Table<MediaType> typeTable = db.GetTable<MediaType>();
            mediaTable = db.GetTable<Media>();
            convertTabletoList(typeTable);
            refreshList();
        }

        private void convertTabletoList(Table<MediaType> typeTable)
        {
            var query = from p in typeTable select p.Name;
            MediaTypeList.Add(query.ToString());
            for (int i = 0; i < query.Count(); i++)
            {
                CB_MediaType.Items.Add(query.ToList()[i]);
            }
        }

        public void refreshList()
        {
            mediaTable = db.GetTable<Media>();
            for(int i=0; i < mediaTable.Count(); i++)
            obsMedia = new ObservableCollection<Media>(mediaTable.ToList());
            LB_ShowData.ItemsSource = obsMedia;
        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_Title.Text) || string.IsNullOrEmpty(TB_Year.Text) || string.IsNullOrEmpty(TB_Publisher.Text) || !int.TryParse(TB_Year.Text, out int t))
            {
                MessageBox.Show("Sie müssen alle Felder füllen!");
            }
            //Get Informationen von TB/CB
            Media newInput = new Media();

            newInput.Title = TB_Title.Text;
            newInput.Year = int.Parse(TB_Year.Text);
            newInput.Publisher = TB_Publisher.Text;
            newInput.MediaType = CB_MediaType.SelectedIndex;


            //Hinzufügen zur Datenbank
            mediaTable.InsertOnSubmit(newInput);

            db.SubmitChanges();
            db = new DataContext(connection);

            refreshList();

        }

    }
}
