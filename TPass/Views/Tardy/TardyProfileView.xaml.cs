
using System;
using System.Threading.Tasks;
using TPass.Models;
using TPass.Services;
using TPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TPass.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TardyProfileView : ContentPage, IView
    {

        TardyProfileViewModel vm;

        public string Name { get => "tardyprofileview"; set => throw new NotImplementedException(); }

        public TardyProfileView(string studentID, string tardyReason)
        {
            InitializeComponent();
            BindingContext = vm = new TardyProfileViewModel(studentID, tardyReason);
            vm.Nav = this;
            byte[] spng;

            Task.Run(async () =>
            {
                spng = await StudentImage.GetImageAsPngAsync();
                var imgtxt = System.Convert.ToBase64String(spng);
            });


            KeyboardService.HideKeyboard();
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

        public void Reload(string data)
        {
            vm.Reload(data);
        }

        public TardyProfileView(StudentDetails details, string tardyReason)
        {
            InitializeComponent();
            BindingContext = vm = new TardyProfileViewModel(details, tardyReason);
            vm.Nav = this;
            KeyboardService.HideKeyboard();
            
        }




    }
}
