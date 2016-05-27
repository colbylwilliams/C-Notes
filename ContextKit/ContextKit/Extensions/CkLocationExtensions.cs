using CoreLocation;

namespace ContextKit
{
	public static class CkLocationExtensions
	{
		public static double RadiusDistance (this CkLocation pointLocation, CLLocation currentLocation)
			=> GeographyUtility.RadiusDistance (currentLocation.Coordinate.Latitude,
												currentLocation.Coordinate.Longitude,
												pointLocation.Latitude, pointLocation.Longitude,
												GeographyUtility.DistanceUnit.Kilometers);
	}
}