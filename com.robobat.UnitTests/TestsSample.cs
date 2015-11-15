using System;
using NUnit.Framework;
using com.robobat.Model;

namespace com.robobat.UnitTests
{
	[TestFixture]
	public class TestsSample
	{
		TVShow myShow;

		[SetUp]
		public void Setup ()
		{

			myShow = new TVShow ();
			myShow.Name = "Dexter";
			myShow.Network = "Showtime";


		}

		
		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void ShowNameIsDexter ()
		{
			Assert.AreEqual ("Dexter", myShow.Name);
		}

		[Test]
		public void ShowNetworkIsShowTime ()
		{
			Assert.AreEqual ("Showtime", myShow.Network);
		}






	}
}

