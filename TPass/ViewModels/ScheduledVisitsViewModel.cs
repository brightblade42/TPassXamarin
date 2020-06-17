using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPass.Api;
using TPass.Models;
using Xamarin.Forms;

namespace TPass.ViewModels
{

    public class ScheduledVisitsViewModel : BaseViewModel
    {

        K12RestApi api;

        ObservableCollection<Visitor> results;

        public ScheduledVisitsViewModel(IEnumerable<Visitor> results)
        {
            if (results == null)
            {
                api = new K12RestApi();
            }

        }

        public async void GetScheduledVisits()
        {
            this.IsBusy = true;

            var visits = await api.GetScheduledVisits(5, DateTime.Now);
            this.results = new ObservableCollection<Visitor>(visits);

            this.IsBusy = false;

            Visitors = new ObservableCollection<Visitor>(results);
        }

        public ObservableCollection<Visitor> Visitors {
            get { return this.results; }
            set { SetProperty(ref results, value); }
        }



    }
}
