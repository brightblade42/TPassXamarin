using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FFImageLoading.Forms.Platform;
//using FFImageLoading.Forms;
//using Symbol.XamarinEMDK;
//using Symbol.XamarinEMDK.Barcode;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TPass.Droid
{

    [Activity(Label = "TPass",
          
        Theme = "@style/MyTheme", 
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	//, EMDKManager.IEMDKListener {
	{
     

		//EMDKManager emdkManager;
		//BarcodeManager barcodeManager;
		//public Scanner scanner;

		public const int MESSAGE_STATE_CHANGE = 1;
		public const int MESSAGE_READ = 2;
		public const int MESSAGE_WRITE = 3;
		public const int MESSAGE_DEVICE_NAME = 4;
		public const int MESSAGE_TOAST = 5;

		public readonly Java.Util.UUID MY_UUID = Java.Util.UUID.RandomUUID();
		// Key names received from the BluetoothService Handler
		public const string DEVICE_NAME = "device_name";
		public const string TOAST = "toast";

		// Intent request codes
		// TODO: Make into Enums
		private const int REQUEST_CONNECT_DEVICE = 1;
		private const int REQUEST_ENABLE_BT = 2;

		const string profileName = "NJBS2017";


		protected override void AttachBaseContext(Context @base)
		{
			
			base.AttachBaseContext(@base);
			 
		}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnCreate(Bundle bundle)
		{
           
            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

//			var results = EMDKManager.GetEMDKManager(Android.App.Application.Context, this);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			CachedImageRenderer.Init(true);

			LoadApplication(new App());

			//var ph = new PrintService();
			

		}

		protected override void OnResume()
		{
			base.OnResume();

			InitScanner();
		}

		protected override void OnPause()
		{
			base.OnPause();
			DeinitScanner();

		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			/*
			if (this.emdkManager != null) {
				this.emdkManager.Release();
				this.emdkManager = null;
			}*/
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			//ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		public void OnClosed()
		{
			/*if (this.emdkManager != null) {
				this.emdkManager.Release();
				this.emdkManager = null;
			}*/

		}

		/*
		public void OnOpened(EMDKManager emdkMgr)
		{
			//this.emdkManager = emdkMgr;

			//InitScanner();
		}
		*/
		void InitScanner()
		{
		/*	if (emdkManager != null) {

				if (barcodeManager == null) {
					try {

						//Get the feature object such as BarcodeManager object for accessing the feature.
						barcodeManager = (BarcodeManager)emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);

						scanner = barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);

						if (scanner != null) {

							//Attahch the Data Event handler to get the data callbacks.
							scanner.Data += scanner_Data;

							//Attach Scanner Status Event to get the status callbacks.
							scanner.Status += scanner_Status;

							scanner.Enable();
						

						} else {
							displayStatus("Failed to enable scanner.\n");
						}
					}
					catch (ScannerException e) {
						displayStatus("Error: " + e.Message);
					}
					catch (Exception ex) {
						displayStatus("Error: " + ex.Message);
					}
				}
			}
			*/
		}

		void DeinitScanner()
		{
			/*if (emdkManager != null) {

				if (scanner != null) {
					try {

						scanner.Data -= scanner_Data;
						scanner.Status -= scanner_Status;

						scanner.Disable();


					}
					catch (ScannerException e) {
						string err = $"Scanner failed to detach : {e.Message}";

					}
				}

				if (barcodeManager != null) {
					emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
				}

				barcodeManager = null;
				scanner = null;
			}*/



		}

		/*
		void scanner_Data(object sender, Scanner.DataEventArgs e)
		{
			ScanDataCollection scanDataCollection = e.P0;

			if ((scanDataCollection != null) && (scanDataCollection.Result == ScannerResults.Success)) {
				IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();

				if (scanData.Count > 0) {
					displaydata(scanData[0].Data);

				}

			
			}

		}
		*/

			/*
		void scanner_Status(object sender, Scanner.StatusEventArgs e)
		{
			String statusStr = "";

			//EMDK: The status will be returned on multiple cases. Check the state and take the action.
			StatusData.ScannerStates state = e.P0.State;

			if (state == StatusData.ScannerStates.Idle) {
				statusStr = "Scanner is idle and ready to submit read.";
				try {
					if (scanner.IsEnabled && !scanner.IsReadPending) {
						scanner.Read();
					}
				}
				catch (ScannerException e1) {
					statusStr = e1.Message;
				}
			}
			if (state == StatusData.ScannerStates.Waiting) {
				statusStr = "Waiting for Trigger Press to scan";
			}
			if (state == StatusData.ScannerStates.Scanning) {
				statusStr = "Scanning in progress...";
			}
			if (state == StatusData.ScannerStates.Disabled) {
				statusStr = "Scanner disabled";
			}
			if (state == StatusData.ScannerStates.Error) {
				statusStr = "Error occurred during scanning";

			}
			displayStatus(statusStr);



		}
		*/
		private void displayStatus(string v)
		{
			var placeholder = v;
		}


		private void displaydata(string v)
		{
			MessagingCenter.Send<object, string>(this, "data", v);
		}
	}
	public class AndroidBug5497WorkaroundForXamarinAndroid {

		// For more information, see https://code.google.com/p/android/issues/detail?id=5497
		// To use this class, simply invoke assistActivity() on an Activity that already has its content view set.

		// CREDIT TO Joseph Johnson (http://stackoverflow.com/users/341631/joseph-johnson) for publishing the original Android solution on stackoverflow.com

		public static void assistActivity(Activity activity)
		{
			new AndroidBug5497WorkaroundForXamarinAndroid(activity);
		}

		private Android.Views.View mChildOfContent;
		private int usableHeightPrevious;
		private FrameLayout.LayoutParams frameLayoutParams;

		private AndroidBug5497WorkaroundForXamarinAndroid(Activity activity)
		{
			FrameLayout content = (FrameLayout)activity.FindViewById(Android.Resource.Id.Content);
			mChildOfContent = content.GetChildAt(0);
			ViewTreeObserver vto = mChildOfContent.ViewTreeObserver;
			vto.GlobalLayout += (object sender, EventArgs e) => {
				possiblyResizeChildOfContent();
			};
			frameLayoutParams = (FrameLayout.LayoutParams)mChildOfContent.LayoutParameters;
		}

		private void possiblyResizeChildOfContent()
		{
			int usableHeightNow = computeUsableHeight();
			if (usableHeightNow != usableHeightPrevious) {
				int usableHeightSansKeyboard = mChildOfContent.RootView.Height;
				int heightDifference = usableHeightSansKeyboard - usableHeightNow;

				frameLayoutParams.Height = usableHeightSansKeyboard - heightDifference;

				mChildOfContent.RequestLayout();
				usableHeightPrevious = usableHeightNow;
			}
		}

		private int computeUsableHeight()
		{
			Rect r = new Rect();
			mChildOfContent.GetWindowVisibleDisplayFrame(r);
			if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) {
				return (r.Bottom - r.Top);
			}
			return r.Bottom;
		}


        
	}
}

