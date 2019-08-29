namespace StudentInfoSys.Domain.Interfaces.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Interface.Specification;

    public interface IAsyncRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<IQueryable<T>> GetAsync(ISpecification<T> specification);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
