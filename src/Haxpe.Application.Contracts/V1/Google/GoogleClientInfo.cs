using Newtonsoft.Json;

namespace Haxpe.V1.Google
{
    public class GoogleCustomerInfo
    {
        [JsonProperty("sub")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string FirstName { get; set; }

        [JsonProperty("family_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}