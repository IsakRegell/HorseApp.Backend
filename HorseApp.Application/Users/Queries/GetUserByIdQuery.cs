using HorseApp.Application.DTOs.Users;
using MediatR;

namespace HorseApp.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponseDto?>
    {
        public Guid Id { get; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
