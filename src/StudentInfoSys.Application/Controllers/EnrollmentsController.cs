namespace StudentInfoSys.Application.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StudentInfoSys.Application.Models.ViewModels;
    using StudentInfoSys.Domain.Interfaces.Services;

    [Authorize]
    [ApiController]
    [Route("api/students")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }

        [HttpPost("{id}/enrollments")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(int id, [FromBody] CourseViewModel course)
        {
            var enrollment = await this.enrollmentService.AddCourseToStudentAsync(id, course.CourseId);

            if (enrollment != null)
            {
                return this.Ok("Successfully Enrolled");
            }

            return this.BadRequest(new { message = "The student or course does not exists" });
        }

        [HttpDelete("{id}/enrollments")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id, [FromBody] CourseViewModel course)
        {
            await this.enrollmentService.RemoveCourseFromStudentAsync(id, course.CourseId);

            return this.Ok("Successfully Removed");
        }
    }
}