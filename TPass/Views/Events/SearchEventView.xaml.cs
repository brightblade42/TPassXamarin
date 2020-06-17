using System;
using System.Linq;
using System.Threading.Tasks;
using TPass.Api;
using TPass.ViewModels;
using TPass.Models;
using Xamarin.Forms;

namespace TPass.Views
{

    public partial class SearchEventView : ContentPage, IView
    {
        K12RestApi api;
        SearchEventViewModel vm;
        bool manualSelected = false;

        EventRecord currentEvent;

        public string Name { get => "searcheventview"; set => throw new NotImplementedException(); }

        public SearchEventView(EventRecord eventRecord)
        {
            InitializeComponent();
            api = new K12RestApi();
            this.currentEvent = eventRecord;
            BindingContext = vm = new SearchEventViewModel();

            
        }


        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private async void btnSearchClicked(object sender, EventArgs e)
        {

            //are we on the list or not on the list. 
            //or can we walk in and be recoded. 

            if (String.IsNullOrEmpty(this.txtSearch.Text))
            {

                await DisplayAlert("Missing id", "Enter a student id", "OK");
                this.txtSearch.Focus();
                return;
            }

            vm.IsBusy = true;

            var details = await api.GetStudentDetails(5, this.txtSearch.Text.Trim());

            vm.IsBusy = false;

            if (details.Count() == 1)
            {

                if(currentEvent.WalkIn)
                {
                    HandleWalkinEvent(details.First());
                    this.txtSearch.Text = "";
                    this.txtSearch.Focus();
                    return;
                }
               
                var attendeeList = await api.LoadEventAttendee(currentEvent.EvntID, DateTime.Now);
                //check the list..
                var stud = details.First();
                bool onlist = false;
                var sname = $"{stud.FName.Trim().ToLower()}{stud.LName.Trim().ToLower()}";
                EventAttendeeRec attendeeMatch = null;
                foreach (var attendee in attendeeList)
                {
                    var attName = $"{attendee.FName.Trim().ToLower()}{attendee.LName.Trim().ToLower()}";
                    if (sname == attName)
                    {
                        onlist = true;
                        attendeeMatch = attendee;
                        break;
                    }
                  
                }

                if(onlist)
                {

                    try
                    {
                        int ccode = (int)attendeeMatch.CCode;
                        var signInRecord = await api.LoadCurrentEventCheckIn(ccode, attendeeMatch.EvntID, DateTime.Now);

                        //result null means not checked in or checked out. 
                        if(signInRecord == null)
                        {
                            signInRecord = new SignedEventAttendeeRec();
                            signInRecord.EvntID = currentEvent.EvntID;
                            signInRecord.AttPkID = attendeeMatch.AttPkID;
                            signInRecord.CCode = attendeeMatch.CCode;
                            signInRecord.Date = DateTime.Now;

                            var checkinStat = await api.CheckInEventAttendee(signInRecord);
                            await DisplayAlert("Success", $"{stud.FName} {stud.LName} has been checked in!", "OK");

                        } else if(signInRecord.SignIn == null) {

                            signInRecord.EvntID = attendeeMatch.EvntID;

                            var checkinStat = await api.CheckInEventAttendee(signInRecord);
                            await DisplayAlert("Success", $"{stud.FName} {stud.LName} has been checked in!", "OK");

                        } else if (signInRecord.SignOut ==null)
                        {
                            //do a signout
                            var checkOutStat = await api.CheckOutEventAttendee(signInRecord);
                            await DisplayAlert("Success", $"{stud.FName} {stud.LName} has been checked out!", "OK");

                        } else if (signInRecord.SignOut > signInRecord.SignIn)
                        {
                            var checkinStat = await api.CheckInEventAttendee(signInRecord);
                            await DisplayAlert("Success", $"{stud.FName} {stud.LName} has been checked in!", "OK");
                            //sign back in
                            //dunno.
                        } else
                        {
                            //sign back out. 
                            var checkOutStat = await api.CheckOutEventAttendee(signInRecord);
                            await DisplayAlert("Success", $"{stud.FName} {stud.LName}  has been Checked out!", "OK");
                        }
                        //we need to know if we are signed in or not
                        
                     
                    }
                    catch(Exception ex)
                    {
                        string boom = ex.Message;
                    }
                    //checking in ... 
                }
                else
                {
                    await DisplayAlert("Denied", $"{stud.FName} {stud.LName} is not on the event list!", "OK");
                }
              //  await this.Navigation.PushAsync(new EventCheckinView(details.FirstOrDefault()));
                this.txtSearch.Text = "";
                this.txtSearch.Focus();

            }
            else if ( details.Count() > 1)
            {
                string x = "dont let this happen";
                /*
                await this.Navigation.PushAsync(new SearchResultsView(details, new DataObject("checkin",null)));
                this.txtSearch.Text = "";
                this.txtSearch.Focus();
                */
            }
                
            else
            {

                await DisplayAlert("No results", "No student found with that id or name!", "OK");
            }

        }

        private async void HandleWalkinEvent(StudentDetails sdetails)
        {
            
            var signInRecord = await api.LoadCurrentEventCheckIn(sdetails.CCode, currentEvent.EvntID, DateTime.Now);

            //result null means not checked in or checked out. 
            if (signInRecord == null)
            {
                
                signInRecord = new SignedEventAttendeeRec();                
                signInRecord.EvntID = currentEvent.EvntID;
                //signInRecord.AttPkID = attendeeMatch.AttPkID;
                signInRecord.CCode = sdetails.CCode;
                signInRecord.Date = DateTime.Now;

                var checkinStat = await api.CheckInEventAttendee(signInRecord);
                await DisplayAlert("Check In", $"{sdetails.FName} {sdetails.LName} has been checked in.", "OK");

            }
            else if (signInRecord.SignIn == null)
            {

                signInRecord.EvntID = currentEvent.EvntID;

                var checkinStat = await api.CheckInEventAttendee(signInRecord);
                await DisplayAlert("Check In", $"{sdetails.FName} {sdetails.LName} has been checked in.", "OK");

            }
            else if (signInRecord.SignOut == null)
            {
                //do a signout
                var checkOutStat = await api.CheckOutEventAttendee(signInRecord);
                await DisplayAlert("Check Out", $"{sdetails.FName} {sdetails.LName} has been Checked out!", "OK");

            }
            else if (signInRecord.SignOut > signInRecord.SignIn)
            {
                var checkinStat = await api.CheckInEventAttendee(signInRecord);
                await DisplayAlert("Check In", $"{sdetails.FName} {sdetails.LName} has been checked in.", "OK");
                //sign back in
                //dunno.
            }
            else
            {
                //sign back out. 
                var checkOutStat = await api.CheckOutEventAttendee(signInRecord);
                await DisplayAlert("Check Out", $"{sdetails.FName} {sdetails.LName} has been checked out.", "OK");
            }
            //we need to know if we are signed in or not
        }

        private void btnScannerClicked(object sender, EventArgs e)
        {

        }

        public void ExecuteNavigation()
        {
            throw new NotImplementedException();
        }

        public async void ExecuteNavigation(string data)
        {
            try
            {

                vm.IsBusy = true;

                var details = await api.GetStudentDetails(5, data);

                if (details.Count() < 1)
                {
                    await DisplayAlert("No results", $"Student id: {data} not found", "OK");
                    return;
                }

                vm.IsBusy = false;

              //  await this.Navigation.PushAsync(new EventCheckinView(details.FirstOrDefault()));
                this.txtSearch.Text = "";
                this.txtSearch.Focus();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                vm.IsBusy = false;
            }
        }

        public void ShowAlert(string title, string message)
        {
            throw new NotImplementedException();
        }

        public Page CurrentView()
        {
            throw new NotImplementedException();
        }

        public void Reload(string data)
        {
            throw new NotImplementedException();
        }
    }
}
