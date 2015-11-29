
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using com.robobat.TheMovieDB;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using System.Collections;

namespace Trakker
{
	public class AddShowsFragment : Fragment
	{

		public int PAGENUM = 1;

		List<JToken> showsJToken = new List<JToken> ();

		//		private readonly string[] Titles = {
		//			"Popular", "Action", "Sci-Fi", "Drama", "Mystery", "Comedy", "Animation", "Sports"
		//		};
		Activity mActivity;
		private int position;
		private string title;
		AddShowsAdapter mAdapter;
		GridView mGridView;

		//		public static AddShowsFragment NewInstance (int position, string title)
		//		{
		//
		//			var f = new AddShowsFragment (this.Activity, position, title);
		//			var b = new Bundle ();
		//			b.PutInt ("position", position);
		//			b.PutString ("title", title);
		//			f.Arguments = b;
		//			return f;
		//
		//		}

		public string getTitle ()
		{
			return title;
		}

		public static AddShowsFragment NewInstance (int position, string title)
		{
			var f = new AddShowsFragment ();
			var b = new Bundle ();
			b.PutInt ("position", position);
			b.PutString ("title", title);
			f.Arguments = b;
			return f;
		}

		//		public AddShowsFragment ()
		//		{
		//			Console.WriteLine ("I called default constructor");
		//		}
		//
		//		public AddShowsFragment (Activity a, int pos, string tit)
		//		{
		//			mActivity = a;
		//			position = pos;
		//			title = tit;
		//		}


		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here

			position = Arguments.GetInt ("position");
			title = Arguments.GetString ("title");

//			position = Arguments.GetInt ("position");
//			title = Arguments.GetString ("title");
		}

		public override View OnCreateView (LayoutInflater inflater, 
		                                   ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var root = inflater.Inflate (Resource.Layout.add_show_gridview_layout, container, false);

			mGridView = root.FindViewById<GridView> (Resource.Id.myGridview);
			setUpAdapter ();

			string buttonText = "I am in position " + position + " with title " + title + " And value " + getGenreValue (title);

			if (savedInstanceState != null) {
				var myNewList = savedInstanceState.GetStringArray ("TrakkedList");
				if (myNewList != null) {
					//mAdapter.setTrakkedShows ((List<string>)savedInstanceState.GetStringArrayList ("TrakkedList"));
					buttonText += "" + myNewList.Length;
				}
			}

			Button b = root.FindViewById<Button> (Resource.Id.bRandomButton);
			b.Text = buttonText;
			b.Click += delegate(object sender, EventArgs e) {
				//mAdapter.NotifyDataSetChanged();
				//Toast.MakeText (this.Activity, "Length of adapter array is "+mAdapter.sizeOfMyShows(), ToastLength.Short).Show ();
				setUpAdapter();
			};


			mGridView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				var intent = new Intent (this.Activity, typeof(ShowDetailsActivity));
				intent.PutExtra ("TVDBID", "" + showsJToken [e.Position] ["id"]);
				StartActivity (intent);


//				Toast.MakeText (this.Activity, "I clicked on position " + e.Position + " with TVDBID of "
//				+ showsJToken [e.Position] ["id"], ToastLength.Short).Show ();
			};

			return root;
		}

		public override void OnPause ()
		{
			base.OnPause ();
		}

		public override void OnResume ()
		{
			base.OnResume ();
			//TODO
			//Need to notify adapter to update all values


		}

		public override void OnSaveInstanceState (Bundle outState)
		{
			List<string> myList = mAdapter.getTrakkedShows ();

			if (myList.Count != 0) {

				string[] myArray = new string[myList.Count];
				for (int i = 0; i < myList.Count; i++) {
					myArray [i] = myList [i];

				}

				outState.PutStringArray ("TrakkedList", myArray);
			}

			Console.WriteLine ("##$$###$$### I Saved my bundle with size " + myList.Count);

		}


		void setUpAdapter ()
		{
			mAdapter = new AddShowsAdapter (this.Activity);
			mGridView.Adapter = mAdapter;
			PAGENUM = 1;
			var myTask = populateArrayOfShows ();
		}

		void getNextPage ()
		{
			PAGENUM++;
			if (PAGENUM < 4) {
				var myTask = populateArrayOfShows ();
			}
		}

		public async Task populateArrayOfShows ()
		{
			String myURL = "";
			URLManufacturer mewManufacturer = new URLManufacturer ();
			IURLBuilder urlBuilder = null;
			urlBuilder = new DiscoverTVURLBuilder (PAGENUM);

			mewManufacturer.Construct (urlBuilder);

			string[] sortByParam = {
				TMDBConstants.SORTBY,
				TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.VoteAverageDesc)
			};
			urlBuilder.AddDesiredParameters (sortByParam);
			string[] voteCountGTE = {
				TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.VoteCountGTE), 
				10.ToString ()
			};
			urlBuilder.AddDesiredParameters (voteCountGTE);
			if (!title.Equals ("Popular")) {
				string[] genres = {
					TMDBConstants.GetEnumDescription (TMDBConstants.DiscoverTV.WithGenres), 
					"" + getGenreValue (title)
				};
				urlBuilder.AddDesiredParameters (genres);
			}
			myURL = urlBuilder.BuiltURL;

			Console.WriteLine ("##################Is Popular with url = " + myURL);	

			var client = new HttpClient ();
			var data = await client.GetStringAsync (myURL);
			JObject o = JObject.Parse (data);

			for (int i = 0; i < o ["results"].Count (); i++) {
				showsJToken.Add (o ["results"] [i]);
				mAdapter.AddShow (o ["results"] [i]);
				mAdapter.NotifyDataSetChanged ();
			}
			if (PAGENUM < 7) {
				getNextPage ();
			}
		}


		int getGenreValue (string title)
		{
			switch (title) {

			case "Action":
				return (int)com.robobat.TheMovieDB.TMDBConstants.Genres.Action;
			
			case "SciFi":
				return (int)com.robobat.TheMovieDB.TMDBConstants.Genres.SciFi;

			case "Drama":
				return (int)com.robobat.TheMovieDB.TMDBConstants.Genres.Drama;

			case "Mystery":
				return (int)com.robobat.TheMovieDB.TMDBConstants.Genres.Mystery;

			case "Comedy":
				return (int)com.robobat.TheMovieDB.TMDBConstants.Genres.Comedy;

			case "Animation":
				return (int)com.robobat.TheMovieDB.TMDBConstants.Genres.Animation;

			default:
				return (int)com.robobat.TheMovieDB.TMDBConstants.Genres.Soap;

			}
		}
	}
}

