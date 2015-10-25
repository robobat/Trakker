using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace Trakker
{
	[Activity (Label = "Trakker", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
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

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}
}


