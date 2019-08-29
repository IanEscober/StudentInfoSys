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
    using StudentInfoSys.Domain.Interfaces.Logging;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;
    using StudentInfoSys.Domain.Specifications;

    public class StudentService : IStudentService
    {
        private readonly IBaseLogger<StudentService> baseLogger;
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository, IBaseLogger<StudentService> baseLogger)
        {
            this.baseLogger = baseLogger;
            this.studentRepository = studentRepository;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var user = student.User;
            var newUser = new User { Email = user.Email, Password = user.Password, Firstname = user.Firstname, Lastname = user.Lastname, Gender = user.Gender };
            var newStudent = new Student { User = newUser };

            try
            {
                var result = await this.studentRepository.AddStudentAsync(newStudent);
                this.baseLogger.LogInfo($"Added {student.User.Firstname} to students");
                return result;
            }
            catch
            {
                throw new Exception("Cannot add student");
            } 
        }

        public async Task<Student> AuthenticateBasicAsync(string authHeader)
        {
            var email = default(string);
            var password = default(string);

            try
            {
                var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
                var credentialBytes = Convert.FromBase64String(authHeaderValue.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                email = credentials[0];
                password = credentials[1];
            }
            catch
            {
                throw new Exception("Cannot parse header");
            }
            
            var student = await this.studentRepository
                .GetStudentsAsync(new StudentFilterSpecification(email, password)
                .With(new StudentIncludesSpecification()));

            if (student.Any())
            {
                return student.First();
            }
            else
            {
                this.baseLogger.LogWarn("Student does not exist");
                return null;
            }
            
        }

        public string GenerateToken(Student student, string key)
        {
            try
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
            catch
            {
                throw new Exception("Cannot generate token");
            }
        }
    }
}
