namespace StudentInfoSys.Domain.Interfaces.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interface.Specification;

    public interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<IReadOnlyCollection<Student>> GetStudentsAsync(ISpecification<Student> specification = null);
        Task<Student> GetStudentByIdAsyc(int id);
        Task<Student> AddStudentAsync(Student student);
    }
}
