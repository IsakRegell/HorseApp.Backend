using HorseApp.Application.Common.Interfaces;
using HorseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Infrastructure.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task AddPostAsync(Post post, CancellationToken ct)
        {
            await _dbContext.Posts.AddAsync(post, ct);
        }

        
        public async Task<Post?> GetPostByIdAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Posts
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        
        public async Task<(List<Post> Posts, int TotalCount)> GetAllPostsAsync(
            int page,
            int pageSize,
            CancellationToken ct)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _dbContext.Posts
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAtUtc); // nyaste först

            var totalCount = await query.CountAsync(ct);

            var posts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (posts, totalCount);
        }

        
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
