using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_about")]
public class About : BaseEntity
{
    [Column("main")]
    public Title? Title { get; set; }

    [Column("text")]
    public string? Text { get; set; }

    [Column("number")]
    public int? Number { get; set; }

    [Column("images")]
    public List<Image>? Images { get; set; }

    [Column("background")]
    public Image? Background { get; set; }

    [Column("main")]
    public Main? Main { get; set; }
}
