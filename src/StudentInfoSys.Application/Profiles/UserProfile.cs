namespace StudentInfoSys.Application.Profiles
{
    using AutoMapper;
    using StudentInfoSys.Domain.Entities;
    using StudentInfoSys.Application.Models.Dtos;
    using StudentInfoSys.Application.Models.ViewModels;

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

            this.CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
        }
    }
}
