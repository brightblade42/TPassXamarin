using System;
using System.Linq;
using TPass.Api;
using TPass.ViewModels;
using Xamarin.Forms;

namespace TPass.Views {

	public partial class SearchView : ContentPage, IView {
		K12RestApi api;
		SearchViewModel vm;


        public string Name { get { return "searchview";  }  set { } }


		public SearchView()
		{
			InitializeComponent();
			api = new K12RestApi();

			BindingContext = vm = new SearchViewModel();
            vm.Nav = this;
			this.txtSearch.Focus();

            		
		}


		//this should be a commmand. 
		private async void btnSearchClicked(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(this.txtSearch.Text)) {
				await DisplayAlert("Missing id", "Enter a student id", "OK");
				return;

			}

			vm.IsBusy = true;

            try
            {
                var details = await api.GetStudentDetails(5, this.txtSearch.Text.Trim());

               

                //pass the model directly..seems wrong though. 
                if (details.Count() == 1)
                {
                    await this.Navigation.PushAsync(new StudentProfileView(details.FirstOrDefault()));
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();

                }
                else if (details.Count() > 1)
                {

                    await this.Navigation.PushAsync(new SearchResultsView(details));
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();

                }
                else
                {

                    await DisplayAlert("No results", "No student found with that id or name!", "OK");
                    this.txtSearch.Focus();
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Could not retrieve student data.","OK");
            }
            finally
            {
                vm.IsBusy = false;
            }

		}

		private async void btnScannerClicked(object sender, EventArgs e)
		{
            /*
			var scanPage = new ZXing.Net.Mobile.Forms.ZXingScannerPage();
			await this.Navigation.PushAsync(scanPage);

			scanPage.OnScanResult += (result) => {
				// Stop scanning
				scanPage.IsScanning = false;

				// Pop the page and show the result
				Device.BeginInvokeOnMainThread(async () => {
					await Navigation.PopAsync();

					int res;
					bool isnum = int.TryParse(result.Text.Trim(), out res);
					if (isnum) {

						await this.Navigation.PushAsync(new StudentProfileView(result.Text));
						

					} else {
						await DisplayAlert("Scanned Barcode", result.Text, "OK");
						this.txtSearch.Text = "";
						this.txtSearch.Focus();
					}
				});
			}; */
		}

		public async void ExecuteNavigation()
		{
			//svm.IsBusy = true;
			await this.Navigation.PushAsync(new StudentProfileView(this.txtSearch.Text));
		}

        public void ShowAlert(string title,string message)
        {

            DisplayAlert(title,message,"OK");
        }

		public async void ExecuteNavigation(string data)
		{

            try
            {

                vm.IsBusy = true;
                var details = await api.GetStudentDetails(5, data);
                vm.IsBusy = false;

                if (details.Count() > 0)
                {
                    await this.Navigation.PushAsync(new StudentProfileView(details.FirstOrDefault()));
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();

                }
                else
                {
                    await DisplayAlert("No results", $"Student id: {data} not found", "OK");

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert($"Bad Scan : {data}", $"Error Msg : {ex.Message}", "OK");
            }
        }

        public Page CurrentView()
        {
            return this.Navigation.NavigationStack.LastOrDefault();
            //throw new NotImplementedException();
        }

        public void Reload(string data)
        {
            throw new NotImplementedException();
        }
    }
}
