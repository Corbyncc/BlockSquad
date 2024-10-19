using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockSquad.Shared.Lobbies;
public interface ILobbiesService
{
    Task<List<Lobby>?> GetLobbies();
    Task<Lobby?> GetLobbyAsync(int lobbyId);
    Task<Lobby?> CreateLobbyAsync(Lobby newLobby);
    Task<Lobby?> UpdateLobbyAsync(int lobbyId, Lobby updatedLobby);
}
