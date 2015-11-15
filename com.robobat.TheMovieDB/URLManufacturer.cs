using System;

namespace com.robobat.TheMovieDB
{
	public class URLManufacturer
	{
		public void Construct (IURLBuilder urlBuilder)
		{
			urlBuilder.BuildStartOfURL ();
			urlBuilder.AddSpecificSearch ();
			urlBuilder.AddParameterStartAndAPIKey ();
			urlBuilder.AddPageNum ();
			//urlBuilder.AddDesiredParameters ();
		}


	}
}

