using System;
using Foundation;

namespace ContextKit
{
	public static class DateTimeExtensions
	{
		public static DateTime ToDateTime (this NSDate date)
		{
			// NSDate has a wider range than DateTime, so clip
			// the converted date to DateTime.Min|MaxValue.
			double secs = date.SecondsSinceReferenceDate;

			if (secs < -63113904000)
				return DateTime.MinValue;
			if (secs > 252423993599)
				return DateTime.MaxValue;

			return (DateTime)date;
		}

		public static NSDate ToNSDate (this DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
				date = DateTime.SpecifyKind (date, DateTimeKind.Utc);
			//date = DateTime.SpecifyKind (date, /* DateTimeKind.Local or DateTimeKind.Utc, this depends on each app */);

			return (NSDate)date;
		}
	}
}