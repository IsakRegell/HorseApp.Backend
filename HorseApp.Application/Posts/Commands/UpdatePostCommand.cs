using HorseApp.Application.DTOs.Posts;
using MediatR;

namespace HorseApp.Application.Posts.Commands
{
    public sealed record UpdatePostCommand(Guid Id, UpdatePostDto Dto)
        : IRequest<PostResponseDto>;
}
