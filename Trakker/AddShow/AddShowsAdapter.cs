using System;
using Android.Widget;
using Android.Views;
using Android.App;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Trakker
{
	public class AddShowsAdapter: BaseAdapter
	{
		private readonly Activity activity;

		List<string> myTitles = new List<string> ();

		List<JToken> myShows = new List<JToken> ();

		public AddShowsAdapter (Activity a)
		{
			activity = a;

		}

		public void AddShow (JToken showJSON)
		{

			myShows.Add (showJSON);


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
					Toast.MakeText (activity, "Star was clicked", ToastLength.Short).Show ();
				};

				gridView.Tag = holder;
			} else {
				holder = gridView.Tag as ViewHolder;
			}

			holder.tvShowTitle.Text = "" + myShows [position % myShows.Count] ["name"] +
			" With ID = " + myShows [position % myShows.Count] ["id"];

			Koush.UrlImageViewHelper.SetUrlDrawable (holder.ivShowThumbNail, 
				"http://image.tmdb.org/t/p/w92" + myShows [position % myShows.Count] ["poster_path"]);





			return gridView;

		}

		
	}
}

