using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using App.Model;

namespace App.Shared
{
    public class RssFeedViewModel : INotifyPropertyChanged
    {
        private List<RssFeedItem> _feed = new List<RssFeedItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        public RssFeedViewModel()
        {
        }

        public async Task Load()
        {
            try
            {
                _feed = await MyAppContext.FeedService.Load();
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
