using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.RequestModel;

public class MainRequestModel
{
    public ImageRequestModel? Img { get; set; }

    public string? Title { get; set; }

}
