using BlockSquad.Sdk.Users;
using BlockSquad.Shared.Api;
using BlockSquad.Shared.Lobbies;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockSquad.Sdk.Lobbies
{
    public class LobbiesService : ILobbiesService
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<UsersService> _logger;

        private const string BASE_URI = "https://localhost:7225/api/lobbies";

        public LobbiesService(IApiClient apiClient, ILogger<UsersService> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<List<Lobby>?> GetLobbies()
        {
            _logger.LogInformation($"Calling GetAllAsync on lobbies from SDK");

            var response = await _apiClient.GetAsync<List<Lobby>?>($"{BASE_URI}");
            if (!response.IsSuccess || response.Data == null)
            {
                _logger.LogError($"Error getting lobby list {response.ErrorMessage}");
                return null;
            }

            return response.Data;
        }

        public Task<Lobby?> GetLobbyAsync(int lobbyId)
        {
            throw new NotImplementedException();
        }

        public async Task<Lobby?> CreateLobbyAsync(Lobby newLobby)
        {
            var response = await _apiClient.PostAsync<Lobby, Lobby>($"{BASE_URI}", newLobby);
            if (!response.IsSuccess || response.Data == null)
                return null;

            return response.Data;
        }

        public Task<Lobby?> UpdateLobbyAsync(int lobbyId, Lobby updatedLobby)
        {
            throw new NotImplementedException();
        }
    }
}
