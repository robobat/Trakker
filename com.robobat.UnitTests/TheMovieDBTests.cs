using System;
using NUnit.Framework;
using com.robobat.Model;
using MockJockey.org.thoersch.MockJockey;
using MockJockey.org.thoersch.MockJockey.Expectations;
using com.robobat.TheMovieDB;

namespace com.robobat.UnitTests
{
	[TestFixture]
	public class TheMovieDBTests
	{
		URLManufacturer newManufacturer = null;
		IURLBuilder urlBuilder = null;

		[SetUp]
		public void Setup ()
		{
			
		}


		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void GenerateBaseDiscoverTVURLPage1 ()
		{
			newManufacturer = new URLManufacturer ();
			urlBuilder = new DiscoverTVURLBuilder (1);

			newManufacturer.Construct (urlBuilder);

			Assert.AreEqual ("https://api.themoviedb.org/3/discover/tv?" + TMDBConstants.TMDBAPIKEY + "&page=1", 
				urlBuilder.BuiltURL);
		}

		[Test]
		public void GenerateDiscoverTVURLWithGenres ()
		{
			newManufacturer = new URLManufacturer ();
			urlBuilder = new DiscoverTVURLBuilder (1);

			newManufacturer.Construct (urlBuilder);

			string[] myGenres = {
				TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.WithGenres), 
				TMDBConstants.ORSEPARATOR, 
				"" + (int)TMDBConstants.Genres.Drama, 
				"" + (int)TMDBConstants.Genres.Action, 
				"" + (int)TMDBConstants.Genres.SciFi
			};
			urlBuilder.AddDesiredParameters (myGenres);

			Assert.AreEqual ("https://api.themoviedb.org/3/discover/tv?" +
			TMDBConstants.TMDBAPIKEY + "&page=1&with_genres=" + (int)TMDBConstants.Genres.Drama
			+ "|" + (int)TMDBConstants.Genres.Action + "|" + (int)TMDBConstants.Genres.SciFi, 
				urlBuilder.BuiltURL);


		}

		[Test]
		public void GenerateDiscoverTVURLWithGenresSortByPopularityDescPage3 ()
		{
			newManufacturer = new URLManufacturer ();
			urlBuilder = new DiscoverTVURLBuilder (3);

			newManufacturer.Construct (urlBuilder);
			string[] myGenres = {
				TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.WithGenres), 
				TMDBConstants.ORSEPARATOR, 
				"" + (int)TMDBConstants.Genres.Drama, 
				"" + (int)TMDBConstants.Genres.Action, 
				"" + (int)TMDBConstants.Genres.SciFi
			};
			urlBuilder.AddDesiredParameters (myGenres);

			string[] sortByParam = {
				TMDBConstants.SORTBY,
				TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.PopularityDesc)
			};
			urlBuilder.AddDesiredParameters (sortByParam);

			Assert.AreEqual ("https://api.themoviedb.org/3/discover/tv?" +
			TMDBConstants.TMDBAPIKEY + "&page=3&with_genres=" + (int)TMDBConstants.Genres.Drama
			+ "|" + (int)TMDBConstants.Genres.Action + "|" + (int)TMDBConstants.Genres.SciFi +
			"&sort_by=" + TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.PopularityDesc), 
				urlBuilder.BuiltURL);

		}

	}
}