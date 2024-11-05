using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;
using Assets.Catalog;
using Assets.Maintenance.Enums;

namespace Assets.Maintenance;

[Table("tech_map")]
public class TechMap : BaseEntity
{
    public int? AssetId { get; set; }
    [ForeignKey("AssetId")] public virtual Catalog.Asset? Asset { get; set; }
    public int? Duration { get; set; }
    public EWorkType? WorkType { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}