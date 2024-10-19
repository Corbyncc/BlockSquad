using BlockSquad.Shared.Lobbies;

namespace BlockSquad.Api.Lobbies.Services;
public class LobbiesService : ILobbiesService
{
    // TODO: For consistency sake, maybe this should be a transient service that consumes a singleton service for the dictionary
    private Dictionary<int, Lobby> _lobbies;

    public LobbiesService()
    {
        _lobbies = [];
    }

    public Task<List<Lobby>?> GetLobbies()
    {
        return Task.FromResult<List<Lobby>?>([.. _lobbies.Values]);
    }

    public Task<Lobby?> GetLobbyAsync(int lobbyId)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            return Task.FromResult<Lobby?>(lobby);
        }

        return Task.FromResult<Lobby?>(null);
    }

    public Task<Lobby?> CreateLobbyAsync(Lobby newLobby)
    {
        newLobby.Id = _lobbies.Count + 1;

        if (!_lobbies.TryAdd(newLobby.Id, newLobby))
            return Task.FromResult<Lobby?>(null);

        return Task.FromResult<Lobby?>(newLobby);
    }

    public Task<Lobby?> UpdateLobbyAsync(int lobbyId, Lobby updatedLobby)
    {
        var lobby = _lobbies.GetValueOrDefault(lobbyId);
        if (lobby == null)
            return Task.FromResult<Lobby?>(null);

        lobby.HostSteamId = updatedLobby.HostSteamId;
        lobby.PlayerCount = updatedLobby.PlayerCount;

        return Task.FromResult<Lobby?>(lobby);
    }
}
