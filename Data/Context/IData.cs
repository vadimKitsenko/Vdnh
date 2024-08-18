using Core.Models.Entities;

namespace Data;

public interface IData<TEntity> : Core.Database.IData<TEntity>
    where TEntity : BaseEntity { }
