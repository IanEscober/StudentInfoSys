namespace StudentInfoSys.Application.Tests.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using StudentInfoSys.Application.Controllers;
    using StudentInfoSys.Application.Models.ViewModels;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Services;
    using Xunit;

    public class EnrollmentsControllerTests
    {
        [Fact]
        public async Task PostByIdAndCourse_WithValidModel_ShouldReturnOk()
        {
            var mockService = new Mock<IEnrollmentService>();
            mockService.Setup(service => service.AddCourseToStudentAsync(0, 0))
                .ReturnsAsync(new Enrollment())
                .Verifiable();
            var controller = new EnrollmentsController(mockService.Object);

            var response = await controller.Post(0, new CourseViewModel());

            Assert.IsType<OkObjectResult>(response);
            mockService.Verify();
        }

        [Fact]
        public async Task PostByIdAndCourse_WithInvalidModel_ShouldReturnBadResponse()
        {
            var mockService = new Mock<IEnrollmentService>();
            mockService.Setup(service => service.AddCourseToStudentAsync(0, 0))
                .ReturnsAsync(null as Enrollment)
                .Verifiable();
            var controller = new EnrollmentsController(mockService.Object);

            var response = await controller.Post(0, new CourseViewModel());

            Assert.IsType<BadRequestObjectResult>(response);
            mockService.Verify();
        }

        [Fact]
        public async Task DeleteByIdAndCourse_WithValidModel_ShouldReturnOk()
        {
            var mockService = new Mock<IEnrollmentService>();
            mockService.Setup(service => service.RemoveCourseFromStudentAsync(0, 0))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new EnrollmentsController(mockService.Object);

            var response = await controller.Delete(0, new CourseViewModel());

            Assert.IsType<OkObjectResult>(response);
            mockService.Verify();
        }
    }
}
