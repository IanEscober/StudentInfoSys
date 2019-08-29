namespace StudentInfoSys.Infrastructure.Tests.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Specifications;
    using StudentInfoSys.Infrastructure.Repositories;
    using Xunit;

    public class StudentRepositoryTests
    {
        private readonly IQueryable<Student> students;
        private readonly Mock<DbSet<Student>> mockSet;

        public StudentRepositoryTests()
        {
            this.students = new List<Student>
            {
                new Student {  UserId = 1, Enrollments = { new Enrollment { Course = new Course { CourseId = 1 } } } },
                new Student {  UserId = 2, Enrollments = { new Enrollment { Course = new Course { CourseId = 2 } } } }
            }.AsQueryable();

            this.mockSet = new Mock<DbSet<Student>>();
            this.mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(this.students.Provider);
            this.mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(this.students.Expression);
            this.mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(this.students.ElementType);
            this.mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(this.students.GetEnumerator());
        }

        [Fact]
        public async Task GetStudentsAsync_WithoutQuery_ShouldReturnAll()
        {
            var expectedCount = this.students.Count();
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Student>())
                .Returns(this.mockSet.Object);
            var repository = new StudentRepository(mockContext.Object);

            var result = await repository.GetStudentsAsync();

            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public async Task GetStudentsAsync_WithQuery_ShouldReturnFiltered()
        {
            var expectedCount = 1;
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Student>())
                .Returns(this.mockSet.Object);
            var repository = new StudentRepository(mockContext.Object);
            var spec = new StudentFilterSpecification(1);

            var result = await repository.GetStudentsAsync(spec);

            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public async Task GetStudentByIdAsyc_ShouldReturnStudent()
        {
            var expectedStudent = this.students.Single(s => s.UserId == 1);
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Student>())
                .Returns(this.mockSet.Object);
            var repository = new StudentRepository(mockContext.Object);

            var result = await repository.GetStudentByIdAsyc(1);

            Assert.Equal(expectedStudent.Enrollments.First().CourseId, result.Enrollments.First().CourseId);
        }

        [Fact]
        public async Task AddStudentAsync_ShouldReturnStudent()
        {
            var expectedStudent = this.students.Single(s => s.UserId == 1);
            var mockContext = new Mock<StudentInfoSysDbContext>();
            mockContext.Setup(c => c.Set<Student>())
                .Returns(this.mockSet.Object);
            var repository = new StudentRepository(mockContext.Object);

            var result = await repository.AddStudentAsync(expectedStudent);

            Assert.Equal(expectedStudent.Enrollments.First().CourseId, result.Enrollments.First().CourseId);
        }
    }
}
