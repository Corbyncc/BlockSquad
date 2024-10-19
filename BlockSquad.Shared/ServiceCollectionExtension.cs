using BlockSquad.Shared.Api;
using Microsoft.Extensions.DependencyInjection;

namespace BlockSquad.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlockSquadShared(this IServiceCollection services)
        {
            services.AddHttpClient<IApiClient, ApiClient>();
        }
    }
}
