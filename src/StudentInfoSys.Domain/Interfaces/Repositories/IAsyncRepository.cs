namespace StudentInfoSys.Domain.Interfaces.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IAsyncRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> query = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
