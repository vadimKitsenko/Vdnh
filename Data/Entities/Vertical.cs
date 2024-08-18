using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_vertical")]
public class Vertical : BaseEntity
{
    [Column("interval")]
    public Interval? Interval { get; set; }

    [Column("header")]
    public Header? Header { get; set; }

    [Column("map")]
    public Map? Map { get; set; }

    [Column("text")]
    public string? Text { get; set; }

    [Column("secondLevel")]
    public SecondLevel? SecondLevel { get; set; }

    [Column("groupId")]
    public long? GroupId { get; set; }

    [Column("periodId")]
    public long? PeriodId { get; set; }
}

