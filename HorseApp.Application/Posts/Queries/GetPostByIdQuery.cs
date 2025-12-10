using HorseApp.Application.DTOs.Posts;
using MediatR;

namespace HorseApp.Application.Posts.Queries
{
    public sealed record GetPostByIdQuery(Guid Id) : IRequest<PostResponseDto>;
}
