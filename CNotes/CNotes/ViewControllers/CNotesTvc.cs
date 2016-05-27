using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using ContextKit;

using HockeyApp;

namespace CNotes
{
	public partial class CNotesTvc : UITableViewController
	{
		const string showDetail = "showDetail";


		List<CNote> notes => CkContext.Shared.Notes;


		public CNotesTvc (IntPtr handle) : base (handle) { }


		bool refreshedContext;

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (!refreshedContext) {

				Log ("Refreshing Context");

				activityIndicator.Hidden = false;
				activityIndicator.StartAnimating ();

				Task.Run (async () => {

					await CkContext.Shared.RefreshContextAndSortNotes ();

					BeginInvokeOnMainThread (() => {
						reloadData ();
						activityIndicator.StopAnimating ();
					});

					refreshedContext = true;
				});

			} else {

				reloadData ();
			}
		}


		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == showDetail) {

				var notesVc = (segue.DestinationViewController as UINavigationController)?.TopViewController as CNotesVc;

				if (notesVc != null) {

					var cell = sender as UITableViewCell;

					if (cell != null) {

						var index = TableView.IndexPathForCell (cell).Row;

						var note = notes [index];

						CkContext.Shared.SelectedNote = note;

						notesVc.Title = note.Title;

						BITHockeyManager.SharedHockeyManager.MetricsManager.TrackEvent (string.Format (HockeyEventIds.OpenedNoteFmt, index));

						Log ($"Set Note Selected: {note}");
					}
				}
			}

			base.PrepareForSegue (segue, sender);
		}


		public override nint NumberOfSections (UITableView tableView) => 1;


		public override nint RowsInSection (UITableView tableView, nint section) => notes.Count;


		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.Dequeue<CNotesTvCell> (indexPath);

			var note = notes [indexPath.Row];

			cell.SetData (note);

			return cell;
		}


		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) {

				var note = notes [indexPath.Row];

				CkContext.Shared.DeleteNote (note);

				tableView.DeleteRows (new [] { indexPath }, UITableViewRowAnimation.Automatic);
			}
		}


		partial void editClicked (NSObject sender)
		{
			BITHockeyManager.SharedHockeyManager.MetricsManager.TrackEvent (HockeyEventIds.SelectedEditNotes);

			//CkContext.Shared.PrintEvents ();
		}


		partial void composeClicked (NSObject sender)
		{
			BITHockeyManager.SharedHockeyManager.MetricsManager.TrackEvent (HockeyEventIds.SelectedNewNote);

			CkContext.Shared.CreateNewNote ();

			reloadData ();

			Log ($"Notes.Count (B) {notes.Count}");

			Task.Run (async () => {

				await CkContext.Shared.AddContextPoint ();

				Log ($"Notes.Count (C) {notes.Count}");

				BeginInvokeOnMainThread (() => reloadData ());
			});

			PerformSegue (showDetail, sender);
		}


		void reloadData ()
		{
			Log ("ReloadData");

			noteCountLabel.Title = $"{notes.Count} Notes";
			TableView.ReloadData ();
		}


		bool log = true;

		void Log (string message) { if (log) System.Diagnostics.Debug.WriteLine ($"[CNotesTvc] {message}"); }
	}
}
