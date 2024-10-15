using BlockSquad.API.Database;
using BlockSquad.Shared.Users;
using Microsoft.EntityFrameworkCore;

namespace BlockSquad.API.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext _dbContext;

        public UsersService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            return await _dbContext.User
                .Include(x => x.Appearance)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User?> GetUserBySteamIdAsync(ulong steamId)
        {
            return await _dbContext.User
                .Include(x => x.Appearance)
                .FirstOrDefaultAsync(x => x.SteamId == steamId);
        }

        public async Task<User?> CreateUserAsync(User newUser)
        {
            var createdUser = await _dbContext.User.AddAsync(newUser);

            await _dbContext.SaveChangesAsync();

            return createdUser.Entity;
        }
    }
}
