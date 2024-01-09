using RailWorkshop.Services.Interfaces;

namespace RailWorkshop.Services.Contracts
{
    public interface IHandbookRepository
    {
        Task<T> Create<T>(T entity) where T : class, IHandbookEntity;
        Task<T> Update<T>(T entity) where T : class, IHandbookEntity;
        Task<T> Delete<T>(T entity) where T : class, IHandbookEntity;
        Task<T> GetById<T>(int id) where T : class, IHandbookEntity;
        Task<IReadOnlyCollection<T>> GetAll<T>() where T : class, IHandbookEntity;
    }
}