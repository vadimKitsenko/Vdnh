using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public interface IAppData<TEntity> : IData<TEntity>
    where TEntity : BaseEntity
{
    DbContext DbContext { get; }
}
