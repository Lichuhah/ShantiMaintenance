using System.ComponentModel.DataAnnotations.Schema;
using Assets.Base;

namespace Assets.Maintenance;

[Table("defect_category")]
public class DefectCategory : BaseEntity
{
    public string? Name { get; set; }
}