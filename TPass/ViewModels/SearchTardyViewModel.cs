using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TPass.ViewModels
{

    class SearchTardyViewModel : BaseViewModel
    {

        public Command SelectExcuseCommand { get; set; }
        public Command ClearExcuseCommand { get; set; }

        public SearchTardyViewModel()
        {
            SelectExcuseCommand = new Command(async (x) => await SelectExcuse());
            ClearExcuseCommand = new Command(async (x) => await ClearExcuse());

        }

        string excuse = String.Empty;
        bool isExcused = false;
        public bool IsExcused { get => isExcused; set => isExcused = value; }

        public string Excuse {
            get { return excuse; }

            set {
                SetProperty(ref excuse, value);
            }
        }

        async Task ClearExcuse()
        {

            await Task.Delay(2000);
        }

        async Task SelectExcuse()
        {

            await Task.Delay(3000);
        }
    }
}
