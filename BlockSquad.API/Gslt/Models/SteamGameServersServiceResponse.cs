using Newtonsoft.Json;

namespace BlockSquad.Api.Gslt.Models
{
    public class SteamGameServersServiceResponse<T>
    {
        [JsonProperty("response")]
        public T? Response { get; set; }
    }
}
