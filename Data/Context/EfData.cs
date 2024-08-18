using System.Linq.Expressions;
using Core.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data;

public abstract class EfData<TEntity> : IData<TEntity>
    where TEntity : BaseEntity
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly DbContext Context;
    protected readonly ILogger<EfData<TEntity>> Logger;

    private DbSet<TEntity> _entities;

    protected EfData(
        DbContext context,
        ILogger<EfData<TEntity>> logger,
        IHttpContextAccessor httpContextAccessor
    )
    {
        Context = context;
        Logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    ///     Gets an entity set
    /// </summary>
    protected virtual DbSet<TEntity> Entities => _entities ??= Context.Set<TEntity>();

    /// <summary>
    ///     Get entity by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Entity</returns>
    public virtual async ValueTask<TEntity> GetById(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        return await Entities.FindAsync(new[] { id }, cancellationToken);
    }

    /// <summary>
    ///     Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task Insert(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        try
        {
            EnsureInsertBaseProperties(entity);
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            throw new Exception(
                await GetFullErrorTextAndRollbackEntityChanges(exception),
                exception
            );
        }
    }

    /// <summary>
    ///     Insert entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual async Task Insert(IEnumerable<TEntity> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        try
        {
            var list = entities.ToList();
            list.ForEach(EnsureInsertBaseProperties);

            await Entities.AddRangeAsync(list);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            throw new Exception(
                await GetFullErrorTextAndRollbackEntityChanges(exception),
                exception
            );
        }
    }

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        try
        {
            EnsureUpdateBaseProperties(entity);
            Entities.Update(entity);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            throw new Exception(
                await GetFullErrorTextAndRollbackEntityChanges(exception),
                exception
            );
        }
    }

    public virtual async Task SimpleUpdate(IEnumerable<TEntity> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        try
        {
            var list = entities.ToList();
            Entities.UpdateRange(list);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            throw new Exception(
                await GetFullErrorTextAndRollbackEntityChanges(exception),
                exception
            );
        }
    }

    /// <summary>
    ///     Update entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual async Task Update(IEnumerable<TEntity> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        try
        {
            var list = entities.ToList();
            list.ForEach(EnsureUpdateBaseProperties);
            Entities.UpdateRange(list);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            throw new Exception(
                await GetFullErrorTextAndRollbackEntityChanges(exception),
                exception
            );
        }
    }

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task Delete(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        try
        {
            Entities.Remove(entity);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            throw new Exception(
                await GetFullErrorTextAndRollbackEntityChanges(exception),
                exception
            );
        }
    }

    /// <summary>
    ///     Delete entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual async Task Delete(IEnumerable<TEntity> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        try
        {
            Entities.RemoveRange(entities);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            throw new Exception(
                await GetFullErrorTextAndRollbackEntityChanges(exception),
                exception
            );
        }
    }

    /// <summary>
    ///     Gets a table
    /// </summary>
    public virtual IQueryable<TEntity> Table => Entities;

    /// <summary>
    ///     Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
    ///     operations
    /// </summary>
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public virtual async Task LoadReference<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, TProperty>> propertyExpression,
        CancellationToken cancellationToken = default
    )
        where TProperty : class
    {
        await Context.Entry(entity).Reference(propertyExpression).LoadAsync(cancellationToken);
    }

    public void SetTimeout(int timeout)
    {
        Context.Database.SetCommandTimeout(timeout);
    }

    public IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
    {
        return Entities.FromSqlRaw(sql, parameters);
    }

    /// <summary>
    ///     Rollback of entity changes and return full error message
    /// </summary>
    /// <param name="exception">Exception</param>
    /// <returns>Error message</returns>
    protected async Task<string> GetFullErrorTextAndRollbackEntityChanges(
        DbUpdateException exception
    )
    {
        //rollback entity changes
        if (Context is { } dbContext)
        {
            var entries = dbContext
                .ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            entries.ForEach(entry =>
            {
                try
                {
                    entry.State = EntityState.Unchanged;
                }
                catch (InvalidOperationException)
                {
                    // ignored
                }
            });
        }

        try
        {
            Logger.LogError(exception, $"{nameof(EfData<TEntity>)} error {exception.Message}");

            if (Context != null)
                await Context.SaveChangesAsync();

            return exception.ToString();
        }
        catch (Exception ex)
        {
            Logger.LogError(exception, $"{nameof(EfData<TEntity>)} error  {exception.Message}");

            return ex.ToString();
        }
    }

    protected virtual void EnsureInsertBaseProperties(TEntity entity)
    {
        try
        {
            var currentUserName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            var dateCreateInfo = entity.GetType().GetProperty("DateCreate");
            var lastDateInfo = entity.GetType().GetProperty("DateUpdate");
            var userUpdateInfo = entity.GetType().GetProperty("UserUpdate");
            var userCreateInfo = entity.GetType().GetProperty("UserCreate");

            var lastDate = lastDateInfo?.GetValue(entity) as DateTime?;
            var dateCreate = dateCreateInfo?.GetValue(entity) as DateTime?;
            var userUpdate = userUpdateInfo?.GetValue(entity) as string;
            var userCreate = userCreateInfo?.GetValue(entity) as string;

            if (userUpdate == null && userUpdateInfo != null)
                userUpdateInfo.SetValue(entity, currentUserName ?? "admin");

            if (userCreate == null && userCreateInfo != null)
                userCreateInfo.SetValue(entity, currentUserName ?? "admin");

            if (dateCreateInfo != null)
                if (dateCreate == DateTime.MinValue || dateCreate == null)
                    dateCreateInfo.SetValue(entity, DateTime.UtcNow);

            if (lastDateInfo != null)
                lastDateInfo.SetValue(entity, DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(EfData<TEntity>)} error  {ex.Message}");
        }
    }

    protected virtual void EnsureUpdateBaseProperties(TEntity entity)
    {
        try
        {
            var currentUserName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(EfData<TEntity>)} error  {ex.Message}");
        }
    }
}
