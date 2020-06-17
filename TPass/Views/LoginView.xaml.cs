using System;
using System.Linq;
using TPass.Api;
using TPass.ViewModels;
using Xamarin.Forms;


namespace TPass.Views
{

    public partial class LoginView : ContentPage, IView
    {

        LoginViewModel lvm;

        public LoginView()
        {
            InitializeComponent();
            BindingContext = lvm = new LoginViewModel();
            lvm.Nav = this; //ewe

        }

        public string Name { get { return "loginview"; } set { } }


        public async void ExecuteNavigation()
        {
            try
            {
                K12RestApi api = new K12RestApi();
                var companies = await api.GetCompanies();
                if (companies.Count() < 1 || companies == null)
                {
                    throw new Exception("Could not find any buildings associated with login.");
                }
                var selection = await DisplayActionSheet("Select building", null, null, (from x in companies select x.Name).ToArray());
                var selectedCompany = (from x in companies
                                       where x.Name == selection
                                       select x).FirstOrDefault();

                api.SetCurrentCompany(selectedCompany);

                var d = new DashboardView();

                await this.Navigation.PushAsync(d, true);
                this.Navigation.RemovePage(this);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                lvm.IsBusy = false;
            }
        }



        public void ShowAlert(string title, string message)
        {

            DisplayAlert(title, message, "OK");
        }

        public void ExecuteNavigation(string name)
        {
            throw new NotImplementedException();
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
