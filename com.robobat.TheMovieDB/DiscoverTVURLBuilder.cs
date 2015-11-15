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

		public void SetSortBy (com.robobat.TheMovieDB.TVConstants.DiscoverTV passedValue)
		{
			builtURL += TVConstants.PARAMATERSEPARATOR + TVConstants.SORTBY +
			TVConstants.PARAMETEREQUAL + TVConstants.GetEnumDescription (passedValue);
		}

		#region IURLBuilderMethods

		public void BuildStartOfURL ()
		{
			builtURL += TVConstants.TMDBURLSTART;
		}

		public void AddSpecificSearch ()
		{
			//Specific to DiscoverTV
			builtURL += TVConstants.DISCOVERTV;
		}

		public void AddParameterStartAndAPIKey ()
		{
			builtURL += TVConstants.PARAMATERSTART + TVConstants.TMDBAPIKEY;
		}

		public void AddPageNum ()
		{
			builtURL += TVConstants.PARAMATERSEPARATOR + TVConstants.GetEnumDescription (TVConstants.DiscoverTV.Page) +
			TVConstants.PARAMETEREQUAL + page;
		}

		public void AddDesiredParameters (string[] myArray)
		{
			if (myArray.Length == 2) {
				builtURL += TVConstants.PARAMATERSEPARATOR + myArray [0] +
				TVConstants.PARAMETEREQUAL + myArray [1];
			} else if (myArray.Length > 2) {
				builtURL += TVConstants.PARAMATERSEPARATOR + myArray [0] + TVConstants.PARAMETEREQUAL;
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

