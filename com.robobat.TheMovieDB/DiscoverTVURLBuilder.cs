using System;

namespace com.robobat.TheMovieDB
{
	public class DiscoverTVURLBuilder : IURLBuilder
	{
		string builtURL;
		int page;


		public DiscoverTVURLBuilder (int pageNum)
		{
			builtURL = "";
			page = pageNum;
		}

		public void SetSortBy (com.robobat.TheMovieDB.TMDBConstants.DiscoverTV passedValue)
		{
			builtURL += TMDBConstants.PARAMATERSEPARATOR + TMDBConstants.SORTBY +
			TMDBConstants.PARAMETEREQUAL + TMDBConstants.GetEnumDescription (passedValue);
		}

		#region IURLBuilderMethods

		public void BuildStartOfURL ()
		{
			builtURL += TMDBConstants.TMDBURLSTART;
		}

		public void AddSpecificSearch ()
		{
			//Specific to DiscoverTV
			builtURL += TMDBConstants.DISCOVERTV;
		}

		public void AddParameterStartAndAPIKey ()
		{
			builtURL += TMDBConstants.PARAMATERSTART + TMDBConstants.TMDBAPIKEY;
		}

		public void AddPageNum ()
		{
			builtURL += TMDBConstants.PARAMATERSEPARATOR + TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.Page) +
			TMDBConstants.PARAMETEREQUAL + page;
		}

		public void AddDesiredParameters (string[] myArray)
		{
			if (myArray.Length == 2) {
				builtURL += TMDBConstants.PARAMATERSEPARATOR + myArray [0] +
				TMDBConstants.PARAMETEREQUAL + myArray [1];
			} else if (myArray.Length > 2) {
				builtURL += TMDBConstants.PARAMATERSEPARATOR + myArray [0] + TMDBConstants.PARAMETEREQUAL;
				int parametersToAdd = myArray.Length - 2;

				builtURL += myArray [2];
				for (int i = 3; i < myArray.Length; i++) {
					builtURL += myArray [1] + myArray [i];
				}



			}




		}






		//Method which Returns the String
		public string BuiltURL {
			get{ return builtURL; }
		}

		public int Page {
			get { return page; }

		}

		#endregion

	}
}

