namespace StudentInfoSys.Application.Helpers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Text.Encodings.Web;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using StudentInfoSys.Domain.Entities;
    using Microsoft.AspNetCore.Authentication;
    using StudentInfoSys.Domain.Interfaces.Services;

    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IStudentService studentService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IStudentService studentService)
            : base(options, logger, encoder, clock)
        {
            this.studentService = studentService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var student = default(Student);

            if (this.Request.Headers.ContainsKey("Authorization"))
            {
                try
                {
                    student = await this.studentService.AuthenticateBasicAsync(this.Request?.Headers["Authorization"]);

                    if(student is null)
                    {
                        return AuthenticateResult.Fail("Invalid Username or Password");
                    }
                }
                catch
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }

            }
            else
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, student.UserId.ToString()),
                new Claim(ClaimTypes.Name, student.User.Email),
            };
            var identity = new ClaimsIdentity(claims, this.Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
