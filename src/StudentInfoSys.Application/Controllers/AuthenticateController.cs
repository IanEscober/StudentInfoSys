namespace StudentInfoSys.Application.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StudentInfoSys.Application.Models;
    using StudentInfoSys.Domain.Interfaces.Services;

    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AuthenticateController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IMapper mapper;
        private readonly Key key;

        public AuthenticateController(
            IStudentService studentService, 
            IMapper mapper,
            Key key)
        {
            this.studentService = studentService;
            this.mapper = mapper;
            this.key = key;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorizeDto>> Post()
        {
            var student = await this.studentService.AuthenticateBasicAsync(this.Request?.Headers["Authorization"]);

            if (student != null)
            {
                var token = this.studentService.GenerateToken(student, this.key.JWT);
                var userDto = this.mapper.Map<UserDto>(student);
                var authorizeDto = new AuthorizeDto { Token = token, Data = userDto };
                return this.Ok(authorizeDto);
            }

            return this.BadRequest("Invalid Credentials");
        }
    }
}