using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Posts;
using HorseApp.Domain.Entities;
using MediatR;

namespace HorseApp.Application.Posts.Commands
{
    public sealed class CreatePostCommandHandler
        : IRequestHandler<CreatePostCommand, PostResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }


        public async Task<PostResponseDto> Handle(CreatePostCommand request, CancellationToken ct)
        {
            // 1️ Mappa DTO → Entity
            var post = _mapper.Map<Post>(request.Dto);

            // 2️ Sätt tidsstämpel
            post.CreatedAtUtc = DateTime.UtcNow;

            // 3️ Spara i databasen
            await _postRepository.AddPostAsync(post, ct);
            await _postRepository.SaveChangesAsync(ct);

            // 4️ Mappa tillbaka till Response DTO
            var response = _mapper.Map<PostResponseDto>(post);

            return response;
        }

    }
}
