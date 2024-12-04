
using System.Linq.Expressions;

namespace Championship.Data.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        Task<bool> AnyAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        void Remove(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> conditions);
    }
}