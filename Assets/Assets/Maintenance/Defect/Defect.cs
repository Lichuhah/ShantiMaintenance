using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;
using Assets.Catalog;

namespace Assets.Maintenance;

[Table("defect")]
public class Defect : BaseEntity
{
    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")] public virtual DefectCategory? Category { get; set; }
    public int? FailureId { get; set; }
    [ForeignKey("FailureId")] public virtual Failure? Failure { get; set; }
    public DateTime? PlanFixDate { get; set; }
    public DateTime? RegisterDate { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? AssetId { get; set; }
    [ForeignKey("AssetId")] public virtual Catalog.Asset? Asset { get; set; }
    public int? WorkId { get; set; }
    [ForeignKey("WorkId")] public virtual Work? Work { get; set; }
}