using System;
using App.Shared;
using UIKit;

namespace NewsReader.iOS
{
    public partial class ViewController : UIViewController
    {
        private RssFeedViewModel _viewModel = new RssFeedViewModel();
        private UITableView rssTable;
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            rssTable = new UITableView(View.Bounds);
            string[] tableItems = { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
            rssTable.Source = new RSSTableSource(tableItems);
            Add(rssTable);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
