﻿using System;
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

    public class CompletedVisitsViewModel : BaseViewModel
    {

        K12RestApi api;

        ObservableCollection<Visitor> results;

        public CompletedVisitsViewModel(IEnumerable<Visitor> results)
        {
            if (results == null)
            {
                api = new K12RestApi();

            }

        }

        public async void GetCompletedVisits()
        {
            this.IsBusy = true;

            var visits = await api.GetCompletedVisits(5, DateTime.Now); //GetCurrentVisits(5, DateTime.Now);
            this.results = new ObservableCollection<Visitor>(visits);

            Visitors = new ObservableCollection<Visitor>(results);

            this.IsBusy = false;
        }

        public ObservableCollection<Visitor> Visitors {
            get { return this.results; }
            set { SetProperty(ref results, value); }
        }



    }
}
