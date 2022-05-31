using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
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
using System.Data.Linq;
using Microsoft.Data.Sqlite;

namespace WPF_SQL_Datenbank
{
    [Table(Name = "Books")]
    public class Book
    {
        [Column(IsPrimaryKey = true)] public int? id { get; set; }
        [Column] public string title { get; set; }
        [Column] public int year { get; set; }
    }

    public partial class MainWindow : Window
    {

        static string cs = "Data Source=SQL_Test.db";

        static SqliteConnection connection = new SqliteConnection(cs);
        static DataContext db = new DataContext(connection);

        Table<Book> Books = db.GetTable<Book>();

        public MainWindow()
        {
            InitializeComponent();
            getData();
        }

        private void getData()
        {
            DataContext db = new DataContext(connection);

            Books = db.GetTable<Book>();

            LB_Books.Items.Clear();
            var query = from p in Books select p;
            foreach (var book in query)
            {
                LB_Books.Items.Add(book.id + " " + book.title +  " " + book.year);
            }
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Books.InsertOnSubmit(new Book() { title = TB_title.Text, year = Int32.Parse(TB_year.Text) });
            db.SubmitChanges();
            getData();
        }
    }
}
