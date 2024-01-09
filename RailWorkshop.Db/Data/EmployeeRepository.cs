using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;
using RailWorkshop.Services.Utils;

namespace RailWorkshop.Db.Data
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PostgresContext context, ILogger<EmployeeRepository> logger) : base(context, logger)
        {
        }

        public async Task<Employee> GetByLoginAndPassword(string name, string password)
        {
            string passwordHash = password.ToSha256Hash();

            IList<Employee> list = await Context.Employees
                .Include(e => e.Segment)
                .Where(e => e.Name == name && e.Password == passwordHash)
                .ToListAsync();

            if (list.Count == 0)
            {
                throw new IncorrectLoginOrPasswordException();
            }

            return list.FirstOrDefault();
        }

        public async Task<Employee> UpdatePassword(Guid id, string oldPassword, string newPassword)
        {
            string passwordHash = oldPassword.ToSha256Hash();

            IList<Employee> list = await Context.Employees
                .Where(e => e.Id == id && e.Password == passwordHash)
                .ToListAsync();

            if (list.Count == 0)
            {
                throw new EntityNotFoundException();
            }

            Employee employee = list.FirstOrDefault();
            employee.Password = newPassword;
            await Context.SaveChangesAsync();

            Logger.LogInformation("Password updated for employee {employee}", employee.Id);

            return employee;
        }
    }
}