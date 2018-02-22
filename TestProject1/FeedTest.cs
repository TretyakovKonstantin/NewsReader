using System;
using App.Model;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var service = new RssFeedService();
            var feeds = await RssFeedService.Load("https://habrahabr.ru/rss/hubs/all");
            try
            {
                Console.WriteLine(feeds[1]);
                Assert.NotNull(feeds[0]);
            }
            catch (Exception e)
            {
                Console.Write("You fucked up. No feeds available");
            }
        }
    }
}