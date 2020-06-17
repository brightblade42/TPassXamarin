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

    public class CurrentEventsViewModel : BaseViewModel
    {

        K12RestApi api;

        ObservableCollection<EventRecord> results;

        public CurrentEventsViewModel(IEnumerable<EventRecord> results)
        {
            if (results == null)
            {
                api = new K12RestApi();

            }

        }

        public async void GetCurrentEvents()
        {
            this.IsBusy = true;
            
            var events = await api.GetEvents(5, DateTime.Now);
            this.results = new ObservableCollection<EventRecord>(events);

            Events = new ObservableCollection<EventRecord>(results);

            this.IsBusy = false;
        }

        public ObservableCollection<EventRecord> Events {
            get { return this.results; }
            set { SetProperty(ref results, value); }
        }



    }
}
