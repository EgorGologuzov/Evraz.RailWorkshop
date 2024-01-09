namespace RailWorkshop.Services.Contracts
{
    public interface IRepository<T>
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<T> GetById(object id);
    }
}