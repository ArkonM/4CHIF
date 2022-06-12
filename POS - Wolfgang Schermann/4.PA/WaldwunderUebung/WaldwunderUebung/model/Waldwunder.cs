using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaldwunderUebung.model
{
    [Table (Name = "Waldwunder")]
    public class Waldwunder
    {
        [Column(IsPrimaryKey = true)] public int? id { get; set; }

        [Column] public string name { get; set; }
        [Column] public string description { get; set; }
        [Column] public string province { get; set; }
        [Column] public float latitude { get; set; }
        [Column] public float longitude { get; set; }
        [Column] public string type { get; set; }
        [Column] public string votes { get; set; }

        private EntitySet<Bilder> _bilder = new EntitySet<Bilder>(); 
    }
}
