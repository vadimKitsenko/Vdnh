using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.RequestModel;

public class TitleRequestModel
{
    public ImageRequestModel? Img { get; set; }

    public string? Name { get; set; }

    public string? TitleName { get; set; }

    public string? Number { get; set; }
}
