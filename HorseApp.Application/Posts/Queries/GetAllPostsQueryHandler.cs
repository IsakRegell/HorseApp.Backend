using AutoMapper;
using HorseApp.Application.Common.DTOs;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Application.Posts.Queries
{
    public sealed class GetAllPostsQueryHandler
        : IRequestHandler<GetAllPostsQuery, PaginationResponseDto<PostListItemDto>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<PostListItemDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _db.Posts
                .AsNoTracking()
                .Where(x => !x.IsDeleted);

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            var posts = await baseQuery
                .OrderByDescending(x => x.CreatedAtUtc)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var postDtos = _mapper.Map<List<PostListItemDto>>(posts);

            return new PaginationResponseDto<PostListItemDto>
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                Items = postDtos
            };
        }
    }
}
