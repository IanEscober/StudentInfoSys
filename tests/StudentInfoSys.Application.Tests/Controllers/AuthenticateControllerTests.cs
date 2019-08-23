namespace StudentInfoSys.Application.Tests.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using StudentInfoSys.Application.Controllers;
    using StudentInfoSys.Application.Models;
    using StudentInfoSys.Application.Models.Dtos;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Services;
    using Xunit;

    public class AuthenticateControllerTests
    {
        [Fact]
        public async Task Post_WithValidCredentials_ShouldReturnOk()
        {

            var expectedToken = "Test_Token";
            var key = new Key { JWT = "Test_JWT_Parent_Key" };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(service => service.AuthenticateBasicAsync(It.IsAny<string>()))
                .ReturnsAsync(new Student())
                .Verifiable();
            mockService.Setup(service => service.GenerateToken(It.IsAny<Student>(), It.IsAny<string>()))
                .Returns(expectedToken)
                .Verifiable();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<UserDto>(It.IsAny<Student>()))
                .Returns(new UserDto())
                .Verifiable();
            var controller = new AuthenticateController(mockService.Object, mockMapper.Object, key);

            var response = await controller.Post();

            var responseResult = Assert.IsType<OkObjectResult>(response.Result);
            var model = Assert.IsAssignableFrom<AuthorizeDto>(responseResult.Value);
            Assert.Equal(expectedToken, model.Token);
            mockService.Verify();
            mockMapper.Verify();
        }
    }
}
