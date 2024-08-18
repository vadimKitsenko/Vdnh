using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Base;
using Data.Entities;
using Domain.ViewModels;

namespace Services.Admin.Vertical
{
    public interface IVertical
    {
        Task<VerticalViewModel> GetVertical(long? id, long? periodId);

        Task<List<VerticalListModel>> GetVerticalList();

        Task<BaseModel> UpdateVertical(Domain.RequestModel.VerticalRequestModel? updateModel);

        Task<BaseModel> InsertVertical(Domain.RequestModel.VerticalRequestModel? insertModel);

        Task<BaseModel> DeleteVertical(long? verticalId);
    }
}
