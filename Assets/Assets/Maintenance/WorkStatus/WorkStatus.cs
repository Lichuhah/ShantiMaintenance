using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;
using Assets.Maintenance.Enums;

namespace Assets.Maintenance;

[Table("work_status")]
public class WorkStatus : BaseEntity
{
    public int? WorkId { get; set; }
    [ForeignKey("WorkId")] public virtual Work? Work { get; set; }
    public EWorkStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}