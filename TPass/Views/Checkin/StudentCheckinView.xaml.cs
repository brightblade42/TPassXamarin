
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
    public partial class StudentCheckinView : ContentPage, IView
    {

        StudentCheckinViewModel vm;

        public string Name { get => "studentcheckinview"; set => throw new NotImplementedException(); }


        public StudentCheckinView(StudentDetails details)
        {
            InitializeComponent();
            BindingContext = vm = new StudentCheckinViewModel(details);
            vm.Nav = this;
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



    }
}
