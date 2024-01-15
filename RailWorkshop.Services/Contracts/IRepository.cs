using RailWorkshop.Services.Entity;

namespace RailWorkshop.Services.Contracts
{
    public interface IRepository<T>
    {
        Task<T> Create(T entity);
        Task<T> Update(object id, T entity);
        Task<T> Delete(object id);
        Task<T> GetById(object id);
        Task<bool> IsExists(object id);
    }
}