using System;
using Foundation;
using NewsReader.Model;
using UIKit;


namespace NewsReader.iOS
{
    public class RssTableSource : UITableViewSource
    {
        public RssFeedItem[] TableItems = {};
        
        private const string CellIdentifier = "TableCell";
//
//        public RssTableSource(RssFeedItem[] items)
//        {
//            TableItems = items;
//        }
        

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier);
            var item = TableItems[indexPath.Row];

            if (cell == null)
            { cell = new UITableViewCell(UITableViewCellStyle.Value1, CellIdentifier); }


            cell.TextLabel.Text = item.Title;
            cell.DetailTextLabel.Text = item.Creator;

            return cell;
        }
    }
}
