using HorseApp.Application.DTOs.Users;     
using MediatR;                              

namespace HorseApp.Application.Users.Commands
{
    public sealed record UpdateUserProfileCommand(
        Guid UserId,                    // Identifier of the user to be updated
        UpdateUserProfileDto Payload    // Data for updating the user's profile
        ) : IRequest<UserResponseDto>;  // Command to update a user's profile and return the updated user data

}
