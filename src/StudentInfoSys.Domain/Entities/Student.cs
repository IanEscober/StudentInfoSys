namespace StudentInfoSys.Domain.Entities
{
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            this.Enrollments = new HashSet<Enrollment>();
        }

        public int StudentId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
