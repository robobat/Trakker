
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

namespace Trakker
{
	public class BasicFragment : Fragment
	{
		private int position;

		public static BasicFragment NewInstance (int position)
		{
			var f = new BasicFragment ();
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
			var root = inflater.Inflate (Resource.Layout.basic_fragment_1, container, false);

			return root;
		}
	}
}

