using System;
using System.Collections.Generic;
using TPass.Models;
using TPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TPass.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduledVisitsView : ContentPage, IView
    {

        ScheduledVisitsViewModel vm;
        public ScheduledVisitsView()
        {
            InitializeComponent();
            vm = new ScheduledVisitsViewModel(null);
            BindingContext = vm;
            vm.GetScheduledVisits();


        }

        public string Name { get { return "scheduledvisitsview"; } set { } }


        public ScheduledVisitsView(IEnumerable<Visitor> results)
        {
            InitializeComponent();
            BindingContext = new ScheduledVisitsViewModel(results);
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
            ;
        }

        public void ShowAlert(string title, string message)
        {
            throw new NotImplementedException();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            /*
			var lv = sender as ListView;
			var details = (StudentDetails)lv.SelectedItem;
			var stype = search_type.ToLower();

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
