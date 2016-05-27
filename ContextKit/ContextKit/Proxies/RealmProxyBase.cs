using System;
using System.Threading.Tasks;
using CoreLocation;

using Realms;
using System.Linq;

namespace ContextKit
{
	public class RealmProxyBase
	{
		protected Realm realm => Realm.GetInstance ();

		protected EventProvider EventProvider;
		protected LocationProvider LocationProvider;


		public RealmProxyBase (EventProvider eventProvider, LocationProvider locationProvider)
		{
			EventProvider = eventProvider;
			LocationProvider = locationProvider;
		}


		public ContextItem CreateNewContextItem ()
		{
			var context = realm.CreateObject<ContextItem> ();

			context.Id = Guid.NewGuid ().ToString ();

			return context;
		}


		public void AddContextPoint (ContextItem contextItem, CLLocation clLocation, CLPlacemark placemark)
		{
			var location = realm.CreateObject<CkLocation> ();

			location.Floor = Convert.ToInt32 (clLocation.Floor?.Level ?? 0);
			location.Speed = clLocation.Speed;
			location.Course = clLocation.Course;
			location.Altitude = clLocation.Altitude;
			location.Latitude = clLocation.Coordinate.Latitude;
			location.Longitude = clLocation.Coordinate.Longitude;
			location.HorizontalAccuracy = clLocation.HorizontalAccuracy;
			location.VerticalAccuracy = clLocation.VerticalAccuracy;

			location.AdministrativeArea = placemark?.AdministrativeArea;
			location.Country = placemark?.Country;
			location.InlandWater = placemark?.InlandWater;
			location.IsoCountryCode = placemark?.IsoCountryCode;
			location.Locality = placemark?.Locality;
			location.Name = placemark?.Name;
			location.Ocean = placemark?.Ocean;
			location.PostalCode = placemark?.PostalCode;
			location.SubAdministrativeArea = placemark?.SubAdministrativeArea;
			location.SubLocality = placemark?.SubLocality;
			location.SubThoroughfare = placemark?.SubThoroughfare;
			location.Thoroughfare = placemark?.Thoroughfare;


			var contextPoint = realm.CreateObject<ContextPoint> ();

			contextPoint.Location = location;
			contextPoint.RelativeDistanceHolder = contextPoint.GetDistanceHolder (clLocation);
			contextPoint.Timestamp = DateTimeOffset.Now;
			contextPoint.RelativeTimeHolder = 0;

			contextItem.ContextPoints.Add (contextPoint);
		}


		public async Task RefreshContextState ()
		{
			var location = await LocationProvider.GetCurrentLocationAsync ();

			if (location != null) {

				// var placemark = await LocationProvider.ReverseGeocodeLocation (location);

				realm.Refresh ();

				var allContextState = realm.All<ContextState> ();

				var allPoints = realm.All<ContextPoint> ().ToList ();

				RunInTransaction (() => {

					allPoints.SetRelativeDistanceValues (location);

					allPoints.SetRelativeTimeValues ();

					var contextState = allContextState.Any () ? allContextState.First () : realm.CreateObject<ContextState> ();

					contextState.LastRefresh = DateTimeOffset.Now;

					Log ($"ContextPoints Relative Values Updated at {contextState.LastRefresh}");
				});
			}
		}


		public void RunInTransaction (Action action, bool refresh = false)
		{
			if (refresh) realm.Refresh ();

			using (var transaction = realm.BeginWrite ()) {

				try {

					action ();

					transaction.Commit ();

				} catch (Exception ex) {

					System.Diagnostics.Debug.WriteLine (ex.Message);

					transaction.Rollback ();
				}
			}
		}

		bool log = true;

		void Log (string message) { if (log) System.Diagnostics.Debug.WriteLine ($"[RealmProxyBase] {message}"); }
	}
}