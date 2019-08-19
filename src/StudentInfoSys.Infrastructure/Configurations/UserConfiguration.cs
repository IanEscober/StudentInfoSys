namespace StudentInfoSys.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StudentInfoSys.Domain.Entities;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Firstname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Lastname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Gender)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId);
        }
    }
}
