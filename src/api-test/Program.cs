using BotanIO.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var botan = new Botan("botan-io-token-goes-here");

            TrackHundredUniqueUsers(botan);
            ShortenUrl(botan);
        }

        private static void ShortenUrl(Botan botan)
        {
            botan.ShortenUrl("http://telegram.com", DateTime.UtcNow.Ticks.ToString());
        }

        private static void TrackHundredUniqueUsers(Botan botan)
        {
            for (int i = 0; i < 100; i++)
                botan.Track(
                    "FirstTimer",
                    DateTime.UtcNow.AddDays(-new Random().Next(1, 365)),
                    DateTime.UtcNow.Ticks.ToString()
                );

            Console.WriteLine("All 100 users tracked.");
        }
    }
}
