using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NewsReader.iOS.Annotations;
using NewsReader.Model;

namespace App.Shared
{
    public class RssFeedViewModel : INotifyPropertyChanged
    {
        private List<RssFeedItem> _feed = new List<RssFeedItem>();

        private string _searchStr = "";

        public delegate void FeedChanged(List<RssFeedItem> feeds);
        public event FeedChanged FeedFiltered;
        
        public string SearchStr
        {
            set
            {
                _searchStr = value;
                Refresh();
            }
            get => _searchStr;
        }
        
//        public event PropertyChangedEventHandler PropertyChanged;

        private void Refresh()
        {
            FeedFiltered?.Invoke(RssFeedService.Filter(SearchStr, _feed));
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

        private string _feedLoadingError;
        public string FeedLoadingError
        {
            get => _feedLoadingError;

            private set
            {
                _feedLoadingError = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FeedLoadingError)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
