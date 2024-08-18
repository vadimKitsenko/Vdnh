using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Base;
using Data.Entities;
using Domain.ViewModels;

namespace Services.Admin.Horisontal
{
    public interface IHorisontal
    {
        Task<HorisontalViewModel> GetHorisontal(long? id);

        Task<List<HorisontalListModel>> GetHorisontalList();

        Task<BaseModel> UpdateHorisontal(Domain.RequestModel.HorisontalRequestModel? updateModel, long? secondLevelId = null);

        Task<BaseModel> InsertHorisontal(Domain.RequestModel.HorisontalRequestModel? insertModel, long? secondLevelId = null);

        Task<BaseModel> DeleteHorisontal(long? horisontalId);
    }
}
