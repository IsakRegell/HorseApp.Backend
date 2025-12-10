using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Posts;
using MediatR;

namespace HorseApp.Application.Posts.Queries
{
    public sealed class GetPostByIdQueryHandler
        : IRequestHandler<GetPostByIdQuery, PostResponseDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostResponseDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostByIdAsync(request.Id, cancellationToken);

            if (post is null)
                throw new KeyNotFoundException("Inlägget hittades inte");

            return _mapper.Map<PostResponseDto>(post);
        }
    }
}
