using System.Collections.Generic;
using CoreLocation;
using System;

namespace ContextKit
{
	public static class ContextPointExtensions
	{
		const double MaxDistance = 99999;

		public static double GetDistanceHolder (this ContextPoint contextPoint, CLLocation currentLocation)
			=> contextPoint.HasLocation ? contextPoint.Location.RadiusDistance (currentLocation) : MaxDistance;


		public static void SetRelativeDistanceValues (this List<ContextPoint> contextPoints, CLLocation currentLocation)
		{
			foreach (var point in contextPoints) point.RelativeDistanceHolder = point.GetDistanceHolder (currentLocation);

			contextPoints.Sort ((x, y) => x.RelativeDistanceHolder.CompareTo (y.RelativeDistanceHolder));

			//foreach (var point in contextPoints) System.Diagnostics.Debug.WriteLine (point.RelativeDistanceHolder);

			//for (int i = 1; i < contextPoints.Count; i++) contextPoints [(i - 1)].RelativeDistanceOrder = i;
		}


		public static int GetTimeHolder (this ContextPoint contextPoint, DateTimeOffset dateTimeNow)
		{
			var timeStamp = contextPoint.Timestamp;

			var relativeValue = 0;

			relativeValue += Math.Abs ((int)dateTimeNow.DayOfWeek - (int)timeStamp.DayOfWeek) * 10;

			relativeValue += Math.Abs (dateTimeNow.Hour - timeStamp.Hour) * 4;

			relativeValue += Math.Abs (dateTimeNow.Minute - timeStamp.Minute);

			// System.Diagnostics.Debug.WriteLine (relativeValue);

			return relativeValue;
		}



		//public static int GetTimeHolder (this ContextPoint contextPoint, DateTimeOffset dateTimeNow)
		//{
		//	var timeStamp = contextPoint.Timestamp;

		//	var relativeValue = 1;

		//	relativeValue += Math.Abs (dateTimeNow.DayOfWeek.CompareTo (timeStamp.DayOfWeek));

		//	//relativeValue += Math.Abs (dateTimeNow.Day.CompareTo (timeStamp.Day));

		//	relativeValue += Math.Abs (dateTimeNow.Hour.CompareTo (timeStamp.Hour));

		//	//relativeValue += Math.Abs (dateTimeNow.Minute.CompareTo (timeStamp.Minute));

		//	System.Diagnostics.Debug.WriteLine (relativeValue);

		//	return relativeValue;
		//}


		public static void SetRelativeTimeValues (this List<ContextPoint> contextPoints)
		{
			var dateTimeNow = DateTimeOffset.Now;

			foreach (var point in contextPoints) point.RelativeTimeHolder = point.GetTimeHolder (dateTimeNow);

			//contextPoints.Sort ((x, y) => x.RelativeTimeHolder.CompareTo (y.RelativeTimeHolder));

			//for (int i = 1; i < contextPoints.Count; i++) contextPoints [(i - 1)].RelativeTimeOrder = i;
		}
	}
}