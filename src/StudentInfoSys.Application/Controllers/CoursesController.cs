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
    using StudentInfoSys.Domain.Interfaces.Repositories;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public CoursesController(ICourseRepository courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)] 
        public async Task<ActionResult<IEnumerable<CourseDto>>> Get()
        {
            var courses = await this.courseRepository.GetCoursesAsync();

            if (courses.Any())
            {
                var coursesDto = this.mapper.Map<IReadOnlyCollection<CourseDto>>(courses);
                return this.Ok(coursesDto);
            }

            return this.NoContent();
        }
    }
}