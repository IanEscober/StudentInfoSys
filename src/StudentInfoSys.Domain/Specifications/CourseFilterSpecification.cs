namespace StudentInfoSys.Domain.Specifications
{
    using StudentInfoSys.Domain.Entities;

    public class CourseFilterSpecification : BaseSpecification<Course>
    {
        public CourseFilterSpecification(int id)
        {
            this.ApplyFilter(c => c.CourseId == id);
        }
    }
}
