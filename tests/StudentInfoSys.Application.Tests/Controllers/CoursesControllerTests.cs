namespace StudentInfoSys.Application.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using StudentInfoSys.Application.Controllers;
    using StudentInfoSys.Application.Models.Dtos;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using Xunit;

    public class CoursesControllerTests
    {
        [Fact]
        public async Task Get_WithCourses_ShouldReturnOk()
        {
            var expectedCount = 2;
            var mockRepo = new Mock<ICourseRepository>();
            mockRepo.Setup(repo => repo.GetCoursesAsync(null))
                .ReturnsAsync(new Course[expectedCount])
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IReadOnlyCollection<CourseDto>>(It.IsAny<Course[]>()))
                .Returns(new CourseDto[expectedCount])
                .Verifiable();
            var controller = new CoursesController(mockRepo.Object, mockMapper.Object);

            var response = await controller.Get();

            var responseResult = Assert.IsType<OkObjectResult>(response.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<CourseDto>>(responseResult.Value);
            Assert.Equal(expectedCount, model.Count());
            mockRepo.Verify();
            mockMapper.Verify();
        }

        [Fact]
        public async Task Get_WithoutCourses_ShouldReturnNoContent()
        {
            var expectedCount = 0;
            var mockRepo = new Mock<ICourseRepository>();
            mockRepo.Setup(repo => repo.GetCoursesAsync(null))
                .ReturnsAsync(new Course[expectedCount])
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            var controller = new CoursesController(mockRepo.Object, mockMapper.Object);

            var response = await controller.Get();

            Assert.IsType<NoContentResult>(response.Result);
            mockRepo.Verify();
        }
    }
}
