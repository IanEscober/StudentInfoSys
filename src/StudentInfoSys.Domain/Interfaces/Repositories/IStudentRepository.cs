namespace StudentInfoSys.Domain.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;

    public interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<IReadOnlyCollection<Student>> GetStudentsAsync(Expression<Func<Student, bool>> query = null);
        Task<Student> GetStudentByIdAsyc(int id);
        Task<Student> AddStudentAsync(Student student);
    }
}
