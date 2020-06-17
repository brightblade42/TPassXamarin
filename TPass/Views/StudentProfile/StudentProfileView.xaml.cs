
using TPass.Models;
using TPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using TPass.Services;

namespace TPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfileView : ContentPage, IView
    {
        StudentProfileViewModel vm;
        public StudentProfileView(string studentID)
        {
            InitializeComponent();
            KeyboardService.HideKeyboard();

            BindingContext = vm = new StudentProfileViewModel(studentID);
            vm.Nav = this;

        }

        public StudentProfileView(StudentDetails details)
        {
            InitializeComponent();
            KeyboardService.HideKeyboard();
            BindingContext = vm = new StudentProfileViewModel(details);
            vm.Nav = this;

            //this.SizeChanged += StudentProfileView_SizeChanged;
        }

        public void UpdateScheduleSize()
        {
                var lv = this.FindByName("ScheduleList") as ListView;
            //vm.GroupedSchedule.count
            lv.HeightRequest = (40 * vm.Schedule.Count) + (25 * vm.GroupedSchedule.Count);// + (10 * vm.Schedule.Count);
            
        }

        public string Name { get { return "studentprofileview"; } set { } }

        public Page CurrentView()
        {
            return this.Navigation.NavigationStack.LastOrDefault();
        }

        public void ExecuteNavigation()
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteNavigation(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Reload(string data)
        {
            vm.Reload(data);
        }

        public void ShowAlert(string title, string message)
        {
            DisplayAlert(title, message, "OK");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var lv = sender as ListView;
            await this.Navigation.PushAsync(new ContactDetailsView(lv.SelectedItem as StudentContact));
        }
    }
}
