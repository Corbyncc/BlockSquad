using System.Threading.Tasks;

namespace BlockSquad.Shared.Api
{
    public interface IApiClient
    {
        Task<BlockSquadApiResponse<T>> GetAsync<T>(string url);

        Task<BlockSquadApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data, bool applyAsQueryParams = false);
    }
}
