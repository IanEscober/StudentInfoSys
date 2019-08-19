namespace StudentInfoSys.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using StudentInfoSys.Domain.Entities;

    public class StudentInfoSysDbContextSeed
    {
        public static async Task Seed(StudentInfoSysDbContext studentInfoSysDbContext)
        {
            studentInfoSysDbContext.Database.Migrate();

            if (!studentInfoSysDbContext.Courses.Any())
            {
                studentInfoSysDbContext.Courses.AddRange(CourseSeeds());

                await studentInfoSysDbContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Course> CourseSeeds()
        {
            return new List<Course>()
            {
                new Course { Name = "Differential Equations" },
                new Course { Name = "Thesis" },
                new Course { Name = "Drop" }
            };
        }
    }
}
