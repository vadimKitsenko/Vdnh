using Core.Models.Entities;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data;

public class DataManager<TEntity> : EfData<TEntity>, IAppData<TEntity>
    where TEntity : BaseEntity
{
    public DataManager(
        VdnhContext context,
        ILogger<EfData<TEntity>> logger,
        IHttpContextAccessor httpContextAccessor
    )
        : base(context, logger, httpContextAccessor) { }

    public DbContext DbContext => Context;
}
