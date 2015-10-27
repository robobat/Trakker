
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
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Java.Lang;
using Android.Graphics.Drawables;
using Android.Util;
using System.Threading.Tasks;
using System.Net.Http;
using JSONParser;
using com.robobat.ParseObjectsTrakker;

namespace Trakker
{
	[Activity (Label = "Shows")]			
	public class Shows : AppCompatActivity, ViewPager.IOnPageChangeListener, TabLayout.IOnTabSelectedListener
	{
		Toolbar toolbar;
		DrawerLayout drawerLayout;
		private MyPagerAdapter adapter;
		private int count = 1;
		private int currentColor;
		private Drawable oldBackground;
		private ViewPager pager;
		private TabLayout tabs;

		//TVShow mShow;

		public void OnPageScrollStateChanged (int state)
		{
			Console.WriteLine ("Page scroll state changed: " + state);
		}

		public void OnPageScrolled (int position, float positionOffset, int positionOffsetPixels)
		{
			Console.WriteLine ("Page Scrolled");
		}

		public void OnTabReselected (TabLayout.Tab p0)
		{
			//throw new NotImplementedException ();
		}

		public void OnTabReselected (int position)
		{
			Toast.MakeText (this, "Tab reselected: " + position, ToastLength.Short).Show ();
		}

		public void OnPageSelected (int position)
		{
			Console.WriteLine ("Page selected: " + position);
		}

		public void OnTabSelected (TabLayout.Tab p0)
		{
			//Toast.MakeText (this, "Tab selected: " + p0, ToastLength.Short).Show ();
//			mOnSwipeSendTab = (OnSwipeSendTab)this.Activity;
//			mOnSwipeSendTab.sentTab (p0.Position);
			changeTab (p0.Position);
		}

		public void OnTabUnselected (TabLayout.Tab p0)
		{

		}

		public void changeTab (int pageNum)
		{

			pager.CurrentItem = pageNum;

		}


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

			SetContentView (Resource.Layout.shows);

			toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			//Toolbar will now take on default actionbar characteristics
			SetSupportActionBar (toolbar);
			SupportActionBar.Title = "Shows";
			SupportActionBar.SetHomeAsUpIndicator (Resource.Drawable.ic_menu);
			SupportActionBar.SetDisplayHomeAsUpEnabled (true);


			drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);

			var navigationView = FindViewById<NavigationView> (Resource.Id.nav_view);
			if (navigationView != null) {
				setupDrawerContent (navigationView);
			}
			setUpTabLayout ();


//			var myTask = exampleTask ();
//
			//createExampleParseObject ();

			var myTask2 = getParseObjectTask ();

		}

		void createExampleParseObject ()
		{
			var myCurrentShow = new TVShowForParse ();
			myCurrentShow.Name = "Seifeld";
			myCurrentShow.IMDBID = "tt0000001";

			myCurrentShow.SaveAsync ();

			var myCurrentShow2 = new TVShowForParse ();
			myCurrentShow2.Name = "Breaking Bad";
			myCurrentShow2.IMDBID = "tt0000002";

			myCurrentShow2.SaveAsync ();

			var myCurrentShow3 = new TVShowForParse ();
			myCurrentShow3.Name = "Sopranos";
			myCurrentShow3.IMDBID = "tt0000003";

			myCurrentShow3.SaveAsync ();

			var myTask2 = getParseObjectTask ();

		}

		public async Task getParseObjectTask ()
		{
			ParseObject gameScore = new ParseObject ("GameScore");
			gameScore ["score"] = 1337;
			gameScore ["playerName"] = "Sean Plott";
			await gameScore.SaveAsync ();


			var query = new ParseQuery<TVShowForParse> ()
				.WhereEqualTo ("showName", "Breaking Bad");
			IEnumerable<TVShowForParse> result = await query.FindAsync ();
			List<TVShowForParse> list = result.ToList ();



			Console.WriteLine ("my list size is " + list.Count + " and at 0 is " + list [0].Get<string> ("imdbID"));

			//Console.WriteLine (testing);


		}

		public async Task exampleTask ()
		{
			var client = new HttpClient ();
			var data = await client.GetStringAsync ("https://api.themoviedb.org/3/tv/1419?api_key=661b76973b90b91e0df214904015fd4d");

			JSONParserForTMDB myParser = new JSONParserForTMDB (data);

			//mShow = myParser.show;


		}


		void setUpTabLayout ()
		{
			adapter = new MyPagerAdapter (this.SupportFragmentManager);
			pager = FindViewById<ViewPager> (Resource.Id.pager);
			tabs = FindViewById<TabLayout> (Resource.Id.tabs);
			pager.Adapter = adapter;
			tabs.SetupWithViewPager (pager);

			var pageMargin = (int)TypedValue.ApplyDimension (ComplexUnitType.Dip, 4, Resources.DisplayMetrics);
			pager.PageMargin = pageMargin;
			pager.CurrentItem = 0;

			tabs.SetOnTabSelectedListener (this);
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
			//MenuInflater.Inflate (Resource.Menu.main, menu);
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


	public class MyPagerAdapter : FragmentPagerAdapter
	{
		private readonly string[] Titles = {
			"Popular", "Action", "Sci-Fi"
		};

		public MyPagerAdapter (FragmentManager fm) : base (fm)
		{
			
		}

		public override ICharSequence GetPageTitleFormatted (int position)
		{
			return new Java.Lang.String (Titles [position]);
		}

		#region implemented abstract members of PagerAdapter

		public override int Count {
			get { return Titles.Length; }
		}

		#endregion

		#region implemented abstract members of FragmentPagerAdapter

		public override Fragment GetItem (int position)
		{
			//This determines what fragment is called based on Tab Position
			if (position == 1) {
				return GridViewFragment.NewInstance (position);
			}

			return BasicFragment.NewInstance (position);

		}

		#endregion
		
	}


}

