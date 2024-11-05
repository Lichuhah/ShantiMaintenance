using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;

namespace Assets.Catalog;

[Table("asset")]
public class Asset: BaseEntity
{
    [Column("name")] public string? Name { get; set; }
    [Column("key")] public string? Key { get; set; }
    [Column("rul")] public DateTime? Rul { get; set; }
}