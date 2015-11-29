using System;
using Android.Widget;
using Android.Views;
using Android.App;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace Trakker
{
	public class AddShowsAdapter: BaseAdapter
	{
		private readonly Activity activity;

		List<string> myTitles = new List<string> ();

		List<JToken> myShows = new List<JToken> ();

		List<string> mTrakkedShows = new  List<string> ();

		public AddShowsAdapter (Activity a)
		{
			activity = a;

		}

		public void AddShow (JToken showJSON)
		{

			myShows.Add (showJSON);


		}

		public int sizeOfMyShows(){
			return myShows.Count;
		}

		public List<string> getTrakkedShows ()
		{
			return mTrakkedShows;
		}

		public void setTrakkedShows (List<string> list)
		{
			mTrakkedShows = list;
		}

		public void AddShows (List<JToken> showJSON)
		{

			foreach (JToken show in showJSON) {
				myShows.Add (show);
			}


		}

		public void populateStringArray (string[] titles)
		{
			foreach (string title in titles) {
				myTitles.Add (title);
			}
		}


		public override int Count {
			get { return myShows.Count; }
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return 0;
		}

		public class ViewHolder : Java.Lang.Object
		{
			public TextView tvShowTitle { get; set; }

			public ImageView ivShowThumbNail { get; set; }

			public ImageView ivShowIsTrakked { get; set; }

		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View gridView = convertView;
			ViewHolder holder;
			if (gridView == null) {
				gridView = LayoutInflater.From (parent.Context).Inflate (Resource.Layout.add_shows_gridview_item, parent, false);

				holder = new ViewHolder ();

				holder.tvShowTitle = gridView.FindViewById<TextView> (Resource.Id.myTitleName);
				holder.ivShowThumbNail = gridView.FindViewById<ImageView> (Resource.Id.showThumbNail);

				holder.ivShowThumbNail.Click += (object sender, EventArgs e) => {
					Toast.MakeText (activity, "Position " + position + " was clicked", ToastLength.Short).Show ();
				};

				holder.ivShowIsTrakked = gridView.FindViewById<ImageView> (Resource.Id.trakkedButton);

				holder.ivShowIsTrakked.Click += (object sender, EventArgs e) => {
					
					holder.ivShowIsTrakked.SetImageResource (Android.Resource.Drawable.ButtonStarBigOn);
					mTrakkedShows.Add ("" + myShows [position] ["id"]);

					Toast.MakeText (activity, "Star was clicked with id " + myShows [position] ["id"] + " and size is " + mTrakkedShows.Count, ToastLength.Short).Show ();
				};

				gridView.Tag = holder;
			} else {
				holder = gridView.Tag as ViewHolder;
			}

			holder.tvShowTitle.Text = "" + myShows [position] ["name"] +
			" With ID = " + myShows [position] ["id"];

			Koush.UrlImageViewHelper.SetUrlDrawable (holder.ivShowThumbNail, 
				"http://image.tmdb.org/t/p/w92" + myShows [position] ["poster_path"]);





			return gridView;

		}

		
	}
}

