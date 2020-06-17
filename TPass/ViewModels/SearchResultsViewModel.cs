using System.Collections.Generic;
using System.Collections.ObjectModel;
using TPass.Models;

namespace TPass.ViewModels
{

    class SearchResultsViewModel : BaseViewModel
    {

        ObservableCollection<StudentDetails> results;

        public SearchResultsViewModel(IEnumerable<StudentDetails> results)
        {
            Results = new ObservableCollection<StudentDetails>(results);

        }

        public ObservableCollection<StudentDetails> Results {
            get { return this.results; }
            set { SetProperty(ref results, value); }
        }



    }
}
