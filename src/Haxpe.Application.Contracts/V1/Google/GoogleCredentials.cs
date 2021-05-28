using Newtonsoft.Json;

namespace Haxpe.V1.Google
{
    public class GoogleCredentials
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}