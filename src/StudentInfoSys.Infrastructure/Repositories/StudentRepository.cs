namespace StudentInfoSys.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;

    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentInfoSysDbContext context) : base(context) { }

        public async Task<IReadOnlyCollection<Student>> GetStudentsAsync(Expression<Func<Student, bool>> query = null)
        {
            var students = this.context.Set<Student>()
                .Include(s => s.User)
                .Include(s => s.Enrollments)
                .AsQueryable();

            if(query != null)
            {
                students = students.Where(query);
            }

            var task = Task.Run(() => students.ToList()); // Due to IAsyncEnumerable == ToListAsync()

            var result = await task;
            return result.AsReadOnly();
        }

        public async Task<Student> GetStudentByIdAsyc(int id)
        {
            var task = Task.Run(() => this.context.Set<Student>() // Due to IAsyncEnumerable == SingleAsync()
                .Include(s => s.User)
                .Include(s => s.Enrollments)
                    .ThenInclude(s => s.Course)
                .Single(s => s.UserId == id));

            var student = await task;

            return student;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var newStudent = await this.AddAsync(student);
            return newStudent;
        }
    }
}
