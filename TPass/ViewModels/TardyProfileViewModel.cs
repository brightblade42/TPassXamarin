using System;
using System.Linq;
using System.Threading.Tasks;
using TPass.Api;
using TPass.Models;
using TPass.XPInterfaces;
using Xamarin.Forms;

namespace TPass.ViewModels
{

    public class TardyProfileViewModel : BaseViewModel
    {

        K12RestApi api = null;
        public string StudentID { get; set; }
        StudentDetails details;
        StudentLog latestLog;

        public Command PrintCommand { get; set; }

        string checkinResult = "Not checked in...";

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
        string tardyReason = "";
        public string TardyReason {
            get { return tardyReason; }
            set {
                SetProperty(ref tardyReason, value);

            }
        }


        public StudentDetails Details {
            get { return details; }
            set {
                SetProperty(ref details, value);

            }
        }

        string timein = "";
        public string TimeIn {
            get { return timein; }
            set
            {
                SetProperty(ref timein, value);
            }
        }
        string printButtonText = "PRINT TARDY PASS";

        public string PrintButtonText {
            get { return printButtonText; }
            set
            {
                SetProperty(ref printButtonText, value);
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


        public TardyProfileViewModel(string studentId, string tardyReason)
        {

            PrintCommand = new Command(async (x) => await PrintTardySlip());

      
            TardyReason = tardyReason;

            PrintButtonText = GetPrintButtonText(tardyReason);


            api = new K12RestApi();
            Init(studentId);
        }

        string GetPrintButtonText(string tardyReason)
        {
            var btnText = "PRINT TARDY PASS";
            if (tardyReason.ToLower().Contains("class"))
            {
                btnText = "PRINT TARDY TO CLASS";
            }

            return btnText;
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
                GetStudentInfo2(int.Parse(studentId));
                ConvertAndSetSuspendedVal();
            }
            catch (Exception ex)
            {
                Nav.ShowAlert("Error", "Could not retrieve student data");
            }
            IsBusy = false;
        }

        public void Reload(string scancode)
        {
            Init(scancode);
        }


        public TardyProfileViewModel(StudentDetails details, string tardyReason)
        {

            PrintCommand = new Command(async (x) => await PrintTardySlip());

            TardyReason = tardyReason;
            PrintButtonText = GetPrintButtonText(tardyReason);
            api = new K12RestApi();
            this.Details = details;
            IsBusy = true;

            GetStudentInfo2(int.Parse(details.IDNumber));
            ConvertAndSetSuspendedVal();

            IsBusy = false;
        }

        void GetStudentInfo2(int id)
        {
            try
            {

                this.IsBusy = true;

                GetLatestStudentLog(5, DateTime.Now);
                CheckinStudent();
                this.IsBusy = false;

            }

            catch (Exception ex)
            {
                this.CheckinResult = ex.Message;
                this.IsBusy = false;
                this.IsCheckedIn = true;
            }
        }

        IPrinter printService = null;

        public string StudentImagePath { get; set; }

        async Task<bool> PrintTardySlip()
        {
            IsBusy = true;
            var done = false;

            try
            {
                if (printService == null)
                    printService = DependencyService.Get<IPrinter>();

                var zpl = CreateZPL();
                done = await printService.PrintZPL(zpl);
            }
            catch (Exception ex)
            {
                ;
            }

            IsBusy = false;

            if (!done)
            {
                this.Nav.ShowAlert("Print failure.", "Check that printer is paired and try again.");
            }

            return true;
        }



        string CreateZPL()
        {
            var tardyTitle = "Tardy Pass";
          /*  if (tardyReason.ToLower().Contains("class"))
            {
                tardyTitle = "Tardy To Class";
            }*/

            var name = $"{details.FName} {details.LName}";
            var school = $"{details.School.ToLower().ToUpperInvariant()}";

            if (lastTimeLog == null)
            {
                lastTimeLog = DateTime.Now;
            }

            var timein = lastTimeLog.ToString("MM-dd-yyyy hh:mm tt");
            var timein_short = lastTimeLog.ToString("hh:mm tt");
            var m0 = timein.Substring(0, 1);
            var m1 = timein.Substring(1, 1);
            var d0 = timein.Substring(3, 1);
            var d1 = timein.Substring(4, 1);

            // var img = ImageToZPL(this.StudentImagePath);
            var zzz1 = "^XA" + "\r\n" +
//"^MMT" + "\r\n" +
"^PW384" + "\r\n" +
"^LL0609" + "\r\n" +
"^LS0" + "\r\n" +
$@"^FO55,40^GB238,56,56^FS" + "\r\n" +
$@"^FT55,84^A0N,45,43^FR^FH\^FD{tardyTitle}^FS" + "\r\n" +
$@"^BY3,3,74^FT73,521^BCN,,Y,N" + "\r\n" +
$@"^FD>;{this.details.IDNumber}>60^FS" + "\r\n" +
$@"^FT14,214^A0N,34,33^FH\^FD{school}^FS" + "\r\n" +
$@"^FT116,399^A0N,56,43^FH\^FD{timein_short}^FS" + "\r\n" +
$@"^FT121,305^A0N,56,43^FH\^FD{m0}{m1} - {d0}{d1}^FS" + "\r\n" +
$@"^FT25,164^A0N,39,38^FH\^FD{name}^FS" + "\r\n" +
$@"^PQ1,0,1,Y^XZ";

            var zz1 = "^XA" + "\r\n" +
//^MMT
"^PW384" + "\r\n" +
"^LL0609" + "\r\n" +
"^LS0" + "\r\n" +
$@"^FO73,55^GB238,56,56^FS" + "\r\n" +
$@"^FT73,99^A0N,45,43^FR^FH\^FDTARDY PASS^FS" + "\r\n" +
//$@"^BY3,3,74^FT73,536^BCN,,Y,N" + "\r\n" +
//$@"^FD>;{this.details.IDNumber}>60^FS" + "\r\n" +
$@"^FT27,211^A0N,31,31^FH\^FD{school}^FS" + "\r\n" +
$@"^FT116,414^A0N,56,43^FH\^FD{timein_short}^FS" + "\r\n" +
$@"^FT121,320^A0N,56,43^FH\^FD{m0}{m1} - {d0}{d1}^FS" + "\r\n" +
$@"^FT53,156^A0N,31,31^FH\^FD{name}^FS" + "\r\n" +
$@"^FT99,512^A0N,31,31^FH\^FD{tardyReason}^FS" + "\r\n" +
$@"^PQ1,0,1,Y^XZ";

            //old stuff
            var zz2 = "^XA" + "\r\n" +
                "^PW384" + "\r\n" +
                "^LL0609" + "\r\n" +
                "^LS0" + "\r\n" +
                "^FO64,11^GB247,56,56^FS" + "\r\n" +
                @"^FT64,55^A0N,45,45^FR^FH\^FDTARDY PASS^FS" + "\r\n" +
                @"^FO54,377^GB278,0,5^FS" + "\r\n" + 
                @"^FO56,267^GB279,0,4^FS" + "\r\n" +

                $@"^FT107,138^A0N,38,35^FH\^FD{name}^FS" + "\r\n" +
                $@"^FT107,200^A0N,30,28^FH\^FD{tardyReason}^FS" + "\r\n" +

                $@"^FT80,321^A0N,25,24^FH\^FD{school}^FS" + "\r\n" +
                $@"^FT89,352^ACN,18,10^FH\^FD{timein}^FS" + "\r\n" +
                @"^BY3,3,74^FT73,481^BCN,,Y,N" + "\r\n" +
                $@"^FD>;{this.details.IDNumber}>60^FS" + "\r\n" +
                @"^PQ1,0,1,Y^XZ";


            return zz1;
        }


       

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


        async void GetCheckinRecord()
        {
            var crec = await api.GetStudentCheckinRecord(Details.CCode, DateTime.Now);
            
        }

        public async void CheckinStudent()
        {
            try
            {
                var cr = new CheckInRec();

                var rec = new Rec();
                rec.CCode = Details.CCode.ToString();
                rec.IDNum = Details.IDNumber;

                rec.CompID = this.Details.CompID.ToString();
                rec.PKID = LatestLog?.PKID.ToString() ?? "";

              

                if (String.IsNullOrEmpty(this.TardyReason))
                {
                    rec.Excused = "False";
                    rec.Remarks = "";
                }

                else if(this.TardyReason.ToLower().Contains("class")) //Tardy to class
                {
                    rec.Excused = "True";
                    rec.Remarks = this.TardyReason;
                }
                else
                {
                    rec.Excused = "True";
                    rec.Remarks = this.TardyReason;
                }


                lastTimeLog = DateTime.Now;
                rec.Stamp = lastTimeLog.ToString("yyyy-MM-dd HH:mm:ss");
               
                cr.rec = rec;

                this.CheckinRecord = cr;

                this.CheckinResult = await api.CheckInStudent(cr);

                if (this.CheckinResult.Contains("success"))
                {
                    IsCheckedIn = true;
                }

                GetCheckinRecord();

                TimeIn = lastTimeLog.ToLocalTime().ToString("g");

            }

            catch (Exception ex)
            {
                Nav.ShowAlert("Error", "Could not check in student!");
                Nav.ExecuteNavigation("back");

            }

        }

    }
}
