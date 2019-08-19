namespace StudentInfoSys.Infrastructure.Repositories
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using System.Linq.Expressions;
    using System;

    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudentInfoSysDbContext context) : base(context) { }

        public async Task<IReadOnlyCollection<Course>> GetCoursesAsync(Expression<Func<Course,bool>> query = null)
        {
            var courses = await this.GetAsync(query);
            return courses.ToList().AsReadOnly();    
        }
    }
}
