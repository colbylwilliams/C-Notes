using System;

using Foundation;
using UIKit;

using ContextKit;
using Realms;

namespace CNotes
{
	public partial class CNotesDetailTvc : UITableViewController
	{
		RealmList<ContextPoint> Points => CkContext.Shared.SelectedNote.Context.ContextPoints;

		public CNotesDetailTvc (IntPtr handle) : base (handle) { Title = "History"; }


		public override nint NumberOfSections (UITableView tableView) => 1;


		public override nint RowsInSection (UITableView tableView, nint section) => Points.Count;


		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.Dequeue<CNotesDetailTvCell> (indexPath);

			var point = Points [indexPath.Row];

			var action = (indexPath.Row == 0) ? "Created" : "Updated";

			cell.TextLabel.Text = $"{action} {point.TimestampString}";
			cell.DetailTextLabel.Text = point.Location.CityStateString;// $"{point.TimestampString.PadRight (25)} {point.Location.CityStateString}";

			return cell;
		}
	}
}