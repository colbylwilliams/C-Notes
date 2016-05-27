using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ContextKit
{
	public class CkContext
	{
		static CkContext _shared;

		public static CkContext Shared => _shared ?? (_shared = new CkContext ());

		CNote _selectedNote;

		public CNote SelectedNote {
			get { return _selectedNote; }
			set {
				_selectedNote = value;

				Task.Run (async () => await AddContextPoint (value.Id));
			}
		}


		public List<CNote> Notes => RealmProxy.Notes;

		public RealmProxy RealmProxy { get; private set; }

		public EventProvider EventProvider = new EventProvider ();

		public LocationProvider LocationProvider = new LocationProvider ();


		CkContext ()
		{
			RealmProxy = new RealmProxy (EventProvider, LocationProvider);
		}


		public void PrintEvents ()
		{
			Task.Run (async () => await EventProvider.GetCalendarEvents ());
		}


		public void CreateNewNote ()
		{
			var id = RealmProxy.CreateNewNote ();

			_selectedNote = RealmProxy.GetNote (id);
		}

		public Task AddContextPoint (string noteId = null) => RealmProxy.AddContextPoint (noteId ?? _selectedNote.Id);

		public void DeleteNote (CNote note) => RealmProxy.DeleteNote (note);

		public void UpdateNote (string note) => RealmProxy.UpdateNote (_selectedNote, null, note);

		public void UpdateNote (string title, string note) => RealmProxy.UpdateNote (_selectedNote, title, note);

		public void UpdateNoteTitle (string title) => RealmProxy.UpdateNote (_selectedNote, title, null);

		public Task RefreshContextAndSortNotes () => RealmProxy.RefreshContextAndSortNotes ();
	}
}