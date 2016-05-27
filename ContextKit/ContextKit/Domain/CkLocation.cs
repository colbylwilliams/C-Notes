using Realms;

namespace ContextKit
{
	public class CkLocation : RealmObject
	{
		public int Floor { get; set; }

		public double Speed { get; set; }

		public double Course { get; set; }

		public double Altitude { get; set; }

		// [Indexed] double not supported 
		public double Latitude { get; set; }

		// [Indexed] double not supported 
		public double Longitude { get; set; }

		public double VerticalAccuracy { get; set; }

		public double HorizontalAccuracy { get; set; }


		public string AdministrativeArea { get; set; }

		public string Country { get; set; }

		public string InlandWater { get; set; }

		public string IsoCountryCode { get; set; }

		public string Locality { get; set; }

		public string Name { get; set; }

		public string Ocean { get; set; }

		public string PostalCode { get; set; }

		public string SubAdministrativeArea { get; set; }

		public string SubLocality { get; set; }

		public string SubThoroughfare { get; set; }

		public string Thoroughfare { get; set; }

		[Ignored]
		public string CoordinatesString => $"{Latitude.ParseLatitude ()} {Longitude.ParseLongitude ()}";

		[Ignored]
		public string CityStateString => $"{Locality}, {AdministrativeArea}";
	}
}