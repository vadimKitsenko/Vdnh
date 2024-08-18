using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_image")]
public class Image : BaseEntity
{
    
    [Column("main")]
    public string? Main { get; set; } = null;

    [Column("preview")]
    public string? Preview { get; set; } = null;

    [Column("priority")]
    public int? Priority { get; set; } = null;

    [Column("link")]
    public string? Link { get; set; } = null;


}
