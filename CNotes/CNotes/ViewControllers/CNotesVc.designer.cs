// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CNotes
{
	[Register ("CNotesVc")]
	partial class CNotesVc
	{
		[Outlet]
		UIKit.UIBarButtonItem actionButton { get; set; }

		[Outlet]
		UIKit.UITextView noteTextView { get; set; }

		[Outlet]
		UIKit.UITextField titleTextField { get; set; }

		[Action ("actionClicked:")]
		partial void actionClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (noteTextView != null) {
				noteTextView.Dispose ();
				noteTextView = null;
			}

			if (titleTextField != null) {
				titleTextField.Dispose ();
				titleTextField = null;
			}

			if (actionButton != null) {
				actionButton.Dispose ();
				actionButton = null;
			}
		}
	}
}
