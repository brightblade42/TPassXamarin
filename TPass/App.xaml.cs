using System.Linq;
using Xamarin.Forms;
using TPass.ViewModels;
using System;

/*
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Distribute;
*/
namespace TPass
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TPass.Views.LoginView())
            {

            };

            InitScannerDispatch();
        }

        protected override void OnStart()
        {
            /* MobileCenter.Start("android=f2da4d2f-0a3a-4d2f-9cae-c230aa77e4e9;" +
                    "uwp={Your UWP App secret here};" +
                    "ios={Your iOS App secret here}",
                    typeof(Analytics), typeof(Crashes));*/
            //, typeof(Distribute));

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        void InitScannerDispatch()
        {
            var pg = MainPage;


            MessagingCenter.Subscribe<object, string>(this, "data", (sender, args) =>
            {

                var scancode = args;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {

                    //where are we when the hardware scanner was used?
                    var lastView = pg.Navigation.NavigationStack.LastOrDefault() as IView;

                    if (lastView == null)
                    {
                        //hmmm.
                        return;
                    }


                    switch (lastView.Name.ToLower())
                    {


                        case "searchview":
                        case "searchtardyview":
                        case "searchcheckinview":
                        case "searchbehaviorview":
                            lastView.ExecuteNavigation(scancode);
                            break;

                        case "studentprofileview":
                        case "tardyprofileview":
                        case "studentcheckinview":
                        case "behaviorprofileview":
                            lastView.Reload(scancode);
                            break;
                  
                        default:
                            break;

                    }

                });
            });


        }
    }
}
