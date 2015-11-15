using System;
using NUnit.Framework;
using com.robobat.Model;
using MockJockey.org.thoersch.MockJockey;
using MockJockey.org.thoersch.MockJockey.Expectations;

namespace com.robobat.UnitTests
{
	[TestFixture]
	public class ShowUnitTests
	{
		TVShow myShow;
		bool eventFlag = false;

		[SetUp]
		public void Setup ()
		{

			myShow = new TVShow ();
			myShow.Name = "Breaking Bad";
			myShow.Network = "AMC";

		}


		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void ShowTestsCorrectly ()
		{
			Assert.AreEqual ("Breaking Bad", myShow.Name);
		}

		[Test]
		public void ShowNetworkIsShowTime ()
		{
			Assert.AreEqual ("AMC", myShow.Network);
		}

		[Test]
		public void TextMyMock ()
		{
			Jockey jockey = new Jockey ();
			IDataService mockData = jockey.Mock<IDataService> ();

			Expect.When (mockData)
					.CallsMethod ("returnDexter")
					.ThatItReturns ("CSI");

			Assert.AreEqual ("CSI", mockData.returnDexter ());
		}

		void MySubscriber (object sender, EventArgs e)
		{
			eventFlag = true;
		}

		[Test]
		public void TextMyMockEvent ()
		{
			Jockey jockey = new Jockey ();
			IEventsTest myMockEvents = jockey.Mock<IEventsTest> ();
			myMockEvents.TestEvent += MySubscriber;

			Expect.When (myMockEvents)
				.CallsMethod ("ExecuteWithSideEffect")
				.ThatItRaises ("TestEvent", myMockEvents, new EventArgs ());

			myMockEvents.ExecuteWithSideEffect ();
			Assert.IsTrue (eventFlag);


			Jockey myJockey = new Jockey ();
			IDataService myMockService = myJockey.Mock<IDataService> ();

			Expect.When (myMockService)
					.CallsProperty ("Name")
					.ThatItReturns ("My Product Name");

			Assert.IsNotNull (myMockService);
			Assert.AreEqual ("My Product Name", myMockService.Name);
		}


	}
}

