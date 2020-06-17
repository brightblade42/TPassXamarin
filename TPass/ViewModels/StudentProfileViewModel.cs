using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TPass.Api;
using TPass.Models;
using TPass.Views;
using Xamarin.Forms;


namespace TPass.ViewModels
{
    class StudentProfileViewModel : BaseViewModel
    {

       
        K12RestApi api = null;
        public string StudentID { get; set; }
        int ccode;

        public int CCode {
            get { return ccode; }
            protected set {
                SetProperty(ref ccode, value);
            }
        }

        StudentDetails details;


        public StudentDetails Details {
            get { return details; }
            set {
                SetProperty(ref details, value);
            }
        }

        string statusHexColor = "#0000FF"; //blue

        public string StatusHexColor { get { return statusHexColor; } set { SetProperty(ref statusHexColor, value); } }
        Color statusTextColor = Color.Blue;
        public Color StatusTextColor { get { return statusTextColor; } set { SetProperty(ref statusTextColor, value); } }


        bool isSuspended = false;
        string suspendedText = "";
        public string SuspendedText { get { return suspendedText; } set { SetProperty(ref suspendedText, value); } }

        public bool IsSuspended { get { return isSuspended; } set { SetProperty(ref isSuspended, value); } }


        bool hasAddress = false;

        public bool HasAddress {
            get { return hasAddress; }
            set {

                SetProperty(ref hasAddress, value);

            }
        }

		bool hasSchedule = false;
		public bool HasSchedule {
			get { return hasSchedule; }
			set
			{
				SetProperty(ref hasSchedule, value);
			}
		}


        void ValidateAddress()
        {
            if (String.IsNullOrEmpty(Details.Address))
                HasAddress = false;        
            else
                HasAddress = true;

        }

        bool hasHomeroomListing = false;

        public bool HasHomeroomListing { get { return hasHomeroomListing; } set { SetProperty(ref hasHomeroomListing, value); } }

        string homeroomText = "";
        public string HomeroomText { get { return homeroomText; } set { SetProperty(ref homeroomText, value); } }

        bool hasMedicalRecord = false;

        public bool HasMedicalRecord { get { return hasMedicalRecord; } set { SetProperty(ref hasMedicalRecord, value); } }

        void ConvertAndSetMedicalVal()
        {
            if (this.MedicalRecord == null)
            {
                HasMedicalRecord = false;
                return;
            }

            if (String.IsNullOrEmpty(this.MedicalRecord.Description))
            {
                HasMedicalRecord = false;
                return;
            }

            this.HasMedicalRecord = this.MedicalRecord.Description.ToLower() != "none";

        }

        void ConvertAndSetHomeroomVal()
        {

            this.HasHomeroomListing = !String.IsNullOrEmpty(this.Details.Homeroom);
            if (HasHomeroomListing)
            {
                var teach = String.IsNullOrEmpty(this.Details.HMTeacher) ? "" : this.Details.HMTeacher;
                HomeroomText = $"HR {this.Details.Homeroom} {teach}";
            }
            else
            {
                HomeroomText = "";
            }

        }
        bool isExternalSuspension = false;
        bool isInternalSuspension = false;

        public bool IsExternalSuspension { get { return isExternalSuspension; } set { SetProperty(ref isExternalSuspension, value); } }
        public bool IsInternalSuspension { get { return isInternalSuspension; } set { SetProperty(ref isInternalSuspension, value); } }


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
            else
            {
                this.SuspendedText = "";
                IsInternalSuspension = false;
                IsExternalSuspension = false;
                this.StatusHexColor = "#0000FF";
                StatusTextColor = Color.FromHex(StatusHexColor);
            }
        }

        ObservableCollection<StudentContact> contacts;


        public ObservableCollection<StudentContact> Contacts {
            get { return contacts; }
            set {

                SetProperty(ref contacts, value);
            }

        }

        ObservableCollection<AssociatedContact> assocContacts;
        public ObservableCollection<AssociatedContact> AssocContacts {
            get { return assocContacts; }
            set {

                SetProperty(ref assocContacts, value);
            }

        }


        ObservableCollection<StudentSchedule> schedule;


        public ObservableCollection<StudentSchedule> Schedule {
            get { return schedule; }
            set {

                SetProperty(ref schedule, value);
            }

        }

        ObservableCollection<Grouping<string, StudentSchedule>> groupedSchedule;
        public ObservableCollection<Grouping<string, StudentSchedule>> GroupedSchedule {
            get { return groupedSchedule; }
            set
            {
                SetProperty(ref groupedSchedule, value);
            }
        }


        StudentMedicalRecord medicalRecord;

        public StudentMedicalRecord MedicalRecord {
            get { return medicalRecord; }
            set {
                SetProperty(ref medicalRecord, value);
            }
        }

        public StudentProfileViewModel(string studentId)
        {
            int id;
            if (!int.TryParse(studentId, out id))
            {
                throw new ArgumentException("Couldn't parse student id");
            }

            InitFromScanCode(studentId);
        }


        public StudentProfileViewModel(StudentDetails details)
        {

            api = new K12RestApi();   // what's this, precious?
            this.Details = details;

            Init();

        }

        public void Reload(string data)
        {
            InitFromScanCode(data);
        }

        async void InitFromScanCode(string scancode)
        {
            try
            {
                api = new K12RestApi();
                var res = await this.GetStudent(scancode);

                if (res)
                    Init();
            }
            catch (Exception ex)
            {
                Nav.ShowAlert("Error", "Could not retrieve Student info from server");
            }
        }

        void Init()
        {

            ValidateAddress();
            ConvertAndSetSuspendedVal();
            ConvertAndSetHomeroomVal();

            GetContacts(this.Details.CCode);
            GetSchedule(this.Details.CCode);
            GetMedicalRecord(this.Details.CCode);
        }

        async Task<bool> GetStudent(string id)
        {

            var detailsList = await api.GetStudentDetails(5, id);

            if (detailsList.Count() < 1)
            {
                this.Nav.ShowAlert("No results", $"Student id: {id} not found");

                return false;
            }

            this.Details = detailsList.FirstOrDefault();

            return true;


        }



        async void GetContacts(int ccode)
        {

            var cts = await api.GetStudentContacts(this.Details.CCode);
            this.Contacts = new ObservableCollection<StudentContact>(cts);

        }

        async void GetSchedule(int ccode)
        {
            var sch = await api.GetStudentSchedule(this.Details.CCode);

            this.Schedule = new ObservableCollection<StudentSchedule>(transformSchedule(sch));

            
			if (this.Schedule != null)
			{
				
				if (this.Schedule.Count > 0)
				{
					this.HasSchedule = true;

					foreach (var sched in this.Schedule)
					{
                        
						var daysTxt = "";
						var mon = "";
						var tues = "";
						var wed = "";
						var thurs = "";
						var fri = "";
						if (sched.Days == null)
						{
							sched.Days = "untracked";
                            sched.DaySortCode = 5;
						}

						if ((bool)sched.Mon)
						{
							mon = "A/M/MTWR";
                            sched.Days = mon;
                            sched.DaySortCode = 0;
						}
						if ((bool)sched.Tue)
						{
							tues = "AER/T";
                            sched.Days = tues;
                            sched.DaySortCode = 1;
						}
						if ((bool)sched.Wed)
						{
							wed = "B/W";
                            sched.Days = wed;
                            sched.DaySortCode = 2;
						}
						if ((bool)sched.Thu)
						{
							thurs = "BER/R";
                            sched.Days = thurs;
                            sched.DaySortCode = 3;
						}
						if ((bool)sched.Fri)
						{
							fri = "FRI";
                            sched.Days = fri;
                            sched.DaySortCode = 4;
						}

						//sched.Days = $"{mon} {tues} {wed} {thurs} {fri}".Trim();
					}


                    var sortedSched = from sched in this.Schedule
                                      orderby sched.DaySortCode, sched.Period
                                      group sched by sched.Days into schedGroup
                                      select new Grouping<string, StudentSchedule>(schedGroup.Key, schedGroup);

                   this.GroupedSchedule = new ObservableCollection<Grouping<string, StudentSchedule>>(sortedSched);
                    
                    //bad
                    var view1 = this.Nav as StudentProfileView;
                    view1.UpdateScheduleSize();


				}
			}
        }

        //The Dev schedule and the Desmoines schedule differ slightly. 
        IEnumerable<StudentSchedule> transformSchedule(IEnumerable<StudentSchedule> schedule)
        {
            if (!K12RestApi.IsDev)
            {
                return schedule;
            }
            var ns = new List<StudentSchedule>();
            foreach (var item in schedule)
            {
                if (item.Mon.Value)
                {
                    var sc = item.ShallowCopy();
                    sc.Tue = sc.Wed = sc.Thu = sc.Fri = false;
                    ns.Add(sc);
                }
                if (item.Tue.Value)
                {
                    var sc = item.ShallowCopy();
                    sc.Mon = sc.Wed = sc.Thu = sc.Fri = false;
                    ns.Add(sc);
                }
                if (item.Wed.Value)
                {
                    var sc = item.ShallowCopy();
                    sc.Mon = sc.Tue = sc.Thu = sc.Fri = false;
                    ns.Add(sc);
                }
                if (item.Thu.Value)
                {
                    var sc = item.ShallowCopy();
                    sc.Mon = sc.Tue = sc.Wed = sc.Fri = false;
                    ns.Add(sc);
                }
                if (item.Fri.Value)
                {
                    var sc = item.ShallowCopy();
                    sc.Mon = sc.Wed = sc.Thu = sc.Tue = false;
                    ns.Add(sc);
                }
            }

            return ns;
        }

            async void GetAssociatedContacts(int ccode)
        {

            var assoc = await api.GetAssociatedContacts(ccode);
            this.AssocContacts = new ObservableCollection<AssociatedContact>(assoc);
        }

        async void GetMedicalRecord(int ccode)
        {
            this.MedicalRecord = await api.GetStudentMedicalRecord(ccode);
            ConvertAndSetMedicalVal();

        }

    }
}
