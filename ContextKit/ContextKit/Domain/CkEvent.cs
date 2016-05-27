using Realms;

namespace ContextKit
{
	public class CkEvent : RealmObject
	{
		// EventKit.EKEvent

		/* You can use this identifier to look up an event with the EKEventStore method eventWithIdentifier:.
		 * If the calendar of an event changes, its identifier most likely changes as well. */
		public string EventIdentifier { get; set; }

		public CkLocation StructuredLocation { get; set; }

		// public bool AllDay { get; set; }

		// public EKEventAvailability Availability  { get; set; }

		// public string BirthdayContactIdentifier { get; set; }

		// public DateTimeOffset EndDate { get; set; }

		// public bool IsDetached { get; set; }

		// public DateTimeOffset OccurrenceDate { get; set; }

		// public string Organizer { get; set; }

		// public DateTimeOffset StartDate { get; set; }

		// public EKEventStatus Status  { get; set; }


		// EventKit.EKCalendarItem

		public string CalendarType { get; set; }

		/* This property is set when the calendar item is created and can be used as a local identifier.
		 * Use calendarItemWithIdentifier: to look up the item by this value.
		 * 
		 * A full sync with the calendar will lose this identifier. You should have a plan for dealing with 
		 * a calendar whose identifier is no longer fetch-able by caching its other properties. */
		public string CalendarItemIdentifier { get; set; }

		/* This identifier allows you to access the same event or reminder across multiple devices.
		 * 
		 * There are some cases where duplicate copies of a calendar item can exist in the same database:
		 * 
		 *   - A calendar item was imported from an ICS file into multiple calendars
		 * 
		 *   - An event was created in a calendar shared with the user and the user was also invited to the event
		 * 
		 *   - The user is a delegate of a calendar that also has this event
		 * 
		 *   - A subscribed calendar was added to multiple accounts
		 * 
		 * In such cases, you should choose between calendar items based on other factors, such as the calendar or source.
		 * 
		 * Recurring event identifiers are the same for all occurrences. If you wish to differentiate between occurrences, 
		 * you may want to use the start date.
		 * 
		 * For Exchange servers, the identifier is different between iOS and OS X and different between devices for reminders. */
		public string CalendarItemExternalIdentifier { get; set; }

		public bool HasAttendees { get; set; }

		public bool HasNotes { get; set; }

		public string Notes { get; set; }

		public string Location { get; set; }

		public string Title { get; set; }

		public string Url { get; set; }

		// public EKAlarm[] Alarms  { get; set; }

		// public DateTimeOffset CreationDate { get; set; }

		// public bool HasAlarms { get; set; }

		// public EKParticipant[] Attendees  { get; set; }

		// public bool HasRecurrenceRules { get; set; }

		// public DateTimeOffset LastModifiedDate { get; set; }

		// public EKRecurrenceRule[] RecurrenceRules  { get; set; }

		// public NSTimeZone TimeZone  { get; set; }
	}
}

/* Sorting against CkEvents from NOW
 * - same location
 * - same event title
 * - same participants
 * - 
 */
