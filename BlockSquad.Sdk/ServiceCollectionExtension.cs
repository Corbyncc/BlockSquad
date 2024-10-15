using BlockSquad.Sdk.API;
using BlockSquad.Shared.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BlockSquad.Sdk
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlockSquadSdk(this IServiceCollection services)
        {
            // Register HttpClient and internal API client
            services.AddHttpClient<IApiClient, ApiClient>();

            // Register the exposed service
            // services.AddTransient<IMySdkService, MySdkService>();

            services.AddTransient<IUsersService, BlockSquad.Sdk.Users.UsersService>();
        }
    }
}
