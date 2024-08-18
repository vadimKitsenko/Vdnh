using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Domain.ViewModels;

public class TitleViewModel
{
    public string? Img { get; set; }

    public string? Name { get; set; }

    public string? TitleName { get; set; }

    public string? Number { get; set; }
}
