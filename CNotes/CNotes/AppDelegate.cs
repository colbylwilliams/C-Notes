using System.Linq;

using Foundation;
using UIKit;

using ContextKit;
using HockeyApp;

namespace CNotes
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }


		public AppDelegate ()
		{
			Settings.RegisterDefaultSettings ();
		}


		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			UIBarButtonItem.AppearanceWhenContainedIn (typeof (UIToolbar)).SetTitleTextAttributes (new UITextAttributes { Font = UIFont.PreferredFootnote }, UIControlState.Normal);


			#region Configure HockeyApp

			var manager = BITHockeyManager.SharedHockeyManager;

			manager.Configure (ApiKeys.HockeyAppKey);

			manager.DisableUpdateManager = true;

			manager.StartManager ();

			#endregion


			#region Configure UISplitViewController

			var splitViewController = Window.RootViewController as UISplitViewController;

			var navigationController = splitViewController.ViewControllers.LastOrDefault () as UINavigationController;

			navigationController.TopViewController.NavigationItem.LeftBarButtonItem = splitViewController.DisplayModeButtonItem;

			splitViewController.WeakDelegate = this;

			#endregion

			return true;
		}


		#region IUISplitViewControllerDelegate

		[Export ("splitViewController:collapseSecondaryViewController:ontoPrimaryViewController:")]
		public bool CollapseSecondViewController (UISplitViewController splitViewController, UIViewController secondaryViewController, UIViewController primaryViewController)
		{
			var navController = secondaryViewController as UINavigationController;

			var notesVc = navController?.TopViewController as CNotesVc;

			return (notesVc != null) && CkContext.Shared.SelectedNote == null;
		}

		#endregion
	}
}