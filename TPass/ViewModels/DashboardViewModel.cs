using Xamarin.Forms;

namespace TPass.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        //a whole list of commands yo. 
        public Command GoSearchCommand { get; set; }

        public DashboardViewModel()
        {
            GoSearchCommand = new Command<string>(x =>
            {

                Nav.ExecuteNavigation(x);

            });

        }








    }
}