using System;
using System.Linq;
using App.Shared;
using CoreGraphics;
using Qoden.Binding;
using Qoden.UI.Wrappers;
using UIKit;

namespace NewsReader.iOS
{
    public partial class MainViewController : UIViewController
    {
        private static readonly UIColor LightGray = UIColor.FromRGB(230, 230, 230);
        
        private readonly RssFeedViewModel _viewModel = new RssFeedViewModel();
        private UITableView _rssTable;
        

        private TextField _feedSearchTextField;
//        private UIButton _button;
        private RssTableSource _tableSource;
        
        private readonly BindingList _bindingList = new BindingList();

        protected MainViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bounds = View.Bounds;
            _rssTable = new UITableView(new CGRect(0, 150, bounds.Width, bounds.Height - 100));

            var innerTextField =
                new UITextField(new CGRect(bounds.Width * 0.1, 60, bounds.Width * 0.8, bounds.Width * 0.1))
                {
                    BackgroundColor = LightGray
                };
            _feedSearchTextField = new TextField(innerTextField);
            
//            _button = new UIButton(new CGRect(bounds.Width * 0.1, 100, bounds.Width * 0.8, bounds.Width * 0.1));
//            _button.SetTitle("refresh", UIControlState.Normal);
//            _button.BackgroundColor = LightGray;

            _viewModel.FeedFiltered += feed =>
            {
                _tableSource.TableItems = feed.ToArray();
                _rssTable.ReloadData();
            };

            _tableSource = new RssTableSource();
            _rssTable.Source = _tableSource;


            Add(_feedSearchTextField);
            Add(_rssTable);
//            Add(_button);

            _bindingList.Property(_viewModel, _ => _.SearchStr)
                .To(_feedSearchTextField.TextProperty())
                .UpdateTarget((t, s) => _feedSearchTextField.SetText((string) t.Target.Value));

            _bindingList.Bind();

            await _viewModel.Load("https://habrahabr.ru/rss/hubs/all");
            _tableSource.TableItems = _viewModel.Feed.ToArray();
            _rssTable.ReloadData();
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            _bindingList.Unbind();
        }
    }
}