using RailWorkshop.Services.Entity;

namespace RailWorkshop.Services.Contracts
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetByLoginAndPassword(string name, string password);
        Task<Employee> UpdatePassword(Guid id, string oldPassword, string newPassword);
        Task<Employee> ResetPassword(Guid id, string newPassword);
    }
}