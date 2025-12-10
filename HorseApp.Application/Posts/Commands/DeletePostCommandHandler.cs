using HorseApp.Application.Common.Interfaces;
using MediatR;

namespace HorseApp.Application.Posts.Commands
{
    public sealed class DeletePostCommandHandler
        : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            // 1️ Hämta posten
            var post = await _postRepository.GetPostByIdAsync(request.Id, cancellationToken);

            if (post is null)
                throw new KeyNotFoundException("Inlägget hittades inte");

            // 2️ Soft delete
            post.IsDeleted = true;
            post.DeletedAtUtc = DateTime.UtcNow;

            // 3️ Spara ändringarna
            await _postRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
