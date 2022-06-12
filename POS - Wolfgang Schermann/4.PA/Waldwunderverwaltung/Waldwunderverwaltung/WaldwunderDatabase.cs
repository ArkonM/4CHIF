using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waldwunderverwaltung
{
    [Database]
    public class WaldwunderDatabase : DataContext
    {

        private static SqliteConnection con = new SqliteConnection("Data Source=Waldwunder.db");
        public WaldwunderDatabase() : base(con) { }

        public Table<Bild> Bilder;
        public Table<Waldwunder> Waldwunder;

    }
}
