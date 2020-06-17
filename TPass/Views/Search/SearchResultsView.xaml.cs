using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TPass.ViewModels;
using TPass.Models;

namespace TPass.Views {

    public class DataObject
    {
        public DataObject() { }
        public DataObject(string searchType, string data)
        {
            this.SearchType = searchType;
            this.Data = data;
        }
        public string SearchType { get; set; }
        public string Data { get; set; }
    }
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchResultsView : ContentPage {

		string search_type = "";
        DataObject dataObject;

		public SearchResultsView(IEnumerable<StudentDetails> results)
		{
			InitializeComponent();
			BindingContext = new SearchResultsViewModel(results);
		}

        public SearchResultsView(IEnumerable<StudentDetails> results, DataObject dataObject)
        {
            InitializeComponent();
            BindingContext = new SearchResultsViewModel(results);
            this.dataObject = dataObject;
        }

        async Task<bool> GetConfirmation(string message)
        {
           
            var confirmed = await DisplayAlert("Confirm", message, "Yes", "No");
            return confirmed;
        }
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var lv = sender as ListView;
			var details = (StudentDetails)lv.SelectedItem;
            
			var stype = dataObject?.SearchType.ToLower();
            var data = dataObject?.Data;

            if (stype == "profile")
                await this.Navigation.PushAsync(new StudentProfileView(details.IDNumber));
            else if (stype == "tardy")
            {
                var name = $"{details.FName} {details.MName} {details.LName}";
                var msg = $"Are you sure you want to assign {dataObject.Data} to {name}?";

                if (await GetConfirmation(msg))
                {
                    await this.Navigation.PushAsync(new TardyProfileView(details, dataObject.Data));
                }
                else
                {
                    return;
                }
            }
            else if (stype == "behavior")
            {

                var name = $"{details.FName} {details.MName} {details.LName}";
                var msg = $"Are you sure you want to assign {dataObject.Data} behavior to {name}?";

                if (await GetConfirmation(msg))
                {
                    await this.Navigation.PushAsync(new BehaviorProfileView(details, dataObject.Data));
                }
                else
                {
                    return;
                }
            }
            else if (stype == "checkin")
            {

                var name = $"{details.FName} {details.MName} {details.LName}";
                var msg = $"Are you sure you want to check {name} in or out?";

                if (await GetConfirmation(msg))
                {
                    await this.Navigation.PushAsync(new StudentCheckinView(details));
                }
                else
                {
                    return;
                }
                
              //  await this.Navigation.PushAsync(new StudentProfileView(details.IDNumber));
            }
            else
                await this.Navigation.PushAsync(new StudentProfileView(details.IDNumber));
			/*
		else if (stype == "checkin")
			await this.Navigation.PushAsync(new StudentProfileView(details.IDNumber));

		else if (stype == "behavior")
			await this.Navigation.PushAsync(new StudentProfileView(details.IDNumber));

		*/
		}
	}


}
