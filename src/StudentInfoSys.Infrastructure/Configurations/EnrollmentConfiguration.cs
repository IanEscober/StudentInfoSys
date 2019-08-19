namespace StudentInfoSys.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StudentInfoSys.Domain.Entities;

    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.CourseId });

            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            builder.HasOne(e => e.Course)
               .WithMany(s => s.Enrollments)
               .HasForeignKey(e => e.CourseId);
        }
    }
}
