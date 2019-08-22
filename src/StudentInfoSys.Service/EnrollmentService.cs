namespace StudentInfoSys.Service
{
    using System;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Logging;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;

    public class EnrollmentService : IEnrollmentService
    {
        private readonly IBaseLogger<EnrollmentService> baseLogger;
        private readonly IEnrollmentRepository enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IBaseLogger<EnrollmentService> baseLogger)
        {
            this.baseLogger = baseLogger;
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<Enrollment> AddCourseToStudentAsync(int studentId, int courseId)
        {
            var newEnrollment = new Enrollment { StudentId = studentId, CourseId = courseId };

            try
            {
                var result = await this.enrollmentRepository.AddEnrollmentAsync(newEnrollment);
                this.baseLogger.LogInfo($"Course {courseId} was added enrolled to Student {studentId}");
                return newEnrollment;
            }
            catch
            {
                throw new Exception("Cannot add course to student");
            }
        }

        public async Task RemoveCourseFromStudentAsync(int studentId, int courseId)
        {
            var enrollmentToRemove = new Enrollment { StudentId = studentId, CourseId = courseId };

            try
            {
                await this.enrollmentRepository.RemoveEnrollmentAsync(enrollmentToRemove);
                this.baseLogger.LogInfo($"Course {courseId} was added un-enrolled to Student {studentId}");
            }
            catch
            {
                throw new Exception("Cannot remove course from student");
            }
        }
    }
}
