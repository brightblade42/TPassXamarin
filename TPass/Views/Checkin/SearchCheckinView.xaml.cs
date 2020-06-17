using System;
using System.Linq;
using System.Threading.Tasks;
using TPass.Api;
using TPass.ViewModels;
using Xamarin.Forms;

namespace TPass.Views
{

    public partial class SearchCheckinView : ContentPage, IView
    {
        K12RestApi api;
        SearchCheckinViewModel vm;
        bool manualSelected = false;

        public string Name { get => "searchbehaviorview"; set => throw new NotImplementedException(); }

        public SearchCheckinView()
        {
            InitializeComponent();
            api = new K12RestApi();
            BindingContext = vm = new SearchCheckinViewModel();
        }


        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private async void btnSearchClicked(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(this.txtSearch.Text))
            {

                await DisplayAlert("Missing id", "Enter a student id", "OK");
                this.txtSearch.Focus();
                return;
            }

            vm.IsBusy = true;

            var details = await api.GetStudentDetails(5, this.txtSearch.Text.Trim());

            vm.IsBusy = false;

            if (details.Count() == 1)
            {

                await this.Navigation.PushAsync(new StudentCheckinView(details.FirstOrDefault()));
                this.txtSearch.Text = "";
                this.txtSearch.Focus();

            }
            else if ( details.Count() > 1)
            {
                await this.Navigation.PushAsync(new SearchResultsView(details, new DataObject("checkin",null)));
                this.txtSearch.Text = "";
                this.txtSearch.Focus();
            }
                
            else
            {

                await DisplayAlert("No results", "No student found with that id or name!", "OK");
            }

        }


        private void btnScannerClicked(object sender, EventArgs e)
        {

        }

        public void ExecuteNavigation()
        {
            throw new NotImplementedException();
        }

        public async void ExecuteNavigation(string data)
        {
            try
            {

                vm.IsBusy = true;

                var details = await api.GetStudentDetails(5, data);

                if (details.Count() < 1)
                {
                    await DisplayAlert("No results", $"Student id: {data} not found", "OK");
                    return;
                }

                vm.IsBusy = false;

                await this.Navigation.PushAsync(new StudentCheckinView(details.FirstOrDefault()));
                this.txtSearch.Text = "";
                this.txtSearch.Focus();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                vm.IsBusy = false;
            }
        }

        public void ShowAlert(string title, string message)
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
