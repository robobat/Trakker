using System;

namespace com.robobat.Model
{
	public class TVShow
	{
		public string fullXML { get; set; }

		public string fullJSON { get; set; }

		public string TVDBID { get; set; }

		public string Name { get; set; }

		public string Overview { get; set; }

		public string Network { get; set; }

		public string IMDBID { get; set; }

		public string posterLink { get; set; }

		public string bannerLink { get; set; }

		public Boolean isFavorite { get; set; }

		public int numberOfSeasons { get; set; }

		public int numberOfEpisodes { get; set; }

		public string TMDBID { get; set; }

		public string language { get; set; }

		public TVShow ()
		{
			isFavorite = false;
		}

		public TVShow (String tvdbid, String name)
		{
			TVDBID = tvdbid;
			Name = name;
			isFavorite = false;
		}



	}
}

