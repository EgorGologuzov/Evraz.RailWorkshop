using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.Db.Data
{
    public class ProductRepository : GeneralRepository<Product>, IProductRepository
    {
        public ProductRepository(PostgresContext context, ILogger<ProductRepository> logger) : base(context, logger)
        {
        }

        public override Task<Product> GetById(object id)
        {
            return Context.Products
                .Include(p => p.Profile)
                .Include(p => p.Steel)
                .Where(p => p.Id == (Guid)id)
                .FirstOrDefaultAsync();
        }
    }
}