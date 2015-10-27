using System;
using Android.App;
using Android.Runtime;
using Parse;
using com.robobat.ParseObjectsTrakker;

namespace Trakker
{
	[Application]
	public class App : Application
	{
		public App (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();

			// Initialize the parse client with your Application ID and .NET Key found on
			// your Parse dashboard
			ParseObject.RegisterSubclass<TVShowForParse> ();
			ParseClient.Initialize ("HiUplm2D7dOr9FC0pAmbZBLKasF2PVPf0mpeKhje",
				"efWpCUqTT1av8SmZkrBlU2FcUZe2az0SL6LYGNJG");
		}
	}
}