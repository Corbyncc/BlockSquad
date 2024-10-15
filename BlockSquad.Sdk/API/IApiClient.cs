using System.Threading.Tasks;

namespace BlockSquad.Sdk.API
{
    public interface IApiClient
    {
        Task<BlockSquadApiResponse<T>> GetAsync<T>(string url);

        Task<BlockSquadApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data);
    }
}
