using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Events
{
    public class EventEmitter : IEventEmitter
    {
        private readonly CentrifugoSettings settings;
        private readonly IHttpClientFactory factory;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public EventEmitter(CentrifugoSettings settings, IHttpClientFactory factory)
        {
            this.settings = settings;
            this.factory = factory;
            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            this.jsonSerializerSettings.Converters.Add(new StringEnumConverter
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            });
        }

        public async Task SendEvent<TEvent>(Guid userId, IEventModel<TEvent> @event)
        {
            var body = new PublishModel<IEventModel<TEvent>>();

            body.Params = new PublishPropsModel<IEventModel<TEvent>>()
            {
                Channel = $"events#{userId}",
                Data = @event
            };

            var content = JsonConvert.SerializeObject(body, this.jsonSerializerSettings);
            var bodyString = new StringContent(
                content,
                Encoding.UTF8,
                "application/json");

            var client = this.factory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"apikey {this.settings.ApiKey}");
            var response = await client.PostAsync(this.settings.Url, bodyString);

            if (response.IsSuccessStatusCode)
            {
                Log.Logger.Information($"Event was sent: ${content}");
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Log.Logger.Error($"Can't send event: ${responseString}");
            }
        }
    }
}
