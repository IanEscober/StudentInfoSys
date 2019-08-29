namespace StudentInfoSys.Infrastructure.Repositories
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interface.Specification;
    using StudentInfoSys.Domain.Specifications;

    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudentInfoSysDbContext context) : base(context) { }

        public async Task<IReadOnlyCollection<Course>> GetCoursesAsync(ISpecification<Course> specification = null)
        {
            if(specification is null)
            {
                specification = new NullSpecification<Course>();
            }

            var courses = await this.GetAsync(specification);
            return courses.ToList().AsReadOnly();   
        }
    }
}
