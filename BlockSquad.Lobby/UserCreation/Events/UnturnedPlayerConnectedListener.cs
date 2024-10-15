using BlockSquad.Shared.Users;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Players.Connections.Events;
using System.Threading.Tasks;

namespace BlockSquad.Lobby.UserCreation.Events
{
    public class UnturnedPlayerConnectedListener : IEventListener<UnturnedPlayerConnectedEvent>
    {
        private readonly IUsersService _usersService;
        public UnturnedPlayerConnectedListener(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task HandleEventAsync(object? sender, UnturnedPlayerConnectedEvent @event)
        {
            var user = await _usersService.GetUserBySteamIdAsync((ulong)@event.Player.SteamId);
            if (user != null)
                return;

            // Create user
            user = await _usersService.CreateUserAsync(new User
            {
                SteamId = (ulong)@event.Player.SteamId,
                Codename = @event.Player.Player.name
            });
        }
    }
}
