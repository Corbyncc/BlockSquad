using BlockSquad.Shared.Lobbies;
using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Core.Console;
using OpenMod.Unturned.Commands;
using OpenMod.Unturned.Users;
using System;

namespace BlockSquad.Hub.Lobbies.Commands;
[Command("create")]
[CommandParent(typeof(LobbyCommand))]
public class LobbyCreateCommand : UnturnedCommand
{
    private readonly ILobbiesService _lobbiesService;

    public LobbyCreateCommand(ILobbiesService lobbiesService, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _lobbiesService = lobbiesService;
    }

    protected override async UniTask OnExecuteAsync()
    {
        var newLobby = new Lobby();

        if (Context.Actor is ConsoleActor)
        {
            newLobby.HostSteamId = 123;
        }

        if (Context.Actor is UnturnedUser unturnedUser)
        {
            newLobby.HostSteamId = (ulong)unturnedUser.SteamId;
        }

        var createdLobby = await _lobbiesService.CreateLobbyAsync(newLobby);
        if (createdLobby == null)
        {
            await Context.Actor.PrintMessageAsync($"Error failed to create lobby");
            return;
        }

        await Context.Actor.PrintMessageAsync($"Created lobby with id {createdLobby.Id}");
    }
}
