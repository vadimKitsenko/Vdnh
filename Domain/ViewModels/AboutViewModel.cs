using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Core.Models.File;

namespace Domain.ViewModels;

public class AboutViewModel
{
    public TitleViewModel? Title { get; set; }

    public string? Text { get; set; }
    public int? Number { get; set; }

    public List<ImageLink>? Images { get; set; }

    public string? Background { get; set; }

    public MainViewModel? Main { get; set; }
}
