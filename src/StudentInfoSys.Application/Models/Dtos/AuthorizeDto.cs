namespace StudentInfoSys.Application.Models.Dtos
{
    public class AuthorizeDto
    {
        public string Token { get; set; }
        public UserDto Data { get; set; }
    }
}
