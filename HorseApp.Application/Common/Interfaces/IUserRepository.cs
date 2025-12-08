using HorseApp.Domain.Entities;

namespace HorseApp.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user, CancellationToken ct);
        Task<bool> UsernameExistsAsync(string username, CancellationToken ct);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct);
        Task<User?> GetUserByIdAsync(Guid id, CancellationToken ct);
        Task<(List<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, CancellationToken ct);

        Task SaveChangesAsync(CancellationToken ct);

    }
}
