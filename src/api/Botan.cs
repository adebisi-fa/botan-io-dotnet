using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace BotanIO.Api
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

        /// <summary>
        /// Shortens a url to https://telgr.me/a-unique-token
        /// </summary>
        /// <param name="url">The original url.</param>
        /// <param name="userIdsSeparatedWithComma">The user (or users for group) meant to click the url.  Multiple values must be separated with a comma, ','.</param>
        /// <returns></returns>
        public string ShortenUrl(string url, string userIdsSeparatedWithComma)
        {
            return _client.GetStringAsync(
                $"s/?token={_botanToken}&url={Uri.EscapeDataString(url)}&user_ids={userIdsSeparatedWithComma}"
            ).Result;
        }

        /// <summary>
        /// Tracks an event along with the metadata stored in message.
        /// </summary>
        /// <param name="eventName">The event name</param>
        /// <param name="message">Event metadata.</param>
        /// <param name="userId">The user who triggers the event.</param>
        /// <returns></returns>
        public BotanTrackResponse Track(string eventName, object message, string userId)
        {
            if (message == null)
                throw new Exception("Message cannot be null.");

            var messageString = JsonConvert.SerializeObject(message);

            var responseString = _client
                .PostAsync($"track?token={_botanToken}&uid={userId}&name={eventName}", new StringContent(messageString))
                .Result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<BotanTrackResponse>(responseString);
        }

        /// <summary>
        /// Tracks a date cohorts (daily, weekly, monthly, and years) for an event.
        /// </summary>
        /// <param name="eventName">The event name.</param>``
        /// <param name="eventDate">The event date.</param>
        /// <param name="userId">The user who triggers the event.</param>
        /// <returns></returns>
        public BotanTrackResponse Track(string eventName, DateTime eventDate, string userId)
        {
            return Track(
                eventName,
                new
                {
                    daily = eventDate.ToString("yyyy-MM-dd"),
                    weekly = eventDate.AddDays(-(int)eventDate.DayOfWeek).ToString("yyyy-MM-dd"),
                    monthly = eventDate.ToString("yyyy-MM"),
                    yearly = eventDate.ToString("yyyy")
                },
                userId
            );
        }
    }
}
