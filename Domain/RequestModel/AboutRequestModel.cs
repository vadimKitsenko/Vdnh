using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.RequestModel;

public class AboutRequestModel
{
    public TitleRequestModel? Title { get; set; }

    public string? Text { get; set; }

    public int? Number { get; set; }

    public List<ImageRequestModel>? Images { get; set; }

    public ImageRequestModel? Background { get; set; }

    public MainRequestModel? Main { get; set; }
}
