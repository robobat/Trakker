
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

namespace Trakker
{
	public class GridViewFragment : Fragment
	{
		private int position;

		public static GridViewFragment NewInstance (int position)
		{

			var f = new GridViewFragment ();
			var b = new Bundle ();
			b.PutInt ("position", position);
			f.Arguments = b;
			return f;

		}



		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here

			position = Arguments.GetInt ("position");
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var root = inflater.Inflate (Resource.Layout.gridview_layout, container, false);

			Button b = root.FindViewById<Button> (Resource.Id.bRandomButton);

			b.Text = "I am in position " + position;

			return root;
		}
	}
}

