using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;
using Assets.Catalog;
using Assets.Maintenance.Enums;

namespace Assets.Maintenance;

[Table("work")]
public class Work : BaseEntity
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public EWorkType? Type { get; set; }
    public int? TechMapId { get; set; }
    [ForeignKey("TechMapId")] public virtual TechMap? TechMap { get; set; }
    public DateTime? RegisterDate { get; set; }
    public DateTime? PlanStartDate { get; set; }
    public DateTime? PlanEndDate { get; set; }
    public DateTime? RealStartDate { get; set; }
    public DateTime? RealEndDate { get; set; }
    public int? AssetId { get; set; }
    [ForeignKey("AssetId")] public virtual Catalog.Asset? Asset { get; set; }
}