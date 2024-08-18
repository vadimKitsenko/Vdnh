using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;

namespace Domain.ViewModels;

public class MainViewModel
{
    public string? Img { get; set; }

    public string? Title { get; set; }

}
