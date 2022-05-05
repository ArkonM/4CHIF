using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SQL_Zugriff
{
    [Table(Name = "Books")]

    [Column(Name = "Category")] private int? categoryId;
    private EntityRef<Category> _category = new EntityRef<Category>();
    public class Book
    {
        [Column(IsPrimaryKey = true)] public int? id { get; set; }
        [Column] public string title { get; set; }
        [Column] public string author { get; set; }
        [Column] public int year { get; set; }
    }

    class Programm
    {
        static void Main(string[] args)
        {
            string cs = "Data Source=SQL_Test.db";

            SqliteConnection connection = new SqliteConnection(cs);
            DataContext db = new DataContext(connection);

            Table<Book> Books = db.GetTable<Book>();

            var query = from p in Books select p;
            foreach(var book in query)
            {
                Console.WriteLine(book.id + " " + book.title + " " + book.author + " " + book.year);
            }

            Console.ReadKey();

            Books.InsertOnSubmit(new Book() { title = "Test", author = "test", year = 2022 });
            db.SubmitChanges();

            query = from p in Books select p;
            foreach (var book in query)
            {
                Console.WriteLine(book.id + " " + book.title + " " + book.author + " " + book.year);
            }

            Console.ReadKey();
        }
        [Association(Name = "FK_Books_BookCategories",
            IsForeignKey = true, Storage = "_category", ThisKey = "categoryId")]
        public Category Category
        {
            get { return _category.Entity; }
            set { _category.Entity = value; }
        }
    }
}
