using BotanIO.Api;
using System;
using System.Threading.Tasks;

namespace api_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var botan = new Botan("JK-t81LYif9MoqPTyiFKQ2llI-oufur8");

            TrackHundredUniqueUsers(botan).Wait();
            ShortenUrl(botan).Wait();
        }

        private static async Task ShortenUrl(Botan botan)
        {
            var shortenedUrl = await botan.ShortenUrlAsync("http://telegram.com", DateTime.UtcNow.Ticks.ToString());
            Console.WriteLine($"http://telegram.com -> {shortenedUrl}");
        }

        private static async Task TrackHundredUniqueUsers(Botan botan)
        {
            for (int i = 0; i < 100; i++)
                await botan.TrackAsync(
                    "TestAsyncApi",
                    DateTime.UtcNow.AddDays(-new Random().Next(1, 365)),
                    DateTime.UtcNow.Ticks.ToString()
                );

            Console.WriteLine("All 100 users tracked.");
        }
    }
}
