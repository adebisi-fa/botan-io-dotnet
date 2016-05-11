using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Botan.IO.Api
{
    public class BotanTrackResponse
    {
        public string Status { get; set; }
        public string Information { get; set; }
    }

    public class Botan
    {
        private string _botanToken;
        HttpClient _client;

        public Botan(string botanToken)
        {
            _botanToken = botanToken;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://api.botan.io/");
        }

        public string ShortenUrlAsync(string url, string userIds)
        {
            return _client.GetStringAsync(
                $"s/?token={_botanToken}&url={url}&user_ids={userIds}"
            ).Result;
        }

        public BotanTrackResponse Track (string userId, string eventName, object message)
        {
            if (message == null)
                throw new Exception("Message cannot be null.");

            var messageString = JsonConvert.SerializeObject(message);
            var responseString = _client
                .PostAsync($"track/?token={_botanToken}&uid={userId}&name={eventName}", new StringContent(messageString))
                .Result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<BotanTrackResponse>(responseString);
        }
    }
}
