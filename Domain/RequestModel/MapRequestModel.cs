using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.RequestModel;

public class MapRequestModel
{
    public ImageRequestModel? Background { get; set; }

    public string? CheckForMap { get; set; }

}
