using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Core.Models.File;

namespace Domain.ViewModels;

public class AboutForVerticalViewModel
{

    public string? Text { get; set; }

    public int? Number { get; set; }

    public List<string>? Images { get; set; }

    public MainViewModel? Main { get; set; }
}
