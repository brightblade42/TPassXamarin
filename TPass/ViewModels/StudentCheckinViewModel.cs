using System;
using System.Linq;
using System.Threading.Tasks;
using TPass.Api;
using TPass.Models;
using Xamarin.Forms;

namespace TPass.ViewModels
{

    public class StudentCheckinViewModel : BaseViewModel
    {

        K12RestApi api = null;
        StudentDetails details;
        StudentLog latestLog;

        public string StudentID { get; set; }
        public Command PrintCommand { get; set; }

        string checkinResult = "Not checked in";

        public string CheckinResult {

            get { return checkinResult; }
            set { SetProperty(ref checkinResult, value); }
        }

        string checkinStatus = ""; //default

        public string CheckinStatus {

            get { return checkinStatus; }
            set {
                SetProperty(ref checkinStatus, value);
            }
        }


        bool isCheckedin = false;

        public bool IsCheckedIn {

            get { return isCheckedin; }
            set {
                SetProperty(ref isCheckedin, value);
            }
        }

        public StudentLog LatestLog {

            get {
                return latestLog;
            }
            set {
                SetProperty(ref latestLog, value);
            }
        }

        string timein;

        public string TimeIn {
            get { return timein;  }
            set
            {
                SetProperty(ref timein, value);
            }
        }

        public StudentDetails Details {

            get { return details; }
            set {
                SetProperty(ref details, value);

            }
        }

        bool isExternalSuspension = false;
        bool isInternalSuspension = false;

        public bool IsExternalSuspension { get { return isExternalSuspension; } set { SetProperty(ref isExternalSuspension, value); } }
        public bool IsInternalSuspension { get { return isInternalSuspension; } set { SetProperty(ref isInternalSuspension, value); } }

        bool isSuspended = false;
        string suspendedText = "";
        public string SuspendedText { get { return suspendedText; } set { SetProperty(ref suspendedText, value); } }

        public bool IsSuspended { get { return isSuspended; } set { SetProperty(ref isSuspended, value); } }

        string statusHexColor = "#0000FF"; //blue

        public string StatusHexColor { get { return statusHexColor; } set { SetProperty(ref statusHexColor, value); } }
        Color statusTextColor = Color.Blue;
        public Color StatusTextColor { get { return statusTextColor; } set { SetProperty(ref statusTextColor, value); } }


        void ConvertAndSetSuspendedVal()
        {
            this.IsSuspended = this.Details?.Status.ToLower() == "suspended" ? true : false;

            if (this.IsSuspended)
            {
                var reason = this.Details?.Reason ?? "";

                switch (this.Details.Suspended)
                {

                    case "I":
                        IsInternalSuspension = true;
                        IsExternalSuspension = false;
                        StatusHexColor = "#FF8800";

                        break;
                    case "O":
                        IsExternalSuspension = true;
                        IsInternalSuspension = false;
                        StatusHexColor = "#FF0000";

                        break;
                    default:
                        break;
                }

                this.SuspendedText = $" {reason}";

                StatusTextColor = Color.FromHex(StatusHexColor);

            }
            else //not suspended
            {
                this.SuspendedText = "";
                IsInternalSuspension = false;
                IsExternalSuspension = false;
                this.StatusHexColor = "#0000FF";
                StatusTextColor = Color.FromHex(StatusHexColor);
            }
        }

        CheckInRec checkinRecord;
        public CheckInRec CheckinRecord {
            get { return checkinRecord; }
            set {
                SetProperty(ref checkinRecord, value);
            }
        }




        public async void Reload(string scancode)
        {
            var api = GetApiInstance();
            var details = await api.GetStudentDetails(5, scancode);
            Init(details.FirstOrDefault());
        }

        K12RestApi GetApiInstance()
        {
            return this.api ?? new K12RestApi();
        }



        public StudentCheckinViewModel(StudentDetails details)
        {

            Init(details);
            TestEvents(details);
        }
        async void TestEvents(StudentDetails details)
        {

            var events = api.GetEvents(0, DateTime.Now);

            string s = "waht we gots";
        }


        async void Init(StudentDetails details)
        {
            try
            {

                api = this.GetApiInstance();
                this.Details = details;
                IsBusy = true;


                this.LatestLog = await GetLatestStudentLog(5, DateTime.Now);
             

                try
                {
                    var res = await CheckStudentInOrOut();

                    this.IsCheckedIn = res.Item1;
                    this.CheckinStatus = res.Item2;

                    if (this.LatestLog != null)
                    {
                            this.TimeIn = this.LatestLog.TimeIn.ToString("g");
                  
                    }
                }
                catch (Exception ex)
                {
                    Nav.ShowAlert("Error", "Could not check out student");
                }

                ConvertAndSetSuspendedVal();
                IsBusy = false;

            }
            catch (Exception ex)
            {
                var p = ex.Message;
                this.CheckinResult = ex.Message;
                this.IsBusy = false;
                Nav.ShowAlert("Error", ex.Message);
            }
            finally
            {
                this.IsBusy = false;
            }
        }



        async Task<StudentLog> GetLatestStudentLog(int compID, DateTime dt)
        {
            var log = await api.GetLatestStudentLog(compID, this.Details.CCode, DateTime.Now);
            return log;
        }



        public string StudentImagePath { get; set; }


        bool ShouldSignIn()
        {

            var log = this.LatestLog;

            if (log == null) //no sign in log
            {
                return true;
            }

            if (log.TimeOut != null)
            {
                if (log.TimeOut.Year == 1)
                    return false;

                return true;
            }

            return false; //should sign out.


        }

        async Task<Tuple<bool, string>> CheckStudentInOrOut()
        {

            bool isCheckedIn = false;
            string checkinStatus = String.Empty;
            var cr = new CheckInRec();

            var rec = new Rec();
            rec.CCode = Details.CCode.ToString();
            rec.IDNum = Details.IDNumber;

            rec.Excused = "True";
            rec.Remarks = "";
            rec.PKID = "";
            rec.Stamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            rec.CompID = Details.CompID.ToString();
            cr.rec = rec;

            this.CheckinRecord = cr;

            if (ShouldSignIn())
            {
                this.CheckinResult = await api.CheckInStudent(cr);
                if (!this.CheckinResult.Contains("success"))
                {
                    throw new Exception("Could not Check in student");
                }

                isCheckedIn = true;
                checkinStatus = "Checked In";
            }
            else
            {
                rec.PKID = this.LatestLog?.PKID.ToString();
                this.CheckinResult = await api.CheckOutStudent(cr);

                if (!this.CheckinResult.Contains("success"))
                {
                    throw new Exception("Could not Check in student");
                }

                isCheckedIn = false;
                checkinStatus = "Checked Out";

            }


            return Tuple.Create<bool, string>(isCheckedIn, checkinStatus);


        }


        async void GetStudentDetails(int compID, string studentID)
        {
            var detailsList = await api.GetStudentDetails(compID, studentID);
            this.Details = detailsList.FirstOrDefault();
        }

    }
}
