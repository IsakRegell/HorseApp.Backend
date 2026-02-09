using HorseApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Application.Posts.Commands
{
    public sealed class DeletePostCommandHandler
        : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public DeletePostCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            // 1️ Hämta posten
            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (post is null)
                throw new KeyNotFoundException("Inlägget hittades inte");

            // 2️ Soft delete
            post.IsDeleted = true;
            post.DeletedAtUtc = DateTime.UtcNow;

            // 3️ Spara ändringarna
            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
