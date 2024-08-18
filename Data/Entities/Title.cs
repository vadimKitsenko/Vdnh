using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_title")]
public class Title : BaseEntity
{
    [Column("img")]
    public Image? Img { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("titleName")]
    public string? TitleName { get; set; }

    [Column("number")]
    public string? Number { get; set; }
}
