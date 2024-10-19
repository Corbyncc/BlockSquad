using BlockSquad.Shared.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BlockSquad.Sdk
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlockSquadSdk(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, Users.UsersService>();
        }
    }
}
