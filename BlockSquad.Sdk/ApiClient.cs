using BlockSquad.Sdk.API;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlockSquad.Sdk
{
    public class BlockSquadApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BlockSquadApiResponse<T>> GetAsync<T>(string url)
        {
            var apiResponse = new BlockSquadApiResponse<T>();

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.ErrorMessage = $"Request failed with status code: {response.StatusCode}";
                    return apiResponse;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                apiResponse.Data = JsonConvert.DeserializeObject<T>(jsonResponse);
                apiResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return apiResponse;
        }

        public async Task<BlockSquadApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var apiResponse = new BlockSquadApiResponse<TResponse>();

            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.ErrorMessage = $"Request failed with status code: {response.StatusCode}";
                    return apiResponse;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                apiResponse.Data = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
                apiResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return apiResponse;
        }
    }
}
