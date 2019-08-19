namespace StudentInfoSys.Domain.Interfaces.Services
{
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;

    public interface IEnrollmentService
    {
        Task<Enrollment> AddCourseToStudentAsync(int studentId, int courseId);
        Task RemoveCourseFromStudentAsync(int studentId, int courseId);
    }
}
