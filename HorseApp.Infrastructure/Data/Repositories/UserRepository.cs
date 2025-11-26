using HorseApp.Application.Common.Interfaces;
using HorseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace HorseApp.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user, CancellationToken ct)
        {
            await _dbContext.Users.AddAsync(user, ct);
        }

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken ct)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username, ct);
        }
                                 
        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email, ct);
        }


        public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id, ct);
        }














        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);

        }
    }
}
