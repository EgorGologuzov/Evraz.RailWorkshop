using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Interfaces;

namespace RailWorkshop.Db.Data
{
    /// <summary>
    /// Общий репозиторий для всех справочников. Сущности справочников должны реализовывать IHandbookEntity.
    /// </summary>
    public class HandbookRepository : IHandbookRepository
    {
        private readonly PostgresContext _context;
        private readonly ILogger<HandbookRepository> _logger;

        public HandbookRepository(PostgresContext context, ILogger<HandbookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<TEntity> Create<TEntity>(TEntity entity) where TEntity : class, IHandbookEntity
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();

            if (await dbSet.AnyAsync(e => e.Id == entity.Id))
            {
                throw new RepeatingIdException();
            }

            TEntity result = (await dbSet.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Created new {type} with data {data}",
                typeof(TEntity).Name,
                result.ToJson()
            );

            return result;
        }

        public virtual async Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IHandbookEntity
        {
            TEntity oldEntity = await GetById<TEntity>(entity.Id);

            if (oldEntity is null)
            {
                throw new EntityNotFoundException();
            }

            string oldData = oldEntity.ToJson();

            DbSet<TEntity> dbSet = _context.Set<TEntity>();
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            TEntity result = dbSet.Update(oldEntity).Entity;
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Updated {type} from {oldData} to {newData}",
                typeof(TEntity).Name,
                oldData,
                result.ToJson()
            );

            return result;
        }

        public virtual async Task<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IHandbookEntity
        {
            TEntity oldEntity = await GetById<TEntity>(entity.Id);

            if (oldEntity is null)
            {
                throw new EntityNotFoundException();
            }

            DbSet<TEntity> dbSet = _context.Set<TEntity>();
            EntityEntry<TEntity> result = dbSet.Remove(oldEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
               "Deleted {type} with data {data}",
               typeof(TEntity).Name,
               oldEntity.ToJson()
           );

            return result.Entity;
        }

        public virtual async Task<TEntity> GetById<TEntity>(int id) where TEntity : class, IHandbookEntity
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();
            TEntity result = await dbSet.FindAsync(id);

            return result;
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAll<TEntity>() where TEntity : class, IHandbookEntity
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();
            IList<TEntity> list = await dbSet.ToListAsync();
            ReadOnlyCollection<TEntity> result = new(list ?? new List<TEntity>());

            return result;
        }
    }
}