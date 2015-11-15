using System;

namespace com.robobat.TheMovieDB
{
	public interface IURLBuilder
	{
		void BuildStartOfURL ();

		void AddSpecificSearch ();

		void AddParameterStartAndAPIKey ();

		void AddPageNum ();

		void AddDesiredParameters (string[] myArray);

		string BuiltURL { get; }

		int Page { get; }

	}
}

