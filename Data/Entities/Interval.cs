using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_interval")]
public class Interval : BaseEntity
{
    [Column("start")]
    public decimal? Start { get; set; }

    [Column("end")]
    public decimal? End { get; set; }
}
