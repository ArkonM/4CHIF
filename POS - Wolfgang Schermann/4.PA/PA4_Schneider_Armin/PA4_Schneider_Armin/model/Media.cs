using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA4_Schneider_Armin.model
{
    [Table(Name = "Media")]
    class Media
    {
        [Column(IsPrimaryKey = true)] public int? ID { get; set; }
        [Column] public string Title { get; set; }
        [Column] public int Year { get; set; }
        [Column] public int MediaType { get; set; }
        [Column] public string Publisher { get; set; }
        [Column] public string Image { get; set; }

        private EntitySet<MediaType> _mediaTypes = new EntitySet<MediaType>();
    }
}
