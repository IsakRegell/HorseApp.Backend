using HorseApp.Application.Common.DTOs;
using HorseApp.Application.DTOs.Posts;
using MediatR;

namespace HorseApp.Application.Posts.Queries
{
    public sealed record GetAllPostsQuery(int Page = 1, int PageSize = 10)
        : IRequest<PaginationResponseDto<PostListItemDto>>;
}
