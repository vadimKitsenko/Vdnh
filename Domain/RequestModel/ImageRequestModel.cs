using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.RequestModel;

public class ImageRequestModel
{
    public string? Link { get; set; }
    public IFormFileCollection? FromDataFile { get; set; }
    public IFormFileCollection? FromDataFilePreview { get; set; }
    public int? Priority { get; set; }
    public long? Id { get; set; }
}
