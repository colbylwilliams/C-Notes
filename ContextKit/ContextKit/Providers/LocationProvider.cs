using System;
using System.Linq;
using System.Threading.Tasks;

using CoreLocation;

namespace ContextKit
{
	public class LocationProvider
	{
		TaskCompletionSource<CLLocation> ClLocationTcs;
		TaskCompletionSource<CLAuthorizationStatus> ClAuthTcs;


		readonly CLLocationManager ClLocationManager = new CLLocationManager { DesiredAccuracy = CLLocation.AccuracyBest, DistanceFilter = 25 /*meters*/};


		public CLLocation Location { get; set; }


		public LocationProvider ()
		{
			ClLocationManager.ShouldDisplayHeadingCalibration += m => false;
		}


		public async Task<CLLocation> GetCurrentLocationAsync ()
		{
			Log ("GetCurrentLocationAsync");

			if (ClLocationTcs == null || (int)ClLocationTcs.Task.Status >= 5) {
				ClLocationTcs = new TaskCompletionSource<CLLocation> ();
			} else if ((int)ClLocationTcs.Task.Status <= 4) {
				return await ClLocationTcs.Task;
			}

			var status = CLLocationManager.Status;

			if (status == CLAuthorizationStatus.NotDetermined) status = await getAuthorizationStatusAsync ();


			if (status == CLAuthorizationStatus.AuthorizedWhenInUse && CLLocationManager.LocationServicesEnabled) {

				Log ("Authorized");

				ClLocationManager.LocationsUpdated += handleLocationsUpdated;

				ClLocationManager.StartUpdatingLocation ();

				Log ("StartUpdatingLocation");

			} else {

				Log ("Not Authorized");

				ClLocationTcs.SetResult (null);
			}

			return await ClLocationTcs.Task;
		}


		Task<CLAuthorizationStatus> getAuthorizationStatusAsync ()
		{
			if (ClAuthTcs == null || (int)ClAuthTcs.Task.Status >= 5) {
				ClAuthTcs = new TaskCompletionSource<CLAuthorizationStatus> ();
			} else if ((int)ClAuthTcs.Task.Status <= 4) {
				return ClAuthTcs.Task;
			}

			ClLocationManager.AuthorizationChanged += handleAuthorizationChanged;

			ClLocationManager.RequestWhenInUseAuthorization ();

			return ClAuthTcs.Task;
		}


		void handleLocationsUpdated (object sender, CLLocationsUpdatedEventArgs e)
		{
			Log ("handleLocationsUpdated");

			// If it's a relatively recent event, turn off updates to save power.
			var location = e.Locations.LastOrDefault ();

			if (location != null) {

				var timestamp = location.Timestamp.ToDateTime ();

				var timedelta = DateTime.UtcNow.Subtract (timestamp);

				// location was retrieved less than 5 seconds ago
				if (Math.Abs (timedelta.TotalSeconds) < 5) {

					ClLocationManager.LocationsUpdated -= handleLocationsUpdated;

					Location = location;

					ClLocationManager.StopUpdatingLocation ();

					Log ("StopUpdatingLocation");

					if (!ClLocationTcs.TrySetResult (location)) {

						Log ("ClLocationTcs Failed to Set Result");

					} else {

						Log ("ClLocationTcs Set Result");
					}

				} else {

					Log ($"Location was too old: ({timedelta}s)");
				}
			}
		}


		void handleAuthorizationChanged (object sender, CLAuthorizationChangedEventArgs e)
		{
			Log ("handleAuthorizationChanged");

			ClLocationManager.AuthorizationChanged -= handleAuthorizationChanged;

			if (!ClAuthTcs.TrySetResult (e.Status)) {
				Log ("ClAuthTcs Failed to Set Result");
			} else {
				Log ("ClAuthTcs Set Result");
			}
		}


		public async Task<CLPlacemark> ReverseGeocodeLocation (CLLocation location)
		{
			var geocoder = new CLGeocoder ();

			var placemarks = await geocoder.ReverseGeocodeLocationAsync (location);

			return placemarks?.FirstOrDefault ();
		}


		bool log = true;

		void Log (string message) { if (log) System.Diagnostics.Debug.WriteLine ($"[LocationProvider] {message}"); }
	}
}