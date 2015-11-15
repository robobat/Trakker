
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

namespace Trakker
{
	public class AddShowsFragment : Fragment
	{

		public int PAGENUM = 1;

		List<JToken> showsJToken = new List<JToken> ();

		//		private readonly string[] Titles = {
		//			"Popular", "Action", "Sci-Fi", "Drama", "Mystery", "Comedy", "Animation", "Sports"
		//		};
		private int position;
		private string title;
		AddShowsAdapter mAdapter;
		GridView mGridView;

		public static AddShowsFragment NewInstance (int position, string title)
		{
			
			var f = new AddShowsFragment ();
			var b = new Bundle ();
			b.PutInt ("position", position);
			b.PutString ("title", title);
			f.Arguments = b;
			return f;

		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here

			position = Arguments.GetInt ("position");
			title = Arguments.GetString ("title");
		}

		public override View OnCreateView (LayoutInflater inflater, 
		                                   ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var root = inflater.Inflate (Resource.Layout.add_show_gridview_layout, container, false);

			mGridView = root.FindViewById<GridView> (Resource.Id.myGridview);
			setUpAdapter ();

			Button b = root.FindViewById<Button> (Resource.Id.bRandomButton);
			b.Text = "I am in position " + position + " with title " + title;


			mGridView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				Toast.MakeText (this.Activity, "I clicked on position " + e.Position + " with TVDBID of "
				+ showsJToken [e.Position] ["id"], ToastLength.Short).Show ();
			};

			return root;
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
			if (PAGENUM < 7) {
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
				TVConstants.SORTBY,
				TVConstants.GetEnumDescription (TVConstants.DiscoverTV.VoteAverageDesc)
			};
			urlBuilder.AddDesiredParameters (sortByParam);
			string[] voteCountGTE = {
				TVConstants.GetEnumDescription (TVConstants.DiscoverTV.VoteCountGTE), 
				10.ToString ()
			};
			urlBuilder.AddDesiredParameters (voteCountGTE);
			if (!title.Equals ("Popular")) {
				string[] genres = {
					TVConstants.GetEnumDescription (TVConstants.DiscoverTV.WithGenres), 
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
				return (int)com.robobat.TheMovieDB.TVConstants.Genres.Action;
			
			case "Sci-Fi":
				return (int)com.robobat.TheMovieDB.TVConstants.Genres.SciFi;

			case "Drama":
				return (int)com.robobat.TheMovieDB.TVConstants.Genres.Drama;

			case "Mystery":
				return (int)com.robobat.TheMovieDB.TVConstants.Genres.Mystery;

			case "Comedy":
				return (int)com.robobat.TheMovieDB.TVConstants.Genres.Comedy;

			case "Animation":
				return (int)com.robobat.TheMovieDB.TVConstants.Genres.Animation;

			default:
				return (int)com.robobat.TheMovieDB.TVConstants.Genres.Soap;

			}
		}
	}
}

