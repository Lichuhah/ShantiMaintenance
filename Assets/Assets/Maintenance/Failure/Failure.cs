using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;

namespace Assets.Maintenance;

[Table("failure")]
public class Failure : BaseEntity
{
    public DateTime? RegisterDate { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")] public virtual FailureCategory? Category { get; set; }
}