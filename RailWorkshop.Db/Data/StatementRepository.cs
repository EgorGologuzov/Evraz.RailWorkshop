using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.Db.Data
{
    public class StatementRepository : GeneralRepository<Statement>, IStatementRepository
    {
        public StatementRepository(PostgresContext context, ILogger<StatementRepository> logger) : base(context, logger)
        {
        }

        public override Task<Statement> GetById(object id)
        {
            return Context.Statements
                .Include(s => s.Products)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(p => p.Profile)
                .Include(s => s.Products)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(p => p.Steel)
                .Include(s => s.Products)
                    .ThenInclude(c => c.Defects)
                        .ThenInclude(cd => cd.Defect)
                .Include(s => s.Segment)
                .Include(s => s.Responsible)
                    .ThenInclude(e => e.Segment)
                .Where(p => p.Id == (Guid)id)
                .FirstOrDefaultAsync();
        }
    }
}