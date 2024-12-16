using System.Linq.Expressions;

namespace Championchip.Core.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> conditions);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> conditions);
        Task<T?> GetAsync(int id);
        Task<T?> GetAsync(Expression<Func<T, bool>> conditions);
        void Remove(T entity);
        void Update(T entity);
    }
}