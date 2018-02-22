using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsReader.Model
{
    public class RssFeedItem
    {
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Link { get; set; }
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

                return (from item in doc.Descendants("item")
                    select new RssFeedItem
                    {
                        Title = item.Element("title").Value,
                        Link = item.Element("link").Value,
                        
                        Creator = item.Element(dc + "creator").Value,
                    }).ToList();
            }
        }

        public static List<RssFeedItem> Filter(string feedUrl, List<RssFeedItem> feedItems)
        {
            return feedItems.Where(feedItem => feedItem.Title.ToLower().Contains(feedUrl.ToLower())).ToList();
        }
    }
    

    public class MyAppContext
    {
        public static readonly RssFeedService FeedService = new RssFeedService();
    }
}
