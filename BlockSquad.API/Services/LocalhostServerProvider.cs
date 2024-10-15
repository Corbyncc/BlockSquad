using BlockSquad.Shared;
namespace BlockSquad.ServerCoordinator.Services;

public class LocalhostServerProvider : IServerProvider
{
    public Task CreateServerAsync()
    {
        return Task.CompletedTask;
    }
}
