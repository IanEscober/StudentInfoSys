namespace StudentInfoSys.Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;

    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(StudentInfoSysDbContext context) : base(context) { }

        public async Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
        {
            var newEnrollment = await this.AddAsync(enrollment);
            return newEnrollment;
        }

        public async Task RemoveEnrollmentAsync(Enrollment enrollment)
        {
            await this.DeleteAsync(enrollment);
        }
    }
}
