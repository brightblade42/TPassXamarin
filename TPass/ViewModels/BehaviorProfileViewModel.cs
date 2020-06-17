using System;
using System.Linq;
//using System.Linq
using System.Threading.Tasks;
using TPass.Api;
using TPass.Models;
using TPass.XPInterfaces;
using Xamarin.Forms;

namespace TPass.ViewModels
{

    public class BehaviorProfileViewModel : BaseViewModel
    {

        K12RestApi api = null;
        public string StudentID { get; set; }
        StudentDetails details;
        StudentLog latestLog;

        public Command PrintCommand { get; set; }

        string checkinResult = "";

        public string CheckinResult {
            get { return checkinResult; }
            set { SetProperty(ref checkinResult, value); }
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
        string behaviorReason = "";

        public string BehaviorReason {

            get { return behaviorReason; }
            set {
                SetProperty(ref behaviorReason, value);

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


        public BehaviorProfileViewModel(string studentId, string behaviorReason)
        {

            BehaviorReason = behaviorReason;

            api = new K12RestApi();
            Init(studentId);
        }

        async void Init(string studentId)
        {
            IsBusy = true;
            try
            {
                var details = await api.GetStudentDetails(5, studentId);

                if (details.Count() < 1)
                {
                    this.Nav.ShowAlert("No results", $"Student id: {studentId} not found");
                    return;
                }

                this.Details = details.FirstOrDefault();
                GetStudentInfo(int.Parse(studentId));
                ConvertAndSetSuspendedVal();
                AddStudentBehavior();

            }
            catch (Exception ex)
            {
                Nav.ShowAlert("Error", "Could not retrieve student data");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void Reload(string scancode)
        {
            Init(scancode);
        }


        public BehaviorProfileViewModel(StudentDetails details, string reason)
        {

            try
            {

                this.BehaviorReason = reason;

                api = new K12RestApi();
                this.Details = details;
                IsBusy = true;

                GetStudentInfo(int.Parse(details.IDNumber));
                ConvertAndSetSuspendedVal();
                AddStudentBehavior();

            }
            catch (Exception ex)
            {
                var p = ex.Message;
                Nav.ShowAlert("Error", ex.Message);
            }
            finally
            {
                IsBusy = false;

            }
        }

        void GetStudentInfo(int id)
        {
            try
            {

                this.IsBusy = true;

                GetLatestStudentLog(5, DateTime.Now);

            }

            catch (Exception ex)
            {
                this.CheckinResult = ex.Message;
                this.IsCheckedIn = true;
            }
            finally
            {
                this.IsBusy = false;

            }
        }



        public string StudentImagePath { get; set; }



        async void GetLatestStudentLog(int compID, DateTime dt)
        {
            var log = await api.GetLatestStudentLog(compID, this.Details.CCode, DateTime.Now);
            this.LatestLog = log;
        }

        async void GetStudentDetails(int compID, string studentID)
        {
            var detailsList = await api.GetStudentDetails(compID, studentID);
            this.Details = detailsList.FirstOrDefault();
        }

        DateTime lastTimeLog;


        async void GetStudentBehaviors()
        {
            ; //Noop
            //var crec = await api.GetStudentBehavior(Details.CCode, DateTime.Now);
            //var hmm = "";
            ///GetStudentCheckinRecord
        }

        public async void AddStudentBehavior()
        {
            try
            {
                IsBusy = true;
                var sb = new StudentBehavior();

                sb.CCode = Details.CCode;
                sb.CompID = this.Details.CompID;
                sb.Type = "";
                sb.Description = "";
                sb.TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var btype = api.GetCurrentBehaviorType();
                sb.VltnTypID = btype.VltnTypID;
                var result = await api.AddStudentBehavior(sb);
                this.BehaviorReason = btype.Description;

                var um = "";
            }

            catch (Exception ex)
            {
                Nav.ShowAlert("Error", "Could not add behavior");
                Nav.ExecuteNavigation("back");

            }
            finally
            {
                IsBusy = false;
            }

        }







    }
}
