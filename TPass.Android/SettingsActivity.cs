
using Android.App;
using Android.OS;
using Android.Preferences;

namespace TPass.Droid
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : PreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           // AddPreferencesFromResource(Resource.Xml.preferences);
            // Create your application here
        }
    }
}