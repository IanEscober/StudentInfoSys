namespace StudentInfoSys.Application.Profiles
{
    using AutoMapper;
    using StudentInfoSys.Application.Models;
    using StudentInfoSys.Domain.Entities;

    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            this.CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
