namespace StudentInfoSys.Service.Tests
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Moq;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using Xunit;

    public class StudentServiceTests
    {
        [Fact]
        public async Task AddStudentAsync_WithValidStudent_ShouldReturnStudent()
        {
            var expectedUser = new User { UserId = 1 };
            var expectedStudent = new Student { User = expectedUser };
            var mockRepository = new Mock<IStudentRepository>();
            mockRepository.Setup(repo => repo.AddStudentAsync(It.IsAny<Student>()))
                .ReturnsAsync(expectedStudent)
                .Verifiable();
            var service = new StudentService(mockRepository.Object);

            var result = await service.AddStudentAsync(expectedStudent);

            Assert.Equal(expectedStudent.User.UserId, result.User.UserId);
            mockRepository.Verify();
        }

        [Fact]
        public async Task AuthenticateBasicAsync_WithValidCredentials_ShouldReturnStudent()
        {
            var header = "Basic dGVzdEB5cy5jb206dGVzdDEyMw==";
            var expectedStudent = new Student { StudentId = 1 };
            var mockRepository = new Mock<IStudentRepository>();
            mockRepository.Setup(repo => repo.GetStudentsAsync(It.IsAny<Expression<Func<Student, bool>>>()))
                .ReturnsAsync(new Student[] { expectedStudent })
                .Verifiable();
            var service = new StudentService(mockRepository.Object);

            var result = await service.AuthenticateBasicAsync(header);

            Assert.Equal(expectedStudent.StudentId, result.StudentId);
            mockRepository.Verify();
        }

        [Fact]
        public void GenerateToken_WithStudentAndKey_ShouldReturnToken()
        {
            var key = "Test_JWT_Key_For_Testing";
            var mockRepository = new Mock<IStudentRepository>();
            var service = new StudentService(mockRepository.Object);

            var result = service.GenerateToken(new Student { UserId = 1 }, key);

            Assert.NotNull(result);
            mockRepository.Verify();
        }
    }
}
