using HorseApp.Domain.Entities;

namespace HorseApp.Application.Common.Interfaces
{
    public interface IPostRepository
    {
        Task AddPostAsync(Post post, CancellationToken ct);
        Task<Post?> GetPostByIdAsync(Guid id, CancellationToken ct);
        Task<(List<Post> Posts, int TotalCount)> GetAllPostsAsync(int page, int pageSize, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
