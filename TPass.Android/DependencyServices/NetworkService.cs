using Android.App;
using Android.Graphics;
using Android.Net;

using Android.Views;
using Android.Widget;
using System;
using TPass.Droid;
using TPass.Models;
using TPass.XPInterfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkService))]
namespace TPass.Droid {
	public class NetworkService : INetworkCheck {


		public NetworkService() { }

		public bool IsOnline()
		{
			return true;
		}
	}
}