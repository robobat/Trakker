
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parse;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Java.Lang;
using Android.Graphics.Drawables;
using Android.Util;
using System.Threading.Tasks;
using System.Net.Http;
using JSONParser;
using com.robobat.ParseObjectsTrakker;
using Android.Accounts;
using Newtonsoft.Json.Linq;

namespace Trakker
{
	[Activity (Label = "Add Shows")]			
	public class ShowDetailsActivity : AppCompatActivity
	{
		Toolbar toolbar;
		DrawerLayout drawerLayout;
		private int count = 1;
		private int currentColor;
		private Drawable oldBackground;
		private ViewPager pager;
		private TabLayout tabs;
		TextView myTextView;

		public AddShowsFragment mAddShows1, mAddShows2, mAddShows3;


		public string name;
		public string network;



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

			SetContentView (Resource.Layout.show_details);

			toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			//Toolbar will now take on default actionbar characteristics
			SetSupportActionBar (toolbar);
			SupportActionBar.Title = "Show Details";
			SupportActionBar.SetHomeAsUpIndicator (Resource.Drawable.ic_menu);
			SupportActionBar.SetDisplayHomeAsUpEnabled (true);


			drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);

			var navigationView = FindViewById<NavigationView> (Resource.Id.nav_view);
			if (navigationView != null) {
				setupDrawerContent (navigationView);
			}
		
			myTextView = FindViewById<TextView> (Resource.Id.tvForID);
			//myTextView.Text = Intent.GetStringExtra ("TVDBID");

			var myDownloadTask = downloadURL ();

		}

		public async Task downloadURL ()
		{
			string myURL = "https://api.themoviedb.org/3/tv/" + Intent.GetStringExtra ("TVDBID")  +"?api_key=661b76973b90b91e0df214904015fd4d";

			var client = new HttpClient ();
			var data = await client.GetStringAsync (myURL);
			JObject o = JObject.Parse (data);
			name = o ["name"] + "";
			network = o ["networks"] [0]["name"]+"";
			setTextvieww();
//				

		}

		void setTextvieww ()
		{
			string mySHow = "Show title is " + name + " on network " +network;
			myTextView.Text = mySHow;
		}

		void setupDrawerContent (NavigationView navigationView)
		{
			navigationView.NavigationItemSelected += (sender, e) => {
				//e.MenuItem.SetChecked (true);
				Console.WriteLine ("Nav Menu Item ID is " + e.MenuItem.ItemId);
				drawerLayout.CloseDrawers ();
//				if (e.MenuItem.ItemId == Resource.Id.nav_popular) {
//					var intent = new Intent (this, typeof(GridViewActivity));
//					StartActivity (intent);
//				}
			};
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.shows_menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
				drawerLayout.OpenDrawer (Android.Support.V4.View.GravityCompat.Start);
				return true;

			default:
				Toast.MakeText (this, "Top ActionBar pressed: " + item.ItemId, ToastLength.Short).Show ();

				return base.OnOptionsItemSelected (item);
			}
		}

	}




}

