namespace StudentInfoSys.Application.Profiles
{
    using AutoMapper;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Application.Models;
    using System.Linq;

    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            this.CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.User.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.User.Lastname))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments.Select(s => s.Course)));
        }
    }
}
