using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;
using Core.Models.Entities;
using Data.Entities;

namespace Domain.RequestModel;

public class VerticalRequestModel
{
    public long Id { get; set; }

    public Interval? Interval { get; set; }

    public Header? Header { get; set; }

    public MapRequestModel? Map { get; set; }

    public string? Text { get; set; }

    public SecondLevelRequestModel? SecondLevel { get; set; }

}
