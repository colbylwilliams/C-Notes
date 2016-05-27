using System;

namespace ContextKit
{
	public static class UomExtensions
	{
		/// <summary>
		/// Converts a Pressure value from Millibars to Inches of Mercury.
		/// </summary>
		/// <returns>The Pressure in Inches of Mercury.</returns>
		/// <param name="p">Pressure in Millibars.</param>
		public static double MillibarsToInchesOfMercury (this double p)
		{
			return p * 0.0295333727;
		}

		/// <summary>
		/// Converts a Pressure value from Inches of Mercury to Millibars.
		/// </summary>
		/// <returns>The Pressure in Millibars.</returns>
		/// <param name="p">Pressure in Inches of Mercury.</param>
		public static double InchesOfMercuryToMillibars (this double p)
		{
			return p * 33.86;
		}

		/// <summary>
		/// Converts a Temperature value from Fahrenheit to Celsiuses.
		/// </summary>
		/// <returns>The Temperature in Celsiuses.</returns>
		/// <param name="f">Temperature in Fahrenheit.</param>
		public static double FahrenheitToCelsius (this double f)
		{
			return (f - 32) / 1.8;
		}

		/// <summary>
		/// Converts a Temperature value from Celsiuses to Fahrenheit.
		/// </summary>
		/// <returns>The Temperature in Fahrenheit.</returns>
		/// <param name="c">Temperature in Celsiuses.</param>
		public static double CelsiusesToFahrenheit (this double c)
		{
			return (c * 1.8) + 32;
		}

		/// <summary>
		/// Converts length value from kilometers to miles.
		/// </summary>
		/// <returns>The length value in miles.</returns>
		/// <param name="k">The length value in kilometers.</param>
		public static double KilometersToMiles (this double k)
		{
			return k * 0.621371;
		}

		/// <summary>
		/// Converts Speed value from Meters per Second to Miles per Hour.
		/// </summary>
		/// <returns>The Speed in Miles per Hour.</returns>
		/// <param name="mps">The Speed in Meters per Second.</param>
		public static double MpsToMph (this double mps)
		{
			return mps.MpsToKph ().KphToMph ();
		}

		/// <summary>
		/// Converts Speed value from Meters per Second to Kilometers per Hour.
		/// </summary>
		/// <returns>The Speed in Kilometers per Hour.</returns>
		/// <param name="mps">The Speed in Meters per Second.</param>
		public static double MpsToKph (this double mps)
		{
			return mps * 3.6;
		}

		/// <summary>
		/// Converts Speed value from Meters per Second to Knots.
		/// </summary>
		/// <returns>The Speed in Knots.</returns>
		/// <param name="mps">The Speed in Meters per Second.</param>
		public static double MpsToKnots (this double mps)
		{
			return mps * 1.943844;
		}

		/// <summary>
		/// Converts Speed value from Kilometers per Hour to Miles per Hour.
		/// </summary>
		/// <returns>The Speed in Miles per Hour.</returns>
		/// <param name="kph">The Speed in Kilometers per Hour.</param>
		public static double KphToMph (this double kph)
		{
			return kph * 0.621371;
		}

		/// <summary>
		/// Converts Speed value from Kilometers per Hour to Knots.
		/// </summary>
		/// <returns>The Speed in Knots.</returns>
		/// <param name="kph">The Speed in Kilometers per Hour.</param>
		public static double KphToKnots (this double kph)
		{
			return kph * 0.539957;
		}

		/// <summary>
		/// Converts Degrees to Radians
		/// </summary>
		/// <returns>Radians.</returns>
		/// <param name="d">Degrees</param>
		public static double ToRadians (this double d)
		{
			return d * (Math.PI / 180);
		}

		/// <summary>
		/// Converts Degrees to Radians
		/// </summary>
		/// <returns>Radians.</returns>
		/// <param name="d">Degrees</param>
		public static double ToRadians (this float d)
		{
			return d * (Math.PI / 180);
		}

		/// <summary>
		/// Converts Degrees to Radians
		/// </summary>
		/// <returns>Radians.</returns>
		/// <param name="d">Degrees</param>
		public static double ToRadians (this int d)
		{
			return d * (Math.PI / 180);
		}

		/// <summary>
		/// Converts value from Feet to Meters.
		/// </summary>
		/// <returns>The value in Meters.</returns>
		/// <param name="f">The value in Feet</param>
		public static double FeetToMeters (this double f)
		{
			return f / 3.28084;
		}

		/// <summary>
		/// Converts value from Meters to Feet.
		/// </summary>
		/// <returns>The value in Feet.</returns>
		/// <param name="m">The value in Meters</param>
		public static double MetersToFeet (this double m)
		{
			return m * 3.28084;
		}

		//public static string ParseLatitude (this double lat)
		//{
		//	string direction = lat < 0 ? "s" : "n";

		//	lat = Math.Abs (lat);

		//	double degrees = Math.Truncate (lat);

		//	lat = ((lat - degrees) * 60) * 1000;

		//	double minutes = Math.Truncate (lat) / 1000;

		//	return string.Format ("{0}° {1}{2}", degrees, minutes.ToString ("00.000"), direction);
		//}

		//public static string ParseLongitude (this double lon)
		//{
		//	string direction = lon < 0 ? "w" : "e";

		//	lon = Math.Abs (lon);

		//	double degrees = Math.Truncate (lon);

		//	lon = ((lon - degrees) * 60) * 1000;

		//	double minutes = Math.Truncate (lon) / 1000;

		//	return string.Format ("{0}° {1}{2}", degrees, minutes.ToString ("00.000"), direction);
		//}

		public static string ParseLatitude (this double lat)
		{
			string direction = lat < 0 ? "S" : "N";

			lat = Math.Abs (lat);

			double degrees = Math.Truncate (lat);

			lat = (lat - degrees) * 60;

			double minutes = Math.Truncate (lat);

			lat = ((lat - minutes) * 60) * 1000;

			double seconds = Math.Truncate (lat) / 1000;

			return $"{degrees}°{minutes}\'{seconds}\" {direction}";
		}

		public static string ParseLongitude (this double lon)
		{
			string direction = lon < 0 ? "W" : "E";

			lon = Math.Abs (lon);

			double degrees = Math.Truncate (lon);

			lon = (lon - degrees) * 60;

			double minutes = Math.Truncate (lon);

			lon = ((lon - minutes) * 60) * 1000;

			double seconds = Math.Truncate (lon) / 1000;

			return $"{degrees}°{minutes}\'{seconds}\" {direction}";
		}
	}
}