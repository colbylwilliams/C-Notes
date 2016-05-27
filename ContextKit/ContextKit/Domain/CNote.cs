using Realms;

namespace ContextKit
{
	public class CNote : RealmObject
	{
		[ObjectId]
		public string Id { get; set; }

		public string Title { get; set; }

		public string Note { get; set; }

		public ContextItem Context { get; set; }

		[Ignored]
		public bool HasTitle => !string.IsNullOrEmpty (Title);

		[Ignored]
		public bool HasNote => !string.IsNullOrEmpty (Note);

		[Ignored]
		public bool HasContext => Context?.HasContextPoints ?? false;

		[Ignored]
		public string NotePreview => HasNote ? Note : null;

		public override string ToString () => $"\n[CNote] {Title}\n{Context}";
	}
}