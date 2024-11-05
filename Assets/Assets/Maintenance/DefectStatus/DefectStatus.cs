using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;

namespace Assets.Maintenance;

[Table("defect_status")]
public class DefectStatus : BaseEntity
{
    public int? DefectId { get; set; }
    [ForeignKey("DefectId")] public virtual Defect? Defect { get; set; }
    public Enums.EDefectStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}