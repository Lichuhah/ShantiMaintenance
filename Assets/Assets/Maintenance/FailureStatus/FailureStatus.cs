using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;
using Assets.Maintenance.Enums;

namespace Assets.Maintenance;

[Table("failure_status")]
public class FailureStatus : BaseEntity
{
    public int? FailureId { get; set; }
    [ForeignKey("FailureId")] public virtual Failure? Failure { get; set; }
    public EDefectStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}