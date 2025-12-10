using HorseApp.Application.DTOs.Posts;
using MediatR;

namespace HorseApp.Application.Posts.Commands
{
    public sealed record CreatePostCommand(CreatePostDto Dto)
        : IRequest<PostResponseDto>;
}
