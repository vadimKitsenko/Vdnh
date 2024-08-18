using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Data.Entities;

[Table("di_secondLevel")]
public class SecondLevel : BaseEntity
{
    [Column("header")]
    public Header? Header { get; set; }

    [Column("background")]
    public Image? Background { get; set; }

    [Column("thirdLevelBackground")]
    public Image? ThirdLevelBackground { get; set; }

    [Column("map")]
    public Map? Map { get; set; }

    [Column("sources")]
    public List<Horisontal>? Sources { get; set; }

    [Column("text")]
    public string? Text { get; set; }

}
