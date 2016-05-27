using Foundation;
using UIKit;

namespace CNotes
{
	public static class UIExtensions
	{
		public static T Dequeue<T> (this UITableView tableView, NSIndexPath indexPath)
			where T : UITableViewCell
		{
			return tableView.DequeueReusableCell (typeof (T).Name, indexPath) as T;
		}
	}
}