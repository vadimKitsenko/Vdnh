using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_main")]
public class Main : BaseEntity
{
    [Column("img")]
    public Image? Img { get; set; }

    [Column("title")]
    public string? Title { get; set; }

}
