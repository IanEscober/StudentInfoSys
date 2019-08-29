namespace StudentInfoSys.Domain.Specifications
{
    using StudentInfoSys.Domain.Entities;

    public class StudentFilterSpecification : BaseSpecification<Student>
    {
        public StudentFilterSpecification(int id)
        {
            this.ApplyFilter(s => s.UserId == id);
        }

        public StudentFilterSpecification(string email, string password)
        {
            this.ApplyFilter(s => s.User.Email == email && s.User.Password == password);
        }

        public StudentFilterSpecification(string gender)
        {
            this.ApplyFilter(s => s.User.Gender == gender);
        }
    }
}
