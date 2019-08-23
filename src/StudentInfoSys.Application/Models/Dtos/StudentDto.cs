namespace StudentInfoSys.Application.Models.Dtos
{
    using System.Collections.Generic;

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
