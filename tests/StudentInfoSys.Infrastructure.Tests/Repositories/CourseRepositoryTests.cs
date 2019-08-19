namespace StudentInfoSys.Infrastructure.Tests.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Infrastructure.Repositories;
    using Xunit;

    public class CourseRepositoryTests
    {
        private readonly IQueryable<Course> courses;
        private readonly Mock<DbSet<Course>> mockSet;

        public CourseRepositoryTests()
        {
            this.courses = new List<Course>
            {
                new Course { CourseId =  1},
                new Course { CourseId =  2},
                new Course { CourseId =  3}
            }.AsQueryable();

            this.mockSet = new Mock<DbSet<Course>>();
            this.mockSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(this.courses.Provider);
            this.mockSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(this.courses.Expression);
            this.mockSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(this.courses.ElementType);
            this.mockSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(this.courses.GetEnumerator());
        }

        [Fact]
        public async Task GetCoursesAsync_WithoutQuery_ShouldReturnAll()
        {
            var expectedCount = this.courses.Count();
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Course>())
                .Returns(this.mockSet.Object);
            var repository = new CourseRepository(mockContext.Object);

            var result = await repository.GetCoursesAsync();

            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public async Task GetCoursesAsync_WithQuery_ShouldReturnFiltered()
        {
            var expectedCount = this.courses.Count(c => c.CourseId > 1);
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Course>())
                .Returns(this.mockSet.Object);
            var repository = new CourseRepository(mockContext.Object);

            var result = await repository.GetCoursesAsync(c => c.CourseId > 1);

            Assert.Equal(expectedCount, result.Count);
        }
    }
}
