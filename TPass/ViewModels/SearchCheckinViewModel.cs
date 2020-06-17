using System;

namespace TPass.ViewModels
{

    class SearchCheckinViewModel : BaseViewModel
    {


        public SearchCheckinViewModel()
        {

        }

        string excuse = String.Empty;

        public string Excuse {
            get { return excuse; }

            set {
                SetProperty(ref excuse, value);
            }
        }

      
    }
}
