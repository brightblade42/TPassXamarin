using Xamarin.Forms;

namespace TPass.ViewModels {

    public interface IView
    {
        void ExecuteNavigation();
        void ExecuteNavigation(string name);
        void Reload(string data);
        void ShowAlert(string title, string message);
        Page CurrentView();
        string Name { get; set; }
        
    }
}
