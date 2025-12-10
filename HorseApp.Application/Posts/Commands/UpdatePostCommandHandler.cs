using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Posts;
using MediatR;


namespace HorseApp.Application.Posts.Commands
{
    public sealed class UpdatePostCommandHandler
        : IRequestHandler<UpdatePostCommand, PostResponseDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostResponseDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            // 1️ Hämta befintlig post
            var post = await _postRepository.GetPostByIdAsync(request.Id, cancellationToken);
            if (post is null)
                throw new KeyNotFoundException("Inlägg hittades inte");

            // 2️ Uppdatera fält endast om de finns i DTO
            if (request.Dto.Title is not null)
                post.Title = request.Dto.Title;

            if (request.Dto.Description is not null)
                post.Description = request.Dto.Description;

            if (request.Dto.Status.HasValue)
                post.Status = request.Dto.Status.Value;

            if (request.Dto.LocationId.HasValue)
                post.LocationId = request.Dto.LocationId.Value;

            post.UpdatedAtUtc = DateTime.UtcNow;

            // 3️ Spara
            await _postRepository.SaveChangesAsync(cancellationToken);

            // 4️ Returnera uppdaterad post
            return _mapper.Map<PostResponseDto>(post);
        }
    }
}
