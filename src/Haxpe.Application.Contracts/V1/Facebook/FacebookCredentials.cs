using System;
using Newtonsoft.Json;

namespace Haxpe.V1.Facebook
{
    public class FacebookCredentials
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("userID")]
        public string UserId { get; set; }
    }
}
