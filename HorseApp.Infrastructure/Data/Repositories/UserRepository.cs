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
                .Where(u => !u.IsDeleted)
                .FirstOrDefaultAsync(u => u.Id == id, ct);
        }



        public async Task<(List<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, CancellationToken ct)
        {
            // 1. Skydda mot konstiga värden
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10; // rimligt default
            }

            // 2. Bas-query mot Users-tabellen
            var query = _dbContext.Users
                .AsNoTracking()                // bättre prestanda det gör att EF inte trackar entities utan bara läser dem från databasen och returnerar dem som vanliga objekt
                .Where(u => !u.IsDeleted)      // exkludera soft-deletade users
                .OrderBy(u => u.CreatedAtUtc); // konsekvent sortering (äldst först t.ex.)

            // 3. Ta reda på totalt antal users (utan pagination)
            var totalCount = await query.CountAsync();

            // 4. Hämta rätt sida (Skip + Take)
            var users = await query
                .Skip((page - 1) * pageSize)   // hoppa över tidigare sidor
                .Take(pageSize)                // ta X st
                .ToListAsync();

            // 5. Returnera både listan + totalCount som en tuple
            return (users, totalCount);
        }




        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);

        }
    }
}
