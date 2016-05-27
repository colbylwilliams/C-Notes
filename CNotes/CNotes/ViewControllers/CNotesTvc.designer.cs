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
	[Register ("CNotesTvc")]
	partial class CNotesTvc
	{
		[Outlet]
		UIKit.UIActivityIndicatorView activityIndicator { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem noteCountLabel { get; set; }

		[Action ("composeClicked:")]
		partial void composeClicked (Foundation.NSObject sender);

		[Action ("editClicked:")]
		partial void editClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (activityIndicator != null) {
				activityIndicator.Dispose ();
				activityIndicator = null;
			}

			if (noteCountLabel != null) {
				noteCountLabel.Dispose ();
				noteCountLabel = null;
			}
		}
	}
}
