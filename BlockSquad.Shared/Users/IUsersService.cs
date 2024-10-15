using System.Threading.Tasks;

namespace BlockSquad.Shared.Users
{
    public interface IUsersService
    {
        Task<User?> GetUserAsync(int userId);
        Task<User?> GetUserBySteamIdAsync(ulong steamId);
        Task<User?> CreateUserAsync(User user);
    }
}
