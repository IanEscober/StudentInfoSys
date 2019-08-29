namespace StudentInfoSys.Domain.Interfaces.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interface.Specification;

    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<IReadOnlyCollection<Course>> GetCoursesAsync(ISpecification<Course> specification = null);
    }
}
