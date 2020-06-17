
using System;
using System.Threading.Tasks;
using TPass.Api;
using TPass.Models;
using TPass.Services;
using TPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
namespace TPass.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BehaviorProfileView : ContentPage, IView
    {

        BehaviorProfileViewModel vm;
        K12RestApi api = null;
        public string Name { get => "behaviorprofileview"; set => throw new NotImplementedException(); }

        public BehaviorProfileView(string studentID, string BehaviorReason)
        {
            InitializeComponent();
            BindingContext = vm = new BehaviorProfileViewModel(studentID, BehaviorReason);
            vm.Nav = this;

            KeyboardService.HideKeyboard();
        }

        async Task SelectBehaviorAction()
        {
            //if (manualSelected) return;            

            if (api == null)
            {
                api = new K12RestApi();
            }


            var behaviors = await api.GetBehaviorTypes();

            if (behaviors.Count() < 1 || behaviors == null)
            {
                await DisplayAlert("Error", "Could not find any behavior items.", "OK");
                return;
            }

            var behavior = await DisplayActionSheet("Select Behavior", null, null, (from x in behaviors select x.Description).ToArray());
            if (behavior.ToLower() == "cancel")
            {
                return;
            }

            var selectedBehavior = (from x in behaviors
                                    where x.Description == behavior
                                    select x).FirstOrDefault();

            api.SetCurrentBehavior(selectedBehavior);



        }
        public void ExecuteNavigation()
        {
            throw new NotImplementedException();
        }

        public async void ExecuteNavigation(string name)
        {
            if (name == "back")
            {
                await this.Navigation.PopAsync();
            }
        }

        public void ShowAlert(string title, string message)
        {
            DisplayAlert(title, message, "OK");
        }

        public Page CurrentView()
        {
            throw new NotImplementedException();
        }

        public async void Reload(string data)
        {
            await SelectBehaviorAction();
            vm.Reload(data);
        }

        public BehaviorProfileView(StudentDetails details, string behaviorReason)
        {
            InitializeComponent();
            BindingContext = vm = new BehaviorProfileViewModel(details, behaviorReason);
            vm.Nav = this;
            KeyboardService.HideKeyboard();

        }

    }
}
