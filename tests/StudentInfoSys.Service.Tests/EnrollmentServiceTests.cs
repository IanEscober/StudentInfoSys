﻿namespace StudentInfoSys.Service.Tests
{
    using System.Threading.Tasks;
    using Moq;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Logging;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using Xunit;

    public class EnrollmentServiceTests
    {
        [Fact]
        public async Task AddCourseToStudentAsync_ShouldReturnEnrollment()
        {
            int studentId = 1;
            int courseId = 1;
            var mockLogger = new Mock<IBaseLogger<EnrollmentService>>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            mockRepository.Setup(repo => repo.AddEnrollmentAsync(It.IsAny<Enrollment>()))
                .ReturnsAsync(new Enrollment { StudentId = studentId, CourseId = courseId })
                .Verifiable();
            var service = new EnrollmentService(mockRepository.Object, mockLogger.Object);

            var result = await service.AddCourseToStudentAsync(studentId, courseId);

            Assert.Equal(studentId, result.StudentId);
            Assert.Equal(courseId, result.CourseId);
            mockRepository.Verify();
        }

        [Fact]
        public async Task RemoveCourseFromStudentAsync_ShouldReturn()
        {
            int studentId = 1;
            int courseId = 1;
            var mockLogger = new Mock<IBaseLogger<EnrollmentService>>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            mockRepository.Setup(repo => repo.RemoveEnrollmentAsync(It.IsAny<Enrollment>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var service = new EnrollmentService(mockRepository.Object, mockLogger.Object);

            async Task act() => await service.RemoveCourseFromStudentAsync(studentId, courseId);

            await act();
            mockRepository.Verify();
        }
    }
}
