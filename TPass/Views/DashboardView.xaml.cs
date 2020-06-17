using System;
using TPass.ViewModels;
using Xamarin.Forms;

namespace TPass.Views
{
    public partial class DashboardView : ContentPage, IView
    {
        public DashboardView()
        {
            DashboardViewModel dvm;
            InitializeComponent();
            BindingContext = dvm = new DashboardViewModel();
            dvm.Nav = this;

            Title = "Dashboard";
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                NavigationPage.SetHasBackButton(this, false);
                NavigationPage.SetTitleIcon(this, "ic_launcher.png");
            }

        }

        public string Name { get { return "dashboardview"; } set { } }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetFastScanFlag("");

        }
        public void ExecuteNavigation()
        {
            ;
        }

        void SetFastScanFlag(string flag)
        {
            Application.Current.Properties["fastscan"] = flag;
        }


        public async void ExecuteNavigation(string name)
        {
            Page nextPage = null;
            try
            {
                switch (name)
                {

                    case "profile":
                        nextPage = new SearchView();
                        SetFastScanFlag("profile");
                        break;
                    case "behavior":
                        nextPage = new SearchBehaviorView();
                        break;
                    case "checkin":
                        nextPage = new SearchCheckinView();
                        break;
                    case "tardy":
                        nextPage = new SearchTardyView();
                        SetFastScanFlag("tardy");
                        break;
                    case "currentVisits":
                        nextPage = new CurrentVisitorsView();
                        break;
                    case "scheduled":
                        nextPage = new ScheduledVisitsView();
                        break;
                    case "completedVisits":
                        nextPage = new CompletedVisitsView();
                        break;

                    case "currentEvents":
                        nextPage = new CurrentEventsView();
                        break;

                    default:
                        throw new Exception("bad mojo");
                }

                if (nextPage != null)
                {
                    await this.Navigation.PushAsync(nextPage, true);
                }

            }
            catch (Exception ex)
            {
                string wtf = ex.Message;
            }



        }

        public void ShowAlert(string title, string message)
        {
            throw new NotImplementedException();
        }

        private async void StudentProfileClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new SearchView(), true);
        }

        public Page CurrentView()
        {
            throw new NotImplementedException();
        }

        public void Reload(string data)
        {
            throw new NotImplementedException();
        }
    }
}
