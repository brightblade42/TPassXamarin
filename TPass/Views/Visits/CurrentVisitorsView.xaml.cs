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
    public partial class CurrentVisitorsView : ContentPage, IView
    {

        CurrentVisitorsViewModel vm;
        public CurrentVisitorsView()
        {
            InitializeComponent();
            vm = new CurrentVisitorsViewModel(null);
            BindingContext = vm;

            vm.GetCurrentVisits();

        }

        public string Name { get { return "currentvisitorsview"; } set { } }

        public CurrentVisitorsView(IEnumerable<Visitor> results)
        {
            InitializeComponent();
            BindingContext = new CurrentVisitorsViewModel(results);
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
