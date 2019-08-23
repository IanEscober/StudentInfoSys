namespace StudentInfoSys.Application.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class UserViewModel
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [Required]
        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [Required]
        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}
