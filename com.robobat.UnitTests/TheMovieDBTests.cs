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

			Assert.AreEqual ("https://api.themoviedb.org/3/discover/tv?" + TVConstants.TMDBAPIKEY + "&page=1", 
				urlBuilder.BuiltURL);
		}

		[Test]
		public void GenerateDiscoverTVURLWithGenres ()
		{
			newManufacturer = new URLManufacturer ();
			urlBuilder = new DiscoverTVURLBuilder (1);

			newManufacturer.Construct (urlBuilder);

			string[] myGenres = {
				TVConstants.GetEnumDescription (TVConstants.DiscoverTV.WithGenres), 
				TVConstants.ORSEPARATOR, 
				"" + (int)TVConstants.Genres.Drama, 
				"" + (int)TVConstants.Genres.Action, 
				"" + (int)TVConstants.Genres.SciFi
			};
			urlBuilder.AddDesiredParameters (myGenres);

			Assert.AreEqual ("https://api.themoviedb.org/3/discover/tv?" +
			TVConstants.TMDBAPIKEY + "&page=1&with_genres=" + (int)TVConstants.Genres.Drama
			+ "|" + (int)TVConstants.Genres.Action + "|" + (int)TVConstants.Genres.SciFi, 
				urlBuilder.BuiltURL);


		}

		[Test]
		public void GenerateDiscoverTVURLWithGenresSortByPopularityDescPage3 ()
		{
			newManufacturer = new URLManufacturer ();
			urlBuilder = new DiscoverTVURLBuilder (3);

			newManufacturer.Construct (urlBuilder);
			string[] myGenres = {
				TVConstants.GetEnumDescription (TVConstants.DiscoverTV.WithGenres), 
				TVConstants.ORSEPARATOR, 
				"" + (int)TVConstants.Genres.Drama, 
				"" + (int)TVConstants.Genres.Action, 
				"" + (int)TVConstants.Genres.SciFi
			};
			urlBuilder.AddDesiredParameters (myGenres);

			string[] sortByParam = {
				TVConstants.SORTBY,
				TVConstants.GetEnumDescription (TVConstants.DiscoverTV.PopularityDesc)
			};
			urlBuilder.AddDesiredParameters (sortByParam);

			Assert.AreEqual ("https://api.themoviedb.org/3/discover/tv?" +
			TVConstants.TMDBAPIKEY + "&page=3&with_genres=" + (int)TVConstants.Genres.Drama
			+ "|" + (int)TVConstants.Genres.Action + "|" + (int)TVConstants.Genres.SciFi +
			"&sort_by=" + TVConstants.GetEnumDescription (TVConstants.DiscoverTV.PopularityDesc), 
				urlBuilder.BuiltURL);

		}

	}
}