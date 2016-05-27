using Realms;
using System.Linq;
using System.Text;

namespace ContextKit
{
	public class ContextItem : RealmObject
	{
		const double MaxDistance = 99999;

		[ObjectId]
		public string Id { get; set; }

		public RealmList<ContextPoint> ContextPoints { get; }

		[Ignored]
		public bool HasContextPoints => ContextPoints != null;

		[Ignored]
		public double AverageDistance {
			get {
				var contextPointsList = ContextPoints?.Where (cp => cp.RelativeDistanceHolder < MaxDistance);

				return (contextPointsList?.Any () ?? false) ? contextPointsList.Average (cp => cp.RelativeDistanceHolder) : MaxDistance;
			}
		}
		//=> HasContextPoints ? ContextPoints.Where (cp => cp.RelativeDistanceHolder > 0 && cp.RelativeDistanceHolder < MaxDistance).Average (cp => cp.RelativeDistanceHolder) : MaxDistance;

		//[Ignored]
		//public double AverageDistanceOrder {
		//	get {
		//		var contextPointsList = ContextPoints?.Where (cp => cp.RelativeDistanceOrder > 0);

		//		return (contextPointsList?.Any () ?? false) ? contextPointsList.Average (cp => cp.RelativeDistanceOrder) : MaxDistance;
		//	}
		//}
		//=> HasContextPoints ? ContextPoints.Where (cp => cp.RelativeDistanceOrder > 0).Average (cp => cp.RelativeDistanceOrder) : MaxDistance;

		[Ignored]
		public double AverageTime {
			get {
				var contextPointsList = ContextPoints?.Where (cp => cp.RelativeTimeHolder > 0);

				return (contextPointsList?.Any () ?? false) ? contextPointsList.Average (cp => cp.RelativeTimeHolder) : MaxDistance;
			}
		}
		//=> HasContextPoints ? ContextPoints.Where (cp => cp.RelativeTimeHolder > 0).Average (cp => cp.RelativeTimeHolder) : MaxDistance;


		//[Ignored]
		//public double AverageTimeOrder {
		//	get {
		//		var contextPointsList = ContextPoints?.Where (cp => cp.RelativeTimeOrder > 0);

		//		return (contextPointsList?.Any () ?? false) ? contextPointsList.Average (cp => cp.RelativeTimeOrder) : MaxDistance;
		//	}
		//}
		//=> HasContextPoints ? ContextPoints.Where (cp => cp.RelativeTimeOrder > 0).Average (cp => cp.RelativeTimeOrder) : MaxDistance;

		//[Ignored]
		//public double RelativeSortValue
		//	=> (AverageDistanceOrder * 10) + AverageTime;


		public override string ToString ()
		{
			var sb = new StringBuilder ();
			sb.AppendLine (" ContextItem");
			sb.AppendFormat ("  {0}{1}\n", "Id".PadRight (28), Id);
			sb.AppendFormat ("  {0}{1}\n", "ContextPoints".PadRight (28), ContextPoints);
			sb.AppendFormat ("  {0}{1}\n", "AverageDistance".PadRight (28), AverageDistance);
			//sb.AppendFormat ("  {0}{1}\n", "AverageDistanceOrder".PadRight (28), AverageDistanceOrder);
			sb.AppendFormat ("  {0}{1}\n", "AverageTime".PadRight (28), AverageTime);
			//sb.AppendFormat ("  {0}{1}\n", "AverageTimeOrder".PadRight (28), AverageTimeOrder);
			//sb.AppendFormat ("  {0}{1}\n", "RelativeSortValue".PadRight (28), RelativeSortValue);
			return sb.ToString ();
		}
	}
}

/* Data
 * - Current Location
 * - Current Day of the week
 * - Current 
 */
