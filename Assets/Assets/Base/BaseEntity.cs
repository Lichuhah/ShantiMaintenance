using System.ComponentModel.DataAnnotations.Schema;

namespace Assets.Base;

public class BaseEntity
{
    [Column("id")]
    public int Id { get; set; }
}