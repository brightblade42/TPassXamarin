using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using TPass.Droid;
using TPass.XPInterfaces;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(KeyboardService))]
namespace TPass.Droid { 

    public class KeyboardService : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            var context = Forms.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }
    }

}
