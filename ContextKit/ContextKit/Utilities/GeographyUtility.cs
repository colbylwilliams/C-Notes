using System;

namespace ContextKit
{
	public static class GeographyUtility
	{
		public const double EarthSphereRadiusKilometers = 6371;

		public const double EarthSphereRadiusMiles = 3959;

		public enum DistanceUnit
		{
			Miles,
			Kilometers
		}

		/// <summary>
		/// Gets the distance from one coordinate to another.
		/// </summary>
		/// <param name="aLat">The first coordinate's Latitude to find the distance between, in degrees.</param>
		/// <param name="aLon">The first coordinate's Longitude to find the distance between, in degrees.</param>
		/// <param name="bLat">The second coordinate's Latitude to find the distance between, in degrees.</param>
		/// <param name="bLon">The second coordinate's Longitude to find the distance between, in degrees.</param>
		/// <param name="unit">The unit to return the distance.</param>
		/// <returns>The diatance from one coordinate to another.</returns>
		public static double RadiusDistance (double aLat, double aLon, double bLat, double bLon, DistanceUnit unit)
		{
			// convert the coordinates to radians so we can do some calculating.
			var aLatR = aLat.ToRadians ();
			var bLatR = bLat.ToRadians ();

			var dLatR = (aLat - bLat).ToRadians ();
			var dLonR = (aLon - bLon).ToRadians ();

			return Math.Round (2 * Math.Asin (Math.Sqrt (
				Math.Pow (Math.Sin (dLatR / 2), 2) +
				Math.Cos (aLatR) * Math.Cos (bLatR) *
				Math.Pow (Math.Sin (dLonR / 2), 2))) *
			(unit == DistanceUnit.Miles ? EarthSphereRadiusMiles : EarthSphereRadiusKilometers), 4);
		}
	}
}