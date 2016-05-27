using Realms;
using System;

namespace ContextKit
{
	public class ContextPoint : RealmObject
	{
		//public ContextPoint ()
		//{
		//var day = Timestamp.Day; // day of month
		//var day
		//}

		[Indexed]
		public DateTimeOffset Timestamp { get; set; }

		public CkLocation Location { get; set; }

		public RealmList<CkEvent> Events { get; }

		[Ignored]
		public bool HasLocation => Location != null;


		//[Ignored]
		public double RelativeDistanceHolder { get; set; }

		//[Ignored]
		public double RelativeDistanceOrder { get; set; }


		//[Ignored]
		public double RelativeTimeHolder { get; set; }

		//[Ignored]
		public double RelativeTimeOrder { get; set; }


		[Ignored]
		public string TimestampString => Timestamp.ToLocalTime ().DateTime.ToString ("g");

		[Ignored]
		public string ShortTimestampString => Timestamp.ToLocalTime ().DateTime.ToString ("MM/dd/yy");

		[Ignored]
		public string LocationCoordinatesString => Location?.CoordinatesString;

		[Ignored]
		public string LocationCityStateString => Location?.CityStateString;
	}
}