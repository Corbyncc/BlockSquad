using System;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;

[assembly: PluginMetadata("BlockSquad.Hub")]

namespace BlockSquad.Hub;

public class BlockSquadHubPlugin : OpenModUnturnedPlugin
{
    private readonly ILogger<BlockSquadHubPlugin> _logger;

    public BlockSquadHubPlugin(
        ILogger<BlockSquadHubPlugin> logger,
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
