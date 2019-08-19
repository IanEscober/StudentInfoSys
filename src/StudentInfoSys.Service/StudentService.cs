namespace StudentInfoSys.Service
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var user = student.User;
            var newUser = new User { Email = user.Email, Password = user.Password, Firstname = user.Firstname, Lastname = user.Lastname, Gender = user.Gender };
            var newStudent = new Student { User = newUser };

            var result = await this.studentRepository.AddStudentAsync(newStudent);
            return result;
        }

        public async Task<Student> AuthenticateBasicAsync(string authHeader)
        {
            var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            var credentialBytes = Convert.FromBase64String(authHeaderValue.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var email = credentials[0];
            var password = credentials[1];

            var student = await this.studentRepository
                .GetStudentsAsync(s => s.User.Email == email && s.User.Password == password);

            return student?.FirstOrDefault();
        }

        public string GenerateToken(Student student, string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, student.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}
