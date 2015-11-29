using System;
using System.ComponentModel;
using System.Reflection;

namespace com.robobat.TheMovieDB
{
	public class TMDBConstants
	{
		public const string TMDBURLSTART = "https://api.themoviedb.org/3";

		public const string PARAMATERSTART = "?";

		public const string TMDBAPIKEY = "api_key=661b76973b90b91e0df214904015fd4d";

		public const string PARAMATERSEPARATOR = "&";

		public const string PARAMETEREQUAL = "=";

		public const string DISCOVERTV = "/discover/tv";

		public const string SORTBY = "sort_by";

		public const string ANDSEPARATOR = ",";
		public const string ORSEPARATOR = "|";

		public enum Genres
		{
			Action = 10759,
			Animation = 16,
			Comedy = 35,
			Documentary = 99,
			Drama = 18,
			Family = 10751,
			Kids = 10762,
			Mystery = 9648,
			News = 10763,
			Reality = 10764,
			SciFi = 10765,
			Soap = 10766,
			Talk = 10767,
			War = 10768,
			Western = 37
		}

		public enum DiscoverTV
		{
			[Description ("air_date.gte")]
			AirDateGTE,
			[Description ("air_date.lte")]
			AirDateLTE,
			[Description ("first_air_date.gte")]
			FirstAirDateGTE,
			[Description ("first_air_date.lte")]
			FirstAirDateLTE,
			[Description ("first_air_date_year")]
			FirstAirYearDate,
			[Description ("language")]
			Language,
			[Description ("page")]
			Page,
			[Description ("vote_average.desc")]
			VoteAverageDesc,
			[Description ("vote_average.asc")]
			VoteAverageAsc,
			[Description ("first_air_date.desc")]
			FirstAirDateDesc,
			[Description ("first_air_date.asc")]
			FirstAirDateAsc,
			[Description ("popularity.desc")]
			PopularityDesc,
			[Description ("popularity.asc")]
			PopularityAsc,
			[Description ("vote_average.gte")]
			VoteAverageGTE,
			[Description ("vote_count.gte")]
			VoteCountGTE,
			[Description ("with_genres")]
			WithGenres,
			[Description ("with_networks")]
			WithNetworks


		}


		public static string GetEnumDescription (Enum value)
		{
			FieldInfo fi = value.GetType ().GetField (value.ToString ());

			DescriptionAttribute[] attributes = 
				(DescriptionAttribute[])fi.GetCustomAttributes (typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
				return attributes [0].Description;
			else
				return value.ToString ();
		}

	}


}

