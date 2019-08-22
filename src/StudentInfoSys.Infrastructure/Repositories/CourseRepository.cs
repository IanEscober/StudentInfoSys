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
            if(query != null)
            {
                var filteredCourses = await this.GetAsync(query);
                return filteredCourses.ToList().AsReadOnly();
            }

            var courses = await this.GetAsync();
            return courses.ToList().AsReadOnly();   
        }
    }
}
