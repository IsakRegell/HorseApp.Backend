using HorseApp.Application.Common.DTOs; // PaginationResponseDto
using HorseApp.Application.DTOs.Users;
using MediatR;

namespace HorseApp.Application.Users.Queries
{
    public sealed record GetAllUsersQuery(
    int Page,
    int PageSize
    ) : IRequest<PaginationResponseDto<UserListItemDto>>;
}
