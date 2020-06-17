
using TPass.Models;
using TPass.Views;
using Xamarin.Forms;

namespace TPass.Controls
{

    public class ContactCell : ViewCell
    {

        readonly INavigation navigation;
        string id;
        public ContactCell(string id, INavigation navigation = null)
        {

            this.id = id;
            Height = 60;
            StyleId = "disclosure";
            View = new ContactCellView();
            this.navigation = navigation;
        }



        protected override async void OnTapped()
        {

            base.OnTapped();
            if (navigation == null)
                return;

            var contact = BindingContext as StudentContact;
            if (contact == null)
            {
                return;
            }


            await navigation.PushAsync(new ContactDetailsView());
        }
    }

    public partial class ContactCellView : ContentView
    {

        public ContactCellView()
        {
            InitializeComponent();


        }


    }

}
