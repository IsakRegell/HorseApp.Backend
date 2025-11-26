using AutoMapper;
using HorseApp.Application.DTOs.Users;
using HorseApp.Domain.Entities;

namespace HorseApp.Application.Mapping
{
    // AutoMapper-profil för User-relaterade mappingar
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // CreateUserDto -> User (när vi skapar en ny user)
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsBankIdVerified, opt => opt.Ignore())
                .ForMember(dest => dest.VerifiedAtUtc, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAtUtc, opt => opt.Ignore());

            // User -> UserResponseDto (när vi returnerar en user till klienten)
            CreateMap<User, UserResponseDto>();
        }
    }
}
