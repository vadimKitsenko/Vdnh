using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Data.Entities;

namespace Domain.ViewModels;

public class SecondLevelAllViewModel
{
    public long? id { get; set; }

    public Header? Header { get; set; }

    public string? Background { get; set; }

    public string? ThirdLevelBackground { get; set; }

    public MapViewModel? Map { get; set; }

    public List<HorisontalForVerticalViewModel>? Sources { get; set; }

    public string? Text { get; set; }

}
