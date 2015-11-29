using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using com.robobat.TheMovieDB;
using Parse;

namespace Trakker
{
	[Activity (Label = "Trakker", MainLauncher = true, Icon = "@drawable/icon")]
	public class HomePageActivity : AppCompatActivity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			//Toolbar will now take on default actionbar characteristics
			SetSupportActionBar (toolbar);
			SupportActionBar.SetHomeAsUpIndicator (Resource.Drawable.ic_menu);
			SupportActionBar.SetDisplayHomeAsUpEnabled (true);
			SupportActionBar.Title = "Trakker";

			Console.WriteLine ("My enum for AirDateGTE is " + TMDBConstants.GetEnumDescription (com.robobat.TheMovieDB.TMDBConstants.DiscoverTV.AirDateGTE));

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};

			Button bGoToShows = FindViewById<Button> (Resource.Id.bGoToShows);

			bGoToShows.Click += delegate {
				var intent = new Intent (this, typeof(AddShowsTabActivity));
				StartActivity (intent);
			};

			Button bGoToTrakkedShows = FindViewById<Button> (Resource.Id.bGoToTrackedShows);

//			bGoToTrakkedShows.Click += delegate {
//				var intent = new Intent (this, typeof(ShowDetailsActivity));
//				StartActivity (intent);
//			};


			Button bCreateUser = FindViewById<Button> (Resource.Id.bCreateParseUser);

			bCreateUser.Click += delegate {
				var myUser = SignUpUserAsync ();
			};
	}

		public async System.Threading.Tasks.Task SignUpUserAsync ()
		{
				
			var parseUser = new ParseUser () {
				Username = "robobat91",
				Password = "myOtherPassword",
				Email = "robobat@91.com"
			};
			await parseUser.SignUpAsync ();
				

			await ParseUser.LogInAsync ("robobat91", "myOtherPassword");


		}
	
	}
}


