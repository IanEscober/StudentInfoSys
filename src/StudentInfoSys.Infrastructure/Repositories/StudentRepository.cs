namespace StudentInfoSys.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interface.Specification;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Specifications;

    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentInfoSysDbContext context) : base(context) { }

        public async Task<IReadOnlyCollection<Student>> GetStudentsAsync(ISpecification<Student> specification = null)
        {
            if (specification is null)
            {
                specification = new NullSpecification<Student>();
            }

            var students = await this.GetAsync(specification);
            var task = Task.Run(() => students.ToList()); // Due to IAsyncEnumerable == ToListAsync()

            var result = await task;
            return result.AsReadOnly();
        }

        public async Task<Student> GetStudentByIdAsyc(int id)
        {
            var student = await this.GetAsync(new StudentFilterSpecification(id)
                .With(new StudentIncludesSpecification(true)));
            return student.SingleOrDefault();
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var newStudent = await this.AddAsync(student);
            return newStudent;
        }
    }
}
