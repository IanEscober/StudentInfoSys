namespace StudentInfoSys.Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StudentInfoSys.Application.Models;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IStudentService studentService;
        private readonly IEnrollmentService enrollmentService;
        private readonly IMapper mapper;

        public StudentsController(
            IStudentRepository studentRepository,
            IStudentService studentService,
            IEnrollmentService enrollmentService,
            IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.studentService = studentService;
            this.enrollmentService = enrollmentService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var students = await this.studentRepository.GetStudentsAsync();
            var usersDto = this.mapper.Map<IReadOnlyCollection<UserDto>>(students);

            if (usersDto.Any())
            {
                return this.Ok(usersDto);
            }

            return null;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Post([FromBody] User user)
        {
            var newStudent = await this.studentService.AddStudentAsync(new Student { User = user });
            var newUser = this.mapper.Map<UserDto>(newStudent);

            if (newUser != null)
            {
                return this.Ok(newUser);
            }

            return this.BadRequest();
        }

        [HttpPost("{id}/enrollments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(int id, [FromBody] Course course)
        {
            var enrollment = await this.enrollmentService.AddCourseToStudentAsync(id, course.CourseId);

            if (enrollment != null)
            {
                return this.Ok("Successfully Enrolled");
            }

            return this.BadRequest(new { message = "The student or course does not exists" });
        }

        [HttpDelete("{id}/enrollments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id, [FromBody] Course course)
        {
            await this.enrollmentService.RemoveCourseFromStudentAsync(id, course.CourseId);

            return this.Ok("Successfully Removed");
        }
    }
}