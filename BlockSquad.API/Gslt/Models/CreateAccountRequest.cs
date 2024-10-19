using Newtonsoft.Json;

namespace BlockSquad.Api.Gslt.Models
{
    public class CreateAccountRequest
    {
        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("appid")]
        public uint? AppId { get; set; }

        [JsonProperty("memo")]
        public string? Memo { get; set; }
    }
}
