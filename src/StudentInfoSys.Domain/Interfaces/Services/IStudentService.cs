namespace StudentInfoSys.Domain.Interfaces.Services
{
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;

    public interface IStudentService
    {
        Task<Student> AddStudentAsync(Student student);
        Task<Student> AuthenticateBasicAsync(string authHeader);
        string GenerateToken(Student student, string key);
    }
}
