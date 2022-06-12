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
    /// Interaction logic for InfoDialog.xaml
    /// </summary>
    public partial class InfoDialog : Window
    {
        
        DataContext db;
        Table<Waldwunder> tableWaldwunder;
        Table<Bilder> tableBilder;

        public InfoDialog()
        {
            InitializeComponent();
            //Datenbankverbindung starten
            String cs = "Data Source=Waldwunder.db";
            SqliteConnection connection = new SqliteConnection(cs);
            db = new DataContext(connection);
            //-------

            //Tables holen
            tableWaldwunder = db.GetTable<Waldwunder>();
            tableBilder = db.GetTable<Bilder>();

        }

        

        public void loadInfo(Waldwunder WaldwunderInfo)
        {
            TX_Name.Text = WaldwunderInfo.name;
            TX_Desc.Text = WaldwunderInfo.description;
            TX_Prov.Text = WaldwunderInfo.province;
            TX_Lati.Text = WaldwunderInfo.latitude.ToString();
            TX_Long.Text = WaldwunderInfo.longitude.ToString();
            TX_Type.Text = WaldwunderInfo.type;


            //var query = from b in tableBilder join w in tableWaldwunder on b.wonder equals w.id where w.id == WaldwunderInfo.id select b;
            var query = from b in tableBilder where b.wonder == WaldwunderInfo.id select b.name;
            //Bildnamen aus datenbank holen
            List<string> bilderNamen = new List<string>();
            //GR_Bilder.ColumnDefinitions.Add(new ColumnDefinition());
            foreach (var q in query)
            {
                bilderNamen.Add(q);
                //GR_Bilder.RowDefinitions.Add(new RowDefinition());

            }



            for (int i = 0; i < bilderNamen.Count(); i++)
            {
                var path = "/Images/" + bilderNamen[i];
                Image image = new Image();
                ImageSource imageSource = new BitmapImage(new Uri(path, UriKind.Relative));
                image.Source = imageSource;
                image.Width = 300;
                image.Height = 150;
                Grid.SetRow(image, i);
                Grid.SetColumn(image, 0);
                GR_Bilder.Items.Add(image);

            }

        }

        private void GR_Bilder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var pic = new BildshowDialog((Image)GR_Bilder.SelectedItem);
            pic.Show();
        }
    }
}
