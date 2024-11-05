using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;

namespace Assets.Maintenance;

[Table("failure_category")]
public class FailureCategory : BaseEntity
{
    public string? Name { get; set; }
}