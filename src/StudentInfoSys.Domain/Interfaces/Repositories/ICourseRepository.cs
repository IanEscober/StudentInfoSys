namespace StudentInfoSys.Domain.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;

    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<IReadOnlyCollection<Course>> GetCoursesAsync(Expression<Func<Course, bool>> query = null);
    }
}
