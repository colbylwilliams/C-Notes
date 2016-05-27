using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Realms;

namespace ContextKit
{
	public class RealmProxy : RealmProxyBase
	{
		RealmResults<CNote> allNotes => realm.All<CNote> ();

		//IOrderedQueryable<CNote> allNotesSorted => realm.All<CNote> ().Where (n => n.HasContext && n.Context.ContextPoints.Count > 0).OrderByDescending (n => n.Context.ContextPoints.OrderByDescending (cp => cp.Timestamp).First ().Timestamp);

		List<CNote> _notes;

		public List<CNote> Notes => _notes;

		public RealmProxy (EventProvider eventProvider, LocationProvider locationProvider) : base (eventProvider, locationProvider)
		{
			try {

				_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.AverageTime).ToList ();
				//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderByDescending (n => n.Context.ContextPoints.OrderByDescending (cp => cp.Timestamp).First ().Timestamp).ToList ();

			} catch (RealmMigrationNeededException) {

				Realm.DeleteRealm (RealmConfiguration.DefaultConfiguration);

				throw;
			}
		}


		public CNote GetNote (string noteId)
		{
			realm.Refresh ();

			return allNotes.Where (n => n.Id == noteId).ToList ().FirstOrDefault ();
		}


		public string CreateNewNote ()
		{
			var newId = Guid.NewGuid ().ToString ();

			realm.Refresh ();

			RunInTransaction (() => {

				var note = realm.CreateObject<CNote> ();

				note.Id = newId;
			});

			//realm.Refresh ();

			//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.AverageDistance).ThenBy (n => n.Context.AverageTime).ToList ();
			//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.RelativeSortValue).ToList ();
			//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderByDescending (n => n.Context.ContextPoints.OrderByDescending (cp => cp.Timestamp).First ().Timestamp).ToList ();
			//_notes = allNotes.ToList ();
			return newId;
		}


		public void UpdateNote (CNote note, string title = null, string text = null)
		{
			var refNote = GetNote (note.Id);

			RunInTransaction (() => {

				if (title != null) refNote.Title = title;

				if (text != null) refNote.Note = text;

			});

			_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.AverageDistance).ThenBy (n => n.Context.AverageTime).ToList ();
			//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.RelativeSortValue).ToList ();
			//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderByDescending (n => n.Context.ContextPoints.OrderByDescending (cp => cp.Timestamp).First ().Timestamp).ToList ();
		}


		public async Task AddContextPoint (string noteId)
		{
			var location = await LocationProvider.GetCurrentLocationAsync ();

			if (location != null) {

				var placemark = await LocationProvider.ReverseGeocodeLocation (location);

				//realm.Refresh ();

				var note = GetNote (noteId);

				if (note != null) {

					RunInTransaction (() => {

						if (note.Context == null) note.Context = CreateNewContextItem ();

						AddContextPoint (note.Context, location, placemark);

						Log ($"Note.Context: {note.Context}");
					});
				}

				_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.AverageDistance).ThenBy (n => n.Context.AverageTime).ToList ();
				//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.RelativeSortValue).ToList ();
				//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderByDescending (n => n.Context.ContextPoints.OrderByDescending (cp => cp.Timestamp).First ().Timestamp).ToList ();
				//_notes = allNotes.ToList ();
			}
		}


		bool refreshing = false;

		public async Task RefreshContextAndSortNotes ()
		{
			if (!refreshing) {

				refreshing = true;

				Log ("RefreshContextAndSortNotes");

				await RefreshContextState ();

				realm.Refresh ();

				refreshing = false;
			}

			_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.AverageDistance).ThenBy (n => n.Context.AverageTime).ToList ();
			//_notes = allNotes.ToList ().Where (n => n.HasContext).OrderBy (n => n.Context.RelativeSortValue).ToList ();

			foreach (var note in _notes) Log (note.ToString ());
		}


		public void DeleteNote (CNote note)
		{
			RunInTransaction (() => realm.Remove (note));

			_notes.Remove (note);
		}


		bool log = true;

		void Log (string message) { if (log) System.Diagnostics.Debug.WriteLine ($"[RealmProxy] {message}"); }
	}
}