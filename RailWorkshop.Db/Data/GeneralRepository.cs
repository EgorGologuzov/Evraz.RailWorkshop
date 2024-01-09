using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RailWorkshop.Db;
using RailWorkshop.Db.Utils;

namespace RailWorkshop.Services.Contracts
{
    public class GeneralRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly PostgresContext Context;
        protected readonly ILogger Logger;

        public GeneralRepository(PostgresContext context, ILogger loger)
        {
            Context = context;
            Logger = loger;
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            TEntity result = (await dbSet.AddAsync(entity)).Entity;
            await Context.SaveChangesAsync();

            Logger.LogInformation("Created {type} creation data {data}", typeof(TEntity).Name, result.ToJson());

            return result;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            EntityEntry<TEntity> entryResult = dbSet.Update(entity);
            string originalValues = OriginalValuesToJson(entryResult.OriginalValues);
            TEntity result = entryResult.Entity;
            await Context.SaveChangesAsync();

            Logger.LogInformation(
                "Updated {type} from {oldData} to {newData}",
                typeof(TEntity).Name,
                originalValues,
                result.ToJson()
            );

            return result;
        }

        public virtual async Task<TEntity> Delete(TEntity entity)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            TEntity result = dbSet.Remove(entity).Entity;
            await Context.SaveChangesAsync();

            Logger.LogInformation(
                "Deleted {type} with data {data}",
                typeof(TEntity).Name,
                result.ToJson()
            );

            return result;
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            TEntity result = await dbSet.FindAsync(id);

            return result;
        }

        private string OriginalValuesToJson(PropertyValues originalValues)
        {
            JObject json = new();

            foreach (var prop in originalValues.Properties)
            {
                json[prop.Name.ToLower()] = JToken.FromObject(originalValues[prop]);
            }

            return json.ToString(Formatting.Indented);
        }
    }
}