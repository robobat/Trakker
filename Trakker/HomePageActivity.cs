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

			Console.WriteLine ("My enum for AirDateGTE is " + TVConstants.GetEnumDescription (com.robobat.TheMovieDB.TVConstants.DiscoverTV.AirDateGTE));

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

			bGoToTrakkedShows.Click += delegate {
				var intent = new Intent (this, typeof(ShowDetailsTabActivity));
				StartActivity (intent);
			};
		}
	}
}


