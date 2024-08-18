using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_header")]
public class Header : BaseEntity
{
    [Column("title")]
    public string? Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }
}
