using System;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;

[assembly: PluginMetadata("BlockSquad.Lobby")]

namespace BlockSquad.Lobby;

public class BlockSquadLobbyPlugin : OpenModUnturnedPlugin
{
    private readonly ILogger<BlockSquadLobbyPlugin> _logger;

    public BlockSquadLobbyPlugin(
        ILogger<BlockSquadLobbyPlugin> logger,
        IServiceProvider serviceProvider
    )
        : base(serviceProvider)
    {
        _logger = logger;
    }

    protected override UniTask OnLoadAsync()
    {
        _logger.LogInformation("Lobby plugin loaded");
        return UniTask.CompletedTask;
    }

    protected override UniTask OnUnloadAsync()
    {
        _logger.LogInformation("Lobby plugin unloaded");
        return UniTask.CompletedTask;
    }
}
