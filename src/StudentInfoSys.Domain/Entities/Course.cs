namespace StudentInfoSys.Domain.Entities
{
    using System.Collections.Generic;

    public class Course
    {
        public Course()
        {
            this.Enrollments = new HashSet<Enrollment>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
