namespace StudentInfoSys.Application.Models
{
    using System.Collections.Generic;
    using StudentInfoSys.Domain.Entities;

    public class StudentDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public IEnumerable<CourseDto> Enrollments { get; set; }
    }
}
