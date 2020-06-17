using System;
using System.Linq;
using System.Threading.Tasks;
using TPass.Api;
using TPass.ViewModels;
using Xamarin.Forms;

namespace TPass.Views
{

    public partial class SearchBehaviorView : ContentPage, IView
    {
        K12RestApi api;
        SearchBehaviorViewModel vm;
        bool manualSelected = false;

        public string Name { get => "searchbehaviorview"; set => throw new NotImplementedException(); }

        public SearchBehaviorView()
        {
            InitializeComponent();
            api = new K12RestApi();
            BindingContext = vm = new SearchBehaviorViewModel();
        }


        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private async void btnSearchClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtSearch.Text))
            {

                await DisplayAlert("Missing id", "Enter a student id", "OK");
                this.txtSearch.Focus();
                return;
            }


            //await SelectBehaviorAction();

            this.ExecuteNavigation(this.txtSearch.Text);

        }

        private async void btnSelectBehaviorClicked(object sender, EventArgs e)
        {
            manualSelected = false;
            await SelectBehaviorAction();
            manualSelected = true;

        }

        async Task SelectBehaviorAction()
        {
            string behavior = "";
            try
            {

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


                behavior = await DisplayActionSheet("Select Behavior", null, null, (from x in behaviors select x.Description).ToArray());



                if (behavior == null || behavior.ToLower() == "cancel")
                {
                    this.txtSearch.Text = "";
                    return;
                }

                var selectedBehavior = (from x in behaviors
                                        where x.Description == behavior
                                        select x).FirstOrDefault();

                api.SetCurrentBehavior(selectedBehavior);
                vm.Excuse = behavior;
            }
            catch (Exception ex)
            {
                behavior = "cancel";
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
                    await DisplayAlert("No results", $"Student Lookup: {data} not found", "OK");
                    return;
                }

                await SelectBehaviorAction();

                vm.IsBusy = false;

                if (String.IsNullOrEmpty(vm.Excuse))
                {
                    //this.txtSearch.Text = "";
                    this.txtSearch.Focus();
                    return;
                }

                if (details.Count() == 1)
                {
                    await this.Navigation.PushAsync(new BehaviorProfileView(details.FirstOrDefault(), vm.Excuse));
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();
                }
                else if( details.Count() > 1)
                {

                    await this.Navigation.PushAsync(new SearchResultsView(details, new DataObject("behavior", vm.Excuse)));
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();
                }
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
            ;
        }
    }
}
