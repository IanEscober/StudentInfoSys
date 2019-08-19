namespace StudentInfoSys.Service
{
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;

    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<Enrollment> AddCourseToStudentAsync(int studentId, int courseId)
        {
            var newEnrollment = new Enrollment { StudentId = studentId, CourseId = courseId };
            var result = await this.enrollmentRepository.AddEnrollmentAsync(newEnrollment);

            return newEnrollment;
        }

        public async Task RemoveCourseFromStudentAsync(int studentId, int courseId)
        {
            var enrollmentToRemove = new Enrollment { StudentId = studentId, CourseId = courseId };
            await this.enrollmentRepository.RemoveEnrollmentAsync(enrollmentToRemove);
        }
    }
}
