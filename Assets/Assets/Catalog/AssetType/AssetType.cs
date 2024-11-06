using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;

namespace Assets.Catalog;

[Table("asset_type")]
public class AssetType : BaseEntity
{
    [Column("name")] public string? Name { get; set; }
}