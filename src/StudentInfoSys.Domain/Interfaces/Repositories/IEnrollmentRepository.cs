namespace StudentInfoSys.Domain.Interfaces.Repositories
{
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;

    public interface IEnrollmentRepository : IAsyncRepository<Enrollment>
    {
        Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment);
        Task RemoveEnrollmentAsync(Enrollment enrollment);
    }
}
