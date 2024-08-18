using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.RequestModel;

public class SecondLevelRequestModel
{
    public Header? Header { get; set; }

    public ImageRequestModel? Background { get; set; }

    public ImageRequestModel? ThirdLevelBackground { get; set; }

    public MapRequestModel? Map { get; set; }

    public List<HorisontalRequestModel>? Sources { get; set; }

    public string? Text { get; set; }

}
