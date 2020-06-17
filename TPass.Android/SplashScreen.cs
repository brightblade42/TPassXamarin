
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.AppCompat;
using Android.Util;
using System.Threading.Tasks;

namespace TPass.Droid {


    [Activity(
        Theme = "@style/SplashTheme", 
        MainLauncher = true, 
        NoHistory = true)]
    public class SplashScreen : AppCompatActivity
    {
          static readonly string TAG = "X:" + typeof(SplashScreen).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");

        }

	/// <summary>
	/// now is the winter of our discontent. 
	/// it was the best of times, it was the worst of times. 
	
	/// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => {
                Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
                Log.Debug(TAG, "Working in the background - important stuff.");
            });

            startupWork.ContinueWith(t => {
                Log.Debug(TAG, "Work is finished - start MainActivity.");
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}