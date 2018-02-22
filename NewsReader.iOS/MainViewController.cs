﻿using System;
using System.Linq;
using App.Model;
using App.Shared;
using CoreGraphics;
using Qoden.Binding;
using Qoden.UI;
using Qoden.UI.Wrappers;
using UIKit;

namespace NewsReader.iOS
{
    public partial class MainViewController : UIViewController
    {
        private RssFeedViewModel _viewModel = new RssFeedViewModel();
        private UITableView _rssTable;
        private UITextField _feedUrlTextField;
//        private BindingList _bindingList;

        protected MainViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bounds = View.Bounds;
            _rssTable = new UITableView(new CGRect(0, 100, bounds.Width, bounds.Height - 100));
            _feedUrlTextField = new UITextField(new CGRect(bounds.Width * 0.1, 40, bounds.Width * 0.9, 30));
            _feedUrlTextField.BackgroundColor = UIColor.Magenta;
            
            RssFeedItem[] items =
                {new RssFeedItem {Author = "Evgeny", Title = "What is it that you talking about so much, Evgeny"}};
            var tableSource = new RssTableSource(items);
            _rssTable.Source = tableSource;
            
            
            
            Add(_feedUrlTextField);
            Add(_rssTable);

            await _viewModel.Load("https://habrahabr.ru/rss/hubs/all");
            tableSource.TableItems = _viewModel.Feed.ToArray();
            _rssTable.ReloadData();
            
//            var urlFeedBinding = new PropertyBinding
//            {
//                Source = p,
//                Target = tableSource.TableItems
//            };

//            PropertyBindingBuilder
//                .Create<string, string>(_feedUrlTextField.Text, _ => _)
//                .To(_viewModel.Property);



            
            
//            _bindingList.Bind();
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
//            _bindingList.Unbind();
        }
    }
}