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
    using StudentInfoSys.Application.Models.ViewModels;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interface.Specification;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;
    using Xunit;

    public class StudentsControllerTests
    {
        [Fact]
        public async Task Get_WithStudents_ShouldReturnOk()
        {
            var expectedCount = 2;
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(repo => repo.GetStudentsAsync(It.IsAny<ISpecification<Student>>()))
                .ReturnsAsync(new Student[expectedCount])
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IReadOnlyCollection<UserDto>>(It.IsAny<Student[]>()))
                .Returns(new UserDto[expectedCount])
                .Verifiable();
            var controller = new StudentsController(mockRepo.Object, null, mockMapper.Object);

            var response = await controller.Get();

            var responseResult = Assert.IsType<OkObjectResult>(response.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserDto>>(responseResult.Value);
            Assert.Equal(expectedCount, model.Count());
            mockRepo.Verify();
            mockMapper.Verify();
        }

        [Fact]
        public async Task Get_WithoutStudents_ShouldReturnNoContent()
        {
            var expectedCount = 0;
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(repo => repo.GetStudentsAsync(It.IsAny<ISpecification<Student>>()))
                .ReturnsAsync(new Student[expectedCount])
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            var controller = new StudentsController(mockRepo.Object, null, mockMapper.Object);

            var response = await controller.Get();

            Assert.IsType<NoContentResult>(response.Result);
            mockRepo.Verify();
        }

        [Fact]
        public async Task GetById_WithStudent_ShouldReturnOk()
        {
            var expectedId = 1;
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(repo => repo.GetStudentByIdAsyc(expectedId))
                .ReturnsAsync(new Student { UserId = expectedId })
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<StudentDto>(It.IsAny<Student>()))
                .Returns(new StudentDto() { Id = expectedId })
                .Verifiable();
            var controller = new StudentsController(mockRepo.Object, null, mockMapper.Object);

            var response = await controller.Get(expectedId);

            var responseResult = Assert.IsType<OkObjectResult>(response.Result);
            var model = Assert.IsAssignableFrom<StudentDto>(responseResult.Value);
            Assert.Equal(expectedId, model.Id);
            mockRepo.Verify();
            mockMapper.Verify();
        }

        [Fact]
        public async Task GetById_WithoutStudent_ShouldReturnNotFound()
        {
            var expectedId = 1;
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(repo => repo.GetStudentByIdAsyc(expectedId))
                .ReturnsAsync(null as Student)
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            var controller = new StudentsController(mockRepo.Object, null, mockMapper.Object);

            var response = await controller.Get(expectedId);

            Assert.IsType<NotFoundResult>(response.Result);
            mockRepo.Verify();
        }

        [Fact]
        public async Task PostByUser_WithValidModel_ShouldReturnOk()
        {
            var expectedEmail = "test@ys.com";
            var mockService = new Mock<IStudentService>();
            mockService.Setup(service => service.AddStudentAsync(It.IsAny<Student>()))
                .ReturnsAsync(new Student { User = new User { Email = expectedEmail } })
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<Student>()))
                .Returns(new UserDto { Email = expectedEmail })
                .Verifiable();
            var controller = new StudentsController(null, mockService.Object, mockMapper.Object);

            var response = await controller.Post(It.IsAny<UserViewModel>());

            var responseResult = Assert.IsType<OkObjectResult>(response.Result);
            var model = Assert.IsAssignableFrom<UserDto>(responseResult.Value);
            Assert.Equal(expectedEmail, model.Email);
            mockService.Verify();
            mockMapper.Verify();
        }

        [Fact]
        public async Task PostByUser_WithInvalidModel_ShouldReturnBadRequest()
        {

            var mockService = new Mock<IStudentService>();
            mockService.Setup(service => service.AddStudentAsync(It.IsAny<Student>()))
                .ReturnsAsync(null as Student)
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            var controller = new StudentsController(null, mockService.Object, mockMapper.Object);

            var response = await controller.Post(It.IsAny<UserViewModel>());

            Assert.IsType<BadRequestResult>(response.Result);
            mockService.Verify();
        }
    }
}
