using System;

using Foundation;
using UIKit;

using ContextKit;
using HockeyApp;

namespace CNotes
{
	public partial class CNotesVc : UIViewController, IUITextFieldDelegate, IUITextViewDelegate
	{
		CNote Note => CkContext.Shared.SelectedNote;

		public CNotesVc (IntPtr handle) : base (handle) { }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = Note.HasTitle ? Note.Title : "Untitled";

			titleTextField.Text = Note.Title;
			noteTextView.Text = Note.Note;
		}


		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (!titleTextField.HasText) {

				titleTextField.BecomeFirstResponder ();

			} else {

				noteTextView.BecomeFirstResponder ();
			}

			actionButton.Enabled = Note.HasNote;
		}


		#region IUITextFieldDelegate


		NSObject textFieldNotification;


		[Export ("textFieldShouldBeginEditing:")]
		public bool ShouldBeginEditing (UITextField textField)
		{
			textFieldNotification = UITextField.Notifications.ObserveTextFieldTextDidChange (handleTextFieldChangeNotification);

			return true;
		}


		[Export ("textFieldShouldEndEditing:")]
		public bool ShouldEndEditing (UITextField textField)
		{
			// To stop listening:
			textFieldNotification.Dispose ();

			if (textField.Text != Note.Title) CkContext.Shared.UpdateNoteTitle (textField.Text);

			return true;
		}


		[Export ("textFieldShouldReturn:")]
		public bool ShouldReturn (UITextField textField)
		{
			noteTextView.BecomeFirstResponder ();

			return true;
		}


		void handleTextFieldChangeNotification (object sender, NSNotificationEventArgs args)
		{
			var textField = args.Notification?.Object as UITextField;

			Title = string.IsNullOrEmpty (textField?.Text) ? "Untitled" : textField.Text;

			Log ($"Object: {args.Notification.Object}");
		}

		#endregion



		#region IUITextViewDelegate


		[Export ("textViewShouldEndEditing:")]
		public bool ShouldEndEditing (UITextView textView)
		{
			if (textView.Text != Note.Note) CkContext.Shared.UpdateNote (textView.Text);

			return true;
		}


		[Export ("textViewDidChange:")]
		public void Changed (UITextView textView) => actionButton.Enabled = !string.IsNullOrWhiteSpace (textView?.Text);


		#endregion


		partial void actionClicked (NSObject sender)
		{
			BITHockeyManager.SharedHockeyManager.MetricsManager.TrackEvent (HockeyEventIds.SelectedShareNote);

			var firstActivityItem = new NSString ($"{Title}\n--\n{noteTextView.Text}");

			var activityViewController = new UIActivityViewController (new [] { firstActivityItem }, new UIActivity [] { });

			activityViewController.ExcludedActivityTypes = new [] {
					UIActivityType.PostToWeibo,
					UIActivityType.AddToReadingList,
					UIActivityType.PostToVimeo,
					UIActivityType.PostToTencentWeibo,
					UIActivityType.SaveToCameraRoll
				};


			activityViewController.CompletionWithItemsHandler = (NSString activityType, bool completed, NSExtensionItem [] returnedItems, NSError error) => {

				Log ($"ActivityViewController CompletionWithItems\n  activityType: {activityType}\n  completed: {completed}\n  returnedItems: {returnedItems}\n  error: {error}");

				BITHockeyManager.SharedHockeyManager.MetricsManager.TrackEvent (string.Format (HockeyEventIds.SharedNoteFmt, completed ? activityType.ToString () : "incomplete"));
			};


			// set the action but as the popover's anchor for ipad
			if (activityViewController.PopoverPresentationController != null)
				activityViewController.PopoverPresentationController.BarButtonItem = sender as UIBarButtonItem;

			NavigationController.PresentViewController (activityViewController, true, null);
		}


		bool log = true;

		void Log (string message) { if (log) System.Diagnostics.Debug.WriteLine ($"[CNotesVc] {message}"); }
	}
}
