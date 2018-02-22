using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Model
{
    public class RssFeedItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Excerpt { get; set; }
    }

    public class RssFeedService 
    {
        public static async Task<List<RssFeedItem>> Load(string feedUrl)
        {
            using (var client = new HttpClient())
            {
                var xmlFeed = await client.GetStringAsync(feedUrl);
                var doc = XDocument.Parse(xmlFeed);
                XNamespace dc = "http://purl.org/dc/elements/1.1/";

                var items = (from item in doc.Descendants("item")
                    select new RssFeedItem
                    {
                        Title = item.Element("title").Value,
                        Excerpt = item.Element("pubDate").Value,
                        Author = item.Element(dc + "creator").Value,
                    }).ToArray();

                return new List<RssFeedItem>(items);
            }
        }
    }

    public class MyAppContext
    {
        public static readonly RssFeedService FeedService = new RssFeedService();
    }
}
