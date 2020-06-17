using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TPass.ViewModels
{

    class SearchBehaviorViewModel : BaseViewModel
    {

        public Command SelectBehaviorCommand { get; set; }
        public Command ClearBehaviorCommand { get; set; }

        public SearchBehaviorViewModel()
        {
            SelectBehaviorCommand = new Command(async (x) => await SelectBehavior());
            ClearBehaviorCommand = new Command(async (x) => await ClearBehavior());

        }

        string excuse = String.Empty;

        public string Excuse {
            get { return excuse; }

            set {
                SetProperty(ref excuse, value);
            }
        }

        async Task ClearBehavior()
        {
            await Task.Delay(1000);
        }

        async Task SelectBehavior()
        {
            await Task.Delay(1000);
        }
    }
}
