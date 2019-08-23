namespace StudentInfoSys.Application.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class CourseViewModel
    {
        [Required]
        [JsonProperty("courseid")]
        public int CourseId { get; set; }
    }
}
