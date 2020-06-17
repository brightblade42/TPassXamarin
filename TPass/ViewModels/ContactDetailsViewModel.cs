using TPass.Models;

namespace TPass.ViewModels
{

    public class ContactDetailsViewModel : BaseViewModel
    {

        public ContactDetailsViewModel()
        {

        }

        StudentContact contact;


        public StudentContact Contact { get { return contact; } set { SetProperty(ref contact, value); } }


        public ContactDetailsViewModel(StudentContact contact)
        {
            this.Contact = contact;

            if (string.IsNullOrWhiteSpace(Contact.Phone))
            {
                Contact.Phone = "xxx-xxx-xxxx";
            }

            if (string.IsNullOrWhiteSpace(Contact.Mobile))
            {
                Contact.Mobile = "xxx-xxx-xxxx";
            }

            if (string.IsNullOrWhiteSpace(Contact.PickUpSched))
            {
                Contact.PickUpSched = "Pickup allowed. Schedule not set";
            }

            if (string.IsNullOrWhiteSpace(Contact.CustodyNote))
            {
                Contact.CustodyNote = "Custody Issue. Reason not listed.";

            }

            ValidateAddress();
        }

        bool hasAddress = false;

        public bool HasAddress {
            get { return hasAddress; }
            set {

                SetProperty(ref hasAddress, value);

            }
        }

        void ValidateAddress()
        {
            if (string.IsNullOrWhiteSpace(Contact.Address))
                HasAddress = false;
            else
                HasAddress = true;

        }

    }
}
