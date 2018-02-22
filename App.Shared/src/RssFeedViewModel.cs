using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using App.Model;
using Qoden.Binding;

namespace App.Shared
{
    public class RssFeedViewModel : INotifyPropertyChanged
    {
        private List<RssFeedItem> _feed = new List<RssFeedItem>();

        private string _url;

        public string Url
        {
            set
            {
                _url = value;
                Load(Url);
            }
            get => _url;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RssFeedViewModel()
        {
        }

        public async Task Load(string feedUrl)
        {
            try
            {
                _feed = await RssFeedService.Load(feedUrl);
                FeedLoadingError = "";
            }
            catch (Exception e)
            {
                FeedLoadingError = e.Message;
            }
            finally
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Feed)));
            }
        }

        public IReadOnlyList<RssFeedItem> Feed => _feed;

//        public readonly IProperty FeedProperty = new Property<string>(Feed, "feed");
//        public IProperty<string> UrlProperty = new Property<string>(_url, );
        
        string feedLoadingError;

        public string FeedLoadingError
        {
            get
            {
                return feedLoadingError;
            }

            private set
            {
                feedLoadingError = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FeedLoadingError)));
            }
        }
    }
}
