using System;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;

[assembly: PluginMetadata("BlockSquad.Game")]

namespace BlockSquad.Game;

public class BlockSquadGamePlugin : OpenModUnturnedPlugin
{
    private readonly ILogger<BlockSquadGamePlugin> _logger;

    public BlockSquadGamePlugin(
        ILogger<BlockSquadGamePlugin> logger,
        IServiceProvider serviceProvider
    )
        : base(serviceProvider)
    {
        _logger = logger;
    }

    protected override UniTask OnLoadAsync()
    {
        _logger.LogInformation("Game plugin loaded");
        return UniTask.CompletedTask;
    }

    protected override UniTask OnUnloadAsync()
    {
        _logger.LogInformation("Game plugin unloaded");
        return UniTask.CompletedTask;
    }
}
