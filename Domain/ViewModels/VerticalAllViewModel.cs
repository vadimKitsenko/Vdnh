using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;
using Core.Models.Entities;
using Data.Entities;

namespace Domain.ViewModels;

public class VerticalAllViewModel
{
    public long Id { get; set; }

    public long GroupId { get; set; }

    public long PeriodId { get; set; }

    public Interval? Interval { get; set; }

    public Header? Header { get; set; }

    public MapViewModel? Map { get; set; }

    public string? Text { get; set; }

    public SecondLevelAllViewModel? SecondLevel { get; set; }
}
