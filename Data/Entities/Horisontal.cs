using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_horisontal")]
public class Horisontal : BaseEntity
{
    [Column("x")]
    public double? X { get; set; }

    [Column("y")]
    public double? Y { get; set; }

    [Column("img")]
    public Image? Img { get; set; }

    [Column("about")]
    public About? About { get; set; }

    [Column("text")]
    public string? Text { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("SecondLevelId")]
    public long? SecondLevelId { get; set; }

    [Column("index")]
    public int? Index { get; set; }
}
