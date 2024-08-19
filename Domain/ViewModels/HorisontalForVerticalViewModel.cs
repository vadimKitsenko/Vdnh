using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Data.Entities;
using Core.Models.Entities;

namespace Domain.ViewModels
{
    public class HorisontalForVerticalViewModel
    {
        public long Id { get; set; }

        public long? SecondLevelId { get; set; }

        public double? X { get; set; }

        public double? Y { get; set; }

        public string? Img { get; set; }

        public AboutForVerticalViewModel? About { get; set; }

        public string? Text { get; set; }

        public string? Name { get; set; }

        public int? Index { get; set; }

    }
}
