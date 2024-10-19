using Newtonsoft.Json;

namespace BlockSquad.Api.Gslt.Models
{
    public class CreateAccountResponse
    {
        [JsonProperty("steamid")]
        public ulong? SteamId { get; set; }

        [JsonProperty("login_Token")]
        public string? LoginToken { get; set; }
    }
}
