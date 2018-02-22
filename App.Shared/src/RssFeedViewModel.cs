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

        private string _searchStr;

        public string SearchStr
        {
            set
            {
                _searchStr = value;
                Load(SearchStr);
            }
            get => _searchStr;
        }

        public IProperty<string> SearchStrProperty;

        public event PropertyChangedEventHandler PropertyChanged;

        public RssFeedViewModel()
        {
            SearchStrProperty  = new Property<string>(this, "SearchStr");
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
