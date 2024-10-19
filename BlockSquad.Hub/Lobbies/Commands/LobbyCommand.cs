using BlockSquad.Shared.Lobbies;
using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Commands;
using System;

namespace BlockSquad.Hub.Lobbies.Commands;
[Command("lobby")]
[CommandSyntax("<list/create>")]
public class LobbyCommand : UnturnedCommand
{
    public LobbyCommand(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override UniTask OnExecuteAsync()
    {
        throw new CommandWrongUsageException(Context);
    }
}
