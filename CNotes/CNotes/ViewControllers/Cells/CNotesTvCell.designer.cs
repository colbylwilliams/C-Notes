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
	[Register ("CNotesTvCell")]
	partial class CNotesTvCell
	{
		[Outlet]
		UIKit.UILabel noteAuxtitle { get; set; }

		[Outlet]
		UIKit.UILabel noteSubtitle { get; set; }

		[Outlet]
		UIKit.UILabel noteTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (noteTitle != null) {
				noteTitle.Dispose ();
				noteTitle = null;
			}

			if (noteSubtitle != null) {
				noteSubtitle.Dispose ();
				noteSubtitle = null;
			}

			if (noteAuxtitle != null) {
				noteAuxtitle.Dispose ();
				noteAuxtitle = null;
			}
		}
	}
}
