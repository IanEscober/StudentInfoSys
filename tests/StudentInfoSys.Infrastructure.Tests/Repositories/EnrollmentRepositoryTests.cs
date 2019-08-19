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

    public class EnrollmentRepositoryTests
    {
        private readonly IQueryable<Enrollment> enrollments;
        private readonly Mock<DbSet<Enrollment>> mockSet;

        public EnrollmentRepositoryTests()
        {
            this.enrollments = new List<Enrollment>
            {
                new Enrollment { StudentId = 1, CourseId = 1 },
                new Enrollment { StudentId = 2, CourseId = 2 }
            }.AsQueryable();

            this.mockSet = new Mock<DbSet<Enrollment>>();
            this.mockSet.As<IQueryable<Enrollment>>().Setup(m => m.Provider).Returns(this.enrollments.Provider);
            this.mockSet.As<IQueryable<Enrollment>>().Setup(m => m.Expression).Returns(this.enrollments.Expression);
            this.mockSet.As<IQueryable<Enrollment>>().Setup(m => m.ElementType).Returns(this.enrollments.ElementType);
            this.mockSet.As<IQueryable<Enrollment>>().Setup(m => m.GetEnumerator()).Returns(this.enrollments.GetEnumerator());
        }

        [Fact]
        public async Task AddEnrollmentAsync_WithoutQuery_ShouldReturnAll()
        {
            var expectedEnrollment = this.enrollments.Single(e => e.StudentId == 1);
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Enrollment>())
                .Returns(this.mockSet.Object);
            var repository = new EnrollmentRepository(mockContext.Object);

            var result = await repository.AddEnrollmentAsync(expectedEnrollment);

            Assert.Equal(expectedEnrollment, result);
        }

        [Fact]
        public async Task RemoveEnrollmentAsync_WithoutQuery_ShouldReturnAll()
        {
            var expectedEnrollment = this.enrollments.Single(e => e.StudentId == 1);
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Enrollment>())
                .Returns(this.mockSet.Object);
            var repository = new EnrollmentRepository(mockContext.Object);

            Task act = repository.RemoveEnrollmentAsync(expectedEnrollment);

            await act;
        }
    }
}
