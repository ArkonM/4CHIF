using System.Data.Linq.Mapping;

namespace Waldwunderverwaltung
{
    [Table(Name = "Waldwunder")]
    public class Waldwunder
    {
        [Column(IsPrimaryKey = true)] public int? Id { get; set; }
        [Column] public string Name { get; set; }
        [Column] public string Description { get; set; }
        [Column] public string Province { get; set; }
        [Column] public double Latitude { get; set; }
        [Column] public double Longitude { get; set; }
        [Column] public string Type { get; set; }

    }
}
