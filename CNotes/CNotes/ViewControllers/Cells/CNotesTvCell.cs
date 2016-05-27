using System;
using System.Linq;

using UIKit;

using ContextKit;

namespace CNotes
{
	public partial class CNotesTvCell : UITableViewCell
	{
		public CNotesTvCell (IntPtr handle) : base (handle) { }

		public void SetData (CNote note)
		{
			noteTitle.Text = note.HasTitle ? note.Title : "Untitled";
			noteTitle.TextColor = note.HasTitle ? UIColor.Black : UIColor.DarkGray;

			var point = note.Context?.ContextPoints?.LastOrDefault ();

			noteSubtitle.Text = point?.ShortTimestampString ?? string.Empty;

			noteAuxtitle.Text = note.NotePreview ?? point?.LocationCityStateString ?? string.Empty;
		}
	}
}