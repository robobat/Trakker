using System;
using Parse;

namespace com.robobat.ParseObjectsTrakker
{
	[ParseClassName ("TVShowForParse")]
	public class TVShowForParse : ParseObject
	{
		[ParseFieldName ("displayName")]
		public string DisplayName {
			get { return GetProperty<string> (); }
			set { SetProperty<string> (value); }
		}

		[ParseFieldName ("imdbID")]
		public string IMDBID {
			get { return GetProperty<string> (); }
			set { SetProperty<string> (value); }
		}

		[ParseFieldName ("showName")]
		public string Name {
			get { return GetProperty<string> (); }
			set { SetProperty<string> (value); }
		}
	}
}

