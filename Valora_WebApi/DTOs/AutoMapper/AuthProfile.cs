using AutoMapper;
using Valora.DTOs.Auth;
using Valora.Models;

namespace Valora.DTOs.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // ApplicationUser -> UserDTO
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Roles will be set manually

            // RegisterRequestDTO -> ApplicationUser
            CreateMap<RegisterRequestDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
