namespace StudentInfoSys.Infrastructure.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using StudentInfoSys.Domain.Interface.Specification;
    using StudentInfoSys.Domain.Interfaces.Repositories;

    public class GenericRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly StudentInfoSysDbContext context;

        public GenericRepository(StudentInfoSysDbContext context)
        {
            this.context = context;
        }

        public Task<T> GetAsync(int id)
        {
            return this.context.Set<T>().FindAsync(id);
        }

        public Task<IQueryable<T>> GetAsync(ISpecification<T> specification)
        {
            var initialIncludes = specification.Includes
                .Aggregate(this.context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            var additionalIncludes = specification.IncludesString
                .Aggregate(initialIncludes,
                    (current, include) => current.Include(include));

            return Task.Run(() => additionalIncludes.Where(specification.Filter));
        }

        public async Task<T> AddAsync(T entity)
        {
            this.context.Set<T>().Add(entity);
            await this.context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            await this.context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().Remove(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
