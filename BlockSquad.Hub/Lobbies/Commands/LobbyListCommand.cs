using BlockSquad.Shared.Lobbies;
using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Commands;
using System;

namespace BlockSquad.Hub.Lobbies.Commands;
[Command("list")]
[CommandParent(typeof(LobbyCommand))]
[CommandSyntax("[lobbyId]")]
public class LobbyListCommand : UnturnedCommand
{
    private readonly ILobbiesService _lobbiesService;

    public LobbyListCommand(ILobbiesService lobbiesService, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _lobbiesService = lobbiesService;
    }

    protected override async UniTask OnExecuteAsync()
    {
        // Search by lobbyId
        if (Context.Parameters.TryGet<int>(0, out int lobbyId))
        {
            return;
        }

        // Otherwise, return all lobbies
        var lobbies = await _lobbiesService.GetLobbies();
        if (lobbies == null)
        {
            await Context.Actor.PrintMessageAsync($"No lobbies found");
            return;
        }

        await Context.Actor.PrintMessageAsync($"Total Lobbies: {lobbies.Count}");
    }
}
