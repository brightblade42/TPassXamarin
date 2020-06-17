using TPass.XPInterfaces;
using Xamarin.Forms;

namespace TPass.Services
{
    public static class KeyboardService
    {

        public static void HideKeyboard()
        {
            IKeyboardHelper k = DependencyService.Get<IKeyboardHelper>();
            k.HideKeyboard();
        }

    }
}
