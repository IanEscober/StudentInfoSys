namespace StudentInfoSys.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using StudentInfoSys.Domain.Entities;

    public class StudentInfoSysDbContext : DbContext
    {
        public StudentInfoSysDbContext() : base() { }
        public StudentInfoSysDbContext(DbContextOptions<StudentInfoSysDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentInfoSysDbContext).Assembly);
        }
    }
}
