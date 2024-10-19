namespace BlockSquad.Shared.Lobbies;
public class Lobby
{
    public int Id { get; set; }
    public ulong HostSteamId { get; set; }
    public string? Name { get; set; }
    public int PlayerCount { get; set; }
}
