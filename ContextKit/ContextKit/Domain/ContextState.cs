using Realms;
using System;

namespace ContextKit
{
	public class ContextState : RealmObject
	{
		public DateTimeOffset LastRefresh { get; set; }
	}
}