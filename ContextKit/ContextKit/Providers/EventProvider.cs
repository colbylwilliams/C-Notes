using System;
using System.Linq;
using System.Threading.Tasks;

using EventKit;
using Foundation;

namespace ContextKit
{
	public class EventProvider
	{
		nint dateRange = 1;

		readonly EKEventStore EkEventStore = new EKEventStore ();

		NSDate daysFromNow (nint days) => NSCalendar.CurrentCalendar.DateByAddingUnit (NSCalendarUnit.Day, days, NSDate.Now, NSCalendarOptions.None);

		// Also check if for events that were recently added, accepted, created, updated, etc...

		public async Task GetCalendarEvents ()
		{
			if (EKEventStore.GetAuthorizationStatus (EKEntityType.Event) == EKAuthorizationStatus.Authorized || (await EkEventStore.RequestAccessAsync (EKEntityType.Event)).Item1) {

				var calendars = EkEventStore.Calendars.Where (c => c.Type != EKCalendarType.Birthday && c.Type != EKCalendarType.Subscription).ToArray ();

				var predicate = EkEventStore.PredicateForEvents (daysFromNow (0 - dateRange), daysFromNow (dateRange), calendars);



				EkEventStore.EnumerateEvents (predicate, (EKEvent theEvent, ref bool stop) => {

					var attendees = theEvent.HasAttendees ? string.Join (", ", theEvent.Attendees.Select (a => a.Name)) : "none";

					System.Diagnostics.Debug.WriteLine ($"\n~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\nStop: {stop}\nCalendarType: {theEvent.Calendar.Type}\n\nTitle: {theEvent.Title}\nLocation: {theEvent.Location}\nAttendees: {attendees}\n\n<notes>\n\n{theEvent.Notes}\n\n</notes>\n");

				});
			}
		}
	}
}