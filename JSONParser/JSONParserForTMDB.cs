using System;
using com.robobat.Model;
using Newtonsoft.Json.Linq;

namespace JSONParser
{
	public class JSONParserForTMDB
	{
		string rawData;

		public TVShow show;

		public JSONParserForTMDB (string data)
		{

			rawData = data;
			show = new TVShow ();

			populateShow ();
		}

		public void populateShow ()
		{
			JObject o = JObject.Parse (rawData);

			show.Name = "" + o ["name"];
			show.TMDBID = "" + o ["id"];
			show.posterLink = "" + o ["poster_path"];
			show.language = "" + o ["languages"] [0];
			show.numberOfSeasons = (int)o ["number_of_seasons"];
			show.numberOfEpisodes = (int)o ["number_of_episodes"];
			show.Overview = "" + o ["overview"];
			show.fullJSON = rawData;
		}


	}
}

