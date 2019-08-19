namespace StudentInfoSys.Application.Profiles
{
    using AutoMapper;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Application.Models;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<UserDto, Student>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForPath(dest => dest.User.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForPath(dest => dest.User.Gender, opt => opt.MapFrom(src => src.Gender))
                .ReverseMap();
        }
    }
}
