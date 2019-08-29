namespace StudentInfoSys.Domain.Specifications
{
    using StudentInfoSys.Domain.Entities;

    public class StudentIncludesSpecification : BaseSpecification<Student>
    {
        public StudentIncludesSpecification(bool withCourse = false)
        {
            this.AddInclude(s => s.User);
            this.AddInclude(s => s.Enrollments);

            if (withCourse)
            {
                // .ThenInclude Temp Imp - several PR's are being made on EFCore's Repo
                this.AddInclude($"{nameof(Student.Enrollments)}.{nameof(Enrollment.Course)}");
            }
        }
    }
}
