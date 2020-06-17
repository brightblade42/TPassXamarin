using System.Threading.Tasks;
using TPass.Api;
using Xamarin.Forms;

namespace TPass.ViewModels
{

    public class LoginViewModel : BaseViewModel
    {

        string name = "";
        string password = "";

        public LoginViewModel()
        {
            LoginCommand = new Command(async (x) => await Login());
        }

        public string UserName {
            get { return name; }
            set {
                SetProperty(ref name, value);
            }
        }

        public string Password {
            get { return password; }
            set {

                SetProperty(ref password, value);
            }
        }

        public Command LoginCommand { get; set; }

        async Task Login()
        {
            IsBusy = true;
            K12RestApi api = new K12RestApi();
            string user = this.UserName;
            string pwd = this.Password;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pwd))
            {
                IsBusy = false;
                Nav.ShowAlert("Login failure", "Username and password cannot be blank.");

                return;
            }
            try
            {
                var credentials = api.PrepareCredentials(user, password);
                api.SetAuthToken(credentials);
                var isValid = await api.ValidateCredentials(credentials);

                if (isValid)
                {
                    api.SetAuthToken(credentials);
                    Nav.ExecuteNavigation();

                }
                else
                {

                    Nav.ShowAlert("Bad Login", "Username and/or password is incorrect.");
                }
            }
            catch (System.Exception ex)
            {
                string mm = ex.Message;
                Nav.ShowAlert("Error", mm);
            }

            finally
            {
                IsBusy = false;

            }

        }



    }
}
