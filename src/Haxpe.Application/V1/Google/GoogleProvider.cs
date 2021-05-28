using System;
using System.Net.Http;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Newtonsoft.Json;

namespace Haxpe.V1.Google
{
    public class GoogleProvider: IGoogleProvider
    {
        private readonly IHttpClientFactory factory;

        public GoogleProvider(IHttpClientFactory factory)
        {
            this.factory = factory;
        }
        public async Task<GoogleCustomerInfo> GetCustomerInfo(GoogleCredentials credentials)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://oauth2.googleapis.com/tokeninfo?id_token={credentials.AccessToken}"
                );
            var customer = this.factory.CreateClient();
            var response = await customer.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<GoogleCustomerInfo>(responseString);
                return res;
            }
            else
            {
                throw new BusinessException("Cannot get customer info from Google");
            }
        }
    }
}
