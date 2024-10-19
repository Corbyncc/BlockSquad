using BlockSquad.Api.Gslt.Models;
using BlockSquad.Shared.Api;

namespace BlockSquad.Api.Gslt;
public class GsltService : IGsltService
{
    private readonly IConfiguration _configuration;
    private readonly IApiClient _apiClient;

    public GsltService(IConfiguration configuration, IApiClient apiClient)
    {
        _configuration = configuration;
        _apiClient = apiClient;
    }

    public async Task<string?> GetGsltAsync(string memo)
    {
        return await CreateAccountAsync(304930, memo);
    }

    private async Task<BlockSquadApiResponse<T>> GetAccountListAsync<T>()
    {
        var url = $"https://api.steampowered.com/IGameServersService/GetAccountList/v1/?key={_configuration["SteamWebApiKey"]}";
        return await _apiClient.GetAsync<T>(url);
    }

    private async Task<string?> CreateAccountAsync(uint appId, string memo)
    {
        var url = $"https://api.steampowered.com/IGameServersService/CreateAccount/v1/";

        var requestData = new CreateAccountRequest
        {
            Key = _configuration["SteamWebApiKey"],
            AppId = appId,
            Memo = memo
        };

        var response = await _apiClient.PostAsync<CreateAccountRequest, SteamGameServersServiceResponse<CreateAccountResponse>>(url, requestData, true);

        if (!response.IsSuccess || response.Data == null)
            return null;

        return response.Data?.Response?.LoginToken;
    }

    private async Task<BlockSquadApiResponse<TResponse>> SetMemoAsync<TResponse>(ulong steamId, string memo)
    {
        var url = $"https://api.steampowered.com/IGameServersService/SetMemo/v1/";
        var requestData = new
        {
            key = _configuration["SteamWebApiKey"],
            steamid = steamId,
            memo
        };
        return await _apiClient.PostAsync<object, TResponse>(url, requestData);
    }

    private async Task<BlockSquadApiResponse<TResponse>> ResetLoginTokenAsync<TResponse>(ulong steamId)
    {
        var url = $"https://api.steampowered.com/IGameServersService/ResetLoginToken/v1/";
        var requestData = new
        {
            key = _configuration["SteamWebApiKey"],
            steamid = steamId
        };
        return await _apiClient.PostAsync<object, TResponse>(url, requestData);
    }

    private async Task<BlockSquadApiResponse<TResponse>> DeleteAccountAsync<TResponse>(ulong steamId)
    {
        var url = $"https://api.steampowered.com/IGameServersService/DeleteAccount/v1/";
        var requestData = new
        {
            key = _configuration["SteamWebApiKey"],
            steamid = steamId
        };
        return await _apiClient.PostAsync<object, TResponse>(url, requestData);
    }

    private async Task<BlockSquadApiResponse<T>> GetAccountPublicInfoAsync<T>(ulong steamId)
    {
        var url = $"https://api.steampowered.com/IGameServersService/GetAccountPublicInfo/v1/?key={_configuration["SteamWebApiKey"]}&steamid={steamId}";
        return await _apiClient.GetAsync<T>(url);
    }

    private async Task<BlockSquadApiResponse<T>> QueryLoginTokenAsync<T>(string loginToken)
    {
        var url = $"https://api.steampowered.com/IGameServersService/QueryLoginToken/v1/?key={_configuration["SteamWebApiKey"]}&login_token={loginToken}";
        return await _apiClient.GetAsync<T>(url);
    }

    private async Task<BlockSquadApiResponse<T>> GetServerSteamIDsByIPAsync<T>(string serverIps)
    {
        var url = $"https://api.steampowered.com/IGameServersService/GetServerSteamIDsByIP/v1/?key={_configuration["SteamWebApiKey"]}&server_ips={serverIps}";
        return await _apiClient.GetAsync<T>(url);
    }

    private async Task<BlockSquadApiResponse<T>> GetServerIPsBySteamIDAsync<T>(ulong serverSteamId)
    {
        var url = $"https://api.steampowered.com/IGameServersService/GetServerIPsBySteamID/v1/?key={_configuration["SteamWebApiKey"]}&server_steamids={serverSteamId}";
        return await _apiClient.GetAsync<T>(url);
    }
}

