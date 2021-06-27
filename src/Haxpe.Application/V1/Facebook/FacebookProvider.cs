using System;
using System.Net.Http;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Newtonsoft.Json;

namespace Haxpe.V1.Facebook
{
    public class FacebookProvider: IFacebookProvider
    {
        private readonly IHttpClientFactory factory;

        public FacebookProvider(IHttpClientFactory factory)
        {
            this.factory = factory;
        }
        public async Task<FacebookCustomerInfo> GetCustomerInfo(FacebookCredentials credentials)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://graph.facebook.com/{credentials.UserId}?fields=email,last_name,name,first_name&access_token={credentials.AccessToken}"
                );
            var client = this.factory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<FacebookCustomerInfo>(responseString);
                return res;
            }
            else
            {
                throw new BusinessException("Cannot get customer info from Facebook");
            }
        }
    }
}
