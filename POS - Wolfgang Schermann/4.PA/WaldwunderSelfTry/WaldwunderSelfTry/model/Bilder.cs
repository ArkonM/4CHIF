using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaldwunderSelfTry.model
{

    [Table (Name = "Bilder")]
    class Bilder
    {
        [Column(IsPrimaryKey = true)] public int? id { get; set; }

        [Column] public string name { get; set; }
        [Column] public int wonder { get; set; }


        private EntityRef<Waldwunder> _waldwunder = new EntityRef<Waldwunder>();

        [Association(Name = "Waldwunder",
            IsForeignKey = true, Storage = "_waldwunder", ThisKey = "wonder")]

        public Waldwunder waldwunder
        {
            get { return _waldwunder.Entity; }
            set { _waldwunder.Entity = value; }
        }
    }
}
