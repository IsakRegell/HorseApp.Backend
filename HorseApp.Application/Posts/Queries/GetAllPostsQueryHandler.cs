using AutoMapper;
using HorseApp.Application.Common.DTOs;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Posts;
using MediatR;

namespace HorseApp.Application.Posts.Queries
{
    public sealed class GetAllPostsQueryHandler
        : IRequestHandler<GetAllPostsQuery, PaginationResponseDto<PostListItemDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<PostListItemDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var (posts, totalCount) = await _postRepository.GetAllPostsAsync(
                request.Page, request.PageSize, cancellationToken);

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
