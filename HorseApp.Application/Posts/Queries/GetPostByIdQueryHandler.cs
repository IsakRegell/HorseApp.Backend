using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Application.Posts.Queries
{
    public sealed class GetPostByIdQueryHandler
        : IRequestHandler<GetPostByIdQuery, PostResponseDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PostResponseDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _db.Posts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

            if (post is null)
                throw new KeyNotFoundException("Inlägget hittades inte");

            return _mapper.Map<PostResponseDto>(post);
        }
    }
}
