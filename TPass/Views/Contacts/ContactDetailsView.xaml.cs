
using TPass.Models;
using TPass.ViewModels;
using Xamarin.Forms;

namespace TPass.Views {

    public partial class ContactDetailsView: ContentPage {

	ContactDetailsViewModel vm;
	public ContactDetailsView()
	{
	    InitializeComponent();
	    BindingContext = vm = new ContactDetailsViewModel();
	}

	public ContactDetailsView(StudentContact contact)
	{
	    InitializeComponent();
	    BindingContext = vm = new ContactDetailsViewModel(contact);
	}
    }
}
