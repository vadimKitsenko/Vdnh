using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_map")]
public class Map : BaseEntity
{
    [Column("background")]
    public Image? Background { get; set; }

}
