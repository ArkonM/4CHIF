using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA4_Schneider_Armin.model
{
    [Table (Name = "MediaType")]
    class MediaType
    {
        [Column(IsPrimaryKey = true, Name = "ID")] public int? ID { get; set; }
        [Column] public string Name { get; set; }



        private EntityRef<Media> _media = new EntityRef<Media>();

        [Association(Name = "Media",
            IsForeignKey = true, Storage = "_media", ThisKey = "ID")]

        public Media media
        {
            get { return _media.Entity; }
            set { _media.Entity = value; }
        }
    }
}
