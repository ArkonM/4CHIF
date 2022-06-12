using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waldwunderverwaltung
{
    [Table(Name = "Bilder")]
    public class Bild
    {
        [Column(IsPrimaryKey = true)] public int? Id { get; set; }
        [Column] public string Name { get; set; }

        [Column(Name = "wonder")] private int? waldwunderId;

        private EntityRef<Waldwunder> _waldwunder = new EntityRef<Waldwunder>();

        [Association(Name = "FK_Bilder_BildWaldwunder",
            IsForeignKey = true, Storage = "_waldwunder", ThisKey = "waldwunderId")]
        public Waldwunder Waldwunder
        {
            get { return _waldwunder.Entity; }
            set { _waldwunder.Entity = value; }
        }

    }
}
