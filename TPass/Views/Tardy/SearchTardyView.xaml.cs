using System;
using System.Linq;
using TPass.Api;
using TPass.ViewModels;
using Xamarin.Forms;

namespace TPass.Views
{

    public partial class SearchTardyView : ContentPage, IView
    {
        K12RestApi api;
        SearchTardyViewModel vm;

    

        public string Name { get => "searchtardyview"; set => throw new NotImplementedException(); }

        public SearchTardyView()
        {
            InitializeComponent();
            api = new K12RestApi();
            BindingContext = vm = new SearchTardyViewModel();
           
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

            try
            {
                var details = await api.GetStudentDetails(5, this.txtSearch.Text.Trim());

                vm.IsBusy = false;

                if (details.Count() == 1)
                {

                    await this.Navigation.PushAsync(new TardyProfileView(details.FirstOrDefault(), vm.Excuse));
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();

                }
                else if( details.Count() > 1)
                {
                   
                    await this.Navigation.PushAsync(new SearchResultsView(details, new DataObject("tardy", vm.Excuse)));
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();
                }
                else
                {
                    await DisplayAlert("No results", "No student found with that id or name!", "OK");
                   // await DisplayAlert("No results", "No student found with that id or name!", "OK");
                }

            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Could not retrieve student data.", "OK");
            }
            finally
            {
                vm.IsBusy = false;
            }

        }

        private async void btnSelectExcuseClicked(object sender, EventArgs e)
        {

            var tardytypes = await api.GetTardyTypes();

            if (tardytypes.Count() < 1 || tardytypes == null)
            {
                await DisplayAlert("Error", "Could not find any tardy types.", "OK");
                return;
            }

            var excuse = await DisplayActionSheet("", "CANCEL", "CLEAR", (from x in tardytypes select x.Description).ToArray());

            //var tardytype = await DisplayActionSheet("Select Behavior", null, null, (from x in behaviors select x.Description).ToArray());
            if (excuse.ToLower() == "cancel")
            {
                return;
            }

           
            //var excuse = await DisplayActionSheet("", "CANCEL", "CLEAR", "Medical Note", "Transportation Issue", "Late School Bus");
            if (excuse.ToLower() == "cancel")
            {
                return;
            }

            vm.Excuse = excuse;

            if (vm.Excuse.ToLower() == "clear")
            {
                btnSelectExcuse.Text = "Not Excused";
                vm.Excuse = String.Empty;
                vm.IsExcused = false;
                btnSelectExcuse.BackgroundColor = Color.White;

                btnSelectExcuse.TextColor = Color.Blue;
                btnSelectExcuse.BorderColor = Color.Blue;


            }
            else if (vm.Excuse.Trim().ToLower() == "tardy to class")
            {
                btnSelectExcuse.Text = vm.Excuse;
                btnSelectExcuse.BackgroundColor = Color.Red;
                btnSelectExcuse.TextColor = Color.White;
                btnSelectExcuse.BorderColor = Color.Red;
                vm.IsExcused = false;

            }
            else
            {
                btnSelectExcuse.Text = vm.Excuse;
                btnSelectExcuse.BackgroundColor = Color.Green;
                btnSelectExcuse.TextColor = Color.White;
                btnSelectExcuse.BorderColor = Color.Green;
                vm.IsExcused = true;

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

            vm.IsBusy = true;
            var details = await api.GetStudentDetails(5, data);

            if (details.Count() < 1)
            {
                await DisplayAlert("No results", $"Student id: {data} not found", "OK");
                vm.IsBusy = false;
                return;
            }


            vm.IsBusy = false;
            await this.Navigation.PushAsync(new TardyProfileView(details.FirstOrDefault(), vm.Excuse));
            this.txtSearch.Text = "";
            this.txtSearch.Focus();

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
