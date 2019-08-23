namespace StudentInfoSys.Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StudentInfoSys.Application.Models.Dtos;
    using StudentInfoSys.Application.Models.ViewModels;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IStudentService studentService;
        private readonly IMapper mapper;

        public StudentsController(
            IStudentRepository studentRepository,
            IStudentService studentService,
            IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.studentService = studentService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var students = await this.studentRepository.GetStudentsAsync();
            var usersDto = this.mapper.Map<IReadOnlyCollection<UserDto>>(students);

            if (usersDto.Any())
            {
                return this.Ok(usersDto);
            }

            return this.NoContent();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var student = await this.studentRepository.GetStudentByIdAsyc(id);
            var studentDto = this.mapper.Map<StudentDto>(student);

            if (studentDto != null)
            {
                return this.Ok(studentDto);
            }

            return this.NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserViewModel user)
        {
            var newStudent = await this.studentService.AddStudentAsync(new Student { User = this.mapper.Map<User>(user) });
            var newUser = this.mapper.Map<UserDto>(newStudent);

            if (newUser != null)
            {
                return this.Ok(newUser);
            }

            return this.BadRequest();
        }
    }
}