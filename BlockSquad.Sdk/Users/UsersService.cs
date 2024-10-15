using BlockSquad.Sdk.API;
using BlockSquad.Shared.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlockSquad.Sdk.Users
{
    public class UsersService : IUsersService
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<UsersService> _logger;

        private const string BASE_URI = "https://localhost:7225/api/users";

        public UsersService(IApiClient apiClient, ILogger<UsersService> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            var response = await _apiClient.GetAsync<User>($"{BASE_URI}/?userId={userId}");
            if (!response.IsSuccess || response.Data == null)
                return null;

            return response.Data;
        }

        public async Task<User?> GetUserBySteamIdAsync(ulong steamId)
        {
            var response = await _apiClient.GetAsync<User>($"{BASE_URI}/?steamId={steamId}");
            if (!response.IsSuccess || response.Data == null)
                return null;

            return response.Data;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            var response = await _apiClient.PostAsync<User, User>(BASE_URI, user);
            if (!response.IsSuccess || response.Data == null)
            {
                _logger.LogError($"Error creating user {response.ErrorMessage}");
                throw new Exception($"Error creating user {response.ErrorMessage}");
            }

            return response.Data;
        }
    }
}
