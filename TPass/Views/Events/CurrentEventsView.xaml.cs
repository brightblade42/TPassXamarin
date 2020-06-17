using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TPass.Models;
using TPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TPass.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentEventsView : ContentPage, IView
    {

        CurrentEventsViewModel vm;
        public CurrentEventsView()
        {
            InitializeComponent();
            vm = new CurrentEventsViewModel(null);
            BindingContext = vm;

            try
            {
                vm.GetCurrentEvents();
            }

            catch (Exception ex)
            {
                DisplayAlert("Error!", ex.Message, "OK");
            }

        }

        public string Name { get { return "CurrentEventsView"; } set { } }

        public CurrentEventsView(IEnumerable<EventRecord> results)
        {
            InitializeComponent();
            BindingContext = new CurrentEventsViewModel(results);
        }


        public Page CurrentView()
        {
            throw new NotImplementedException();
        }

        public void ExecuteNavigation()
        {

        }

        public void ExecuteNavigation(string name)
        {
            ; ;
        }

        public void ShowAlert(string title, string message)
        {
            throw new NotImplementedException();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
			var lv = sender as ListView;
			var eventRecord= (EventRecord)lv.SelectedItem;

            await this.Navigation.PushAsync(new SearchEventView(eventRecord));
			//var stype = search_type.ToLower();

            /*
			if (stype == "profile" || String.IsNullOrEmpty(search_type))
				await this.Navigation.PushAsync(new StudentProfileView(details.IDNumber));
			else if (stype == "tardy")
				await this.Navigation.PushAsync(new TardyProfileView(details.IDNumber, details.Reason));

			else
				await this.Navigation.PushAsync(new StudentProfileView(details.IDNumber));
			*/

        }

        public void Reload(string data)
        {
            throw new NotImplementedException();
        }
    }
}
