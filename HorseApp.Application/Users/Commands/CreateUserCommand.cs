using HorseApp.Application.DTOs.Users;
using MediatR;

namespace HorseApp.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserResponseDto>
    {
        public CreateUserDto Dto { get; }

        public CreateUserCommand(CreateUserDto dto)
        {
            Dto = dto;
        }
    }
}
