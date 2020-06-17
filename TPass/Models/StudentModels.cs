//using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TPass.Models
{

    [JsonObject]
    public class StudentDetails : INotifyPropertyChanged, IModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        string address;
        public string Address { get { return this.address; } set { this.address = value; OnPropertyChanged(); } }
        [JsonProperty]
        object alias;
        public object Alias { get { return this.alias; } set { this.alias = value; OnPropertyChanged(); } }
        [JsonProperty]
        string bdate;
        public string BDate { get { return this.bdate; } set { this.bdate = value; OnPropertyChanged(); } }
        [JsonProperty]
        int ccode;
        public int CCode { get { return this.ccode; } set { this.ccode = value; OnPropertyChanged(); } }
        [JsonProperty]
        string canSignOut;
        public string CanSignOut { get { return this.canSignOut; } set { this.canSignOut = value; OnPropertyChanged(); } }
        [JsonProperty]
        string city;
        public string City { get { return this.city; } set { this.city = value; OnPropertyChanged(); } }
        [JsonProperty]
        int clntTID;
        public int ClntTID { get { return this.clntTID; } set { this.clntTID = value; OnPropertyChanged(); } }
        [JsonProperty]
        int compID;
        public int CompID { get { return this.compID; } set { this.compID = value; OnPropertyChanged(); } }
        [JsonProperty]
        object email;
        public object EMail { get { return this.email; } set { this.email = value; OnPropertyChanged(); } }
        [JsonProperty]
        string fname;
        public string FName { get { return this.fname; } set { this.fname = value; OnPropertyChanged(); } }
        [JsonProperty]
        string gender;
        public string Gender { get { return this.gender; } set { this.gender = value; OnPropertyChanged(); } }
        [JsonProperty]
        string grade;
        public string Grade { get { return this.grade; } set { this.grade = value; OnPropertyChanged(); } }
        [JsonProperty]
        object gradtnYear;
        public object GradtnYear { get { return this.gradtnYear; } set { this.gradtnYear = value; OnPropertyChanged(); } }
        [JsonProperty]
        string hmTeacher;
        public string HMTeacher { get { return this.hmTeacher; } set { this.hmTeacher = value; OnPropertyChanged(); } }
        [JsonProperty]
        string homeroom;
        public string Homeroom { get { return this.homeroom; } set { this.homeroom = value; OnPropertyChanged(); } }
        [JsonProperty]
        string homeroomID;
        public string HomeroomID { get { return this.homeroomID; } set { this.homeroomID = value; OnPropertyChanged(); } }
        [JsonProperty]
        string idNumber;
        public string IDNumber { get { return this.idNumber; } set { this.idNumber = value; OnPropertyChanged(); } }
        [JsonProperty]
        string imageUrl;
        public string ImageUrl { get { return this.imageUrl; } set { this.imageUrl = value; OnPropertyChanged(); } }
        [JsonProperty]
        string lname;
        public string LName { get { return this.lname; } set { this.lname = value; OnPropertyChanged(); } }
        [JsonProperty]
        object lcnsNum;
        public object LcnsNum { get { return this.lcnsNum; } set { this.lcnsNum = value; OnPropertyChanged(); } }
        [JsonProperty]
        string mname;
        public string MName { get { return this.mname; } set { this.mname = value; OnPropertyChanged(); } }
        [JsonProperty]
        object mobile;
        public object Mobile { get { return this.mobile; } set { this.mobile = value; OnPropertyChanged(); } }
        [JsonProperty]
        object phone;
        public object Phone { get { return this.phone; } set { this.phone = value; OnPropertyChanged(); } }
        [JsonProperty]
        object proxID;
        public object ProxID { get { return this.proxID; } set { this.proxID = value; OnPropertyChanged(); } }
        [JsonProperty]
        string reason;
        public string Reason { get { return this.reason; } set { this.reason = value; OnPropertyChanged(); } }
        [JsonProperty]
        string school;
        public string School { get { return this.school; } set { this.school = value; OnPropertyChanged(); } }
        [JsonProperty]
        string state;
        public string State { get { return this.state; } set { this.state = value; OnPropertyChanged(); } }
        [JsonProperty]
        string status;
        public string Status { get { return this.status; } set { this.status = value; OnPropertyChanged(); } }
        [JsonProperty]
        string suspended;
        public string Suspended { get { return this.suspended; } set { this.suspended = value; OnPropertyChanged(); } }
        [JsonProperty]
        string type;
        public string Type { get { return this.type; } set { this.type = value; OnPropertyChanged(); } }
        [JsonProperty]
        string zipCode;
        public string ZIPCode { get { return this.zipCode; } set { this.zipCode = value; OnPropertyChanged(); } }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    [JsonObject]
    public class StudentBehavior
    {
        [JsonProperty]
        public int CCode { get; set; }
        [JsonProperty]
        public int CompID { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public string TimeStamp { get; set; }
        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public int VltnTypID { get; set; }
    }


    public class StudentContact
    {

        [JsonProperty]
        public string Address { get; set; }
        [JsonProperty]
        public long CCode { get; set; }
        [JsonProperty]
        public bool CanPickUp { get; set; }
        [JsonProperty]
        public string City { get; set; }
        [JsonProperty]
        public long CntCode { get; set; }
        [JsonProperty]
        public bool? CustodyIssue { get; set; }
        [JsonProperty]
        public string CustodyNote { get; set; }
        [JsonProperty]
        public string EMail { get; set; }
        [JsonProperty]
        public string FName { get; set; }
        [JsonProperty]
        public string ImageUrl { get; set; }
        [JsonProperty]
        public string LName { get; set; }
        [JsonProperty]
        public string MName { get; set; }
        [JsonProperty]
        public string Mobile { get; set; }
        [JsonProperty]
        public string Phone { get; set; }
        [JsonProperty]
        public string PickUpSched { get; set; }
        [JsonProperty]
        public string Relationship { get; set; }
        [JsonProperty]
        public string State { get; set; }
        [JsonProperty]
        public string ZIP { get; set; }
        public bool CanNotPickUp { get { return !CanPickUp; } }


    }

    public class StudentMedicalRecord
    {

        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public int ID { get; set; }
    }

    public class StudentSchedule
    {

        [JsonProperty]
        public string Class { get; set; }
        [JsonProperty]
        public string Days { get; set; } = string.Empty;
        [JsonProperty]
        public bool? Fri { get; set; }
        [JsonProperty]
        public string Location { get; set; }
        [JsonProperty]
        public bool? Mon { get; set; }
        [JsonProperty]
        public string Period { get; set; }
        [JsonProperty]
        public bool? Thu { get; set; }
        [JsonProperty]
        public bool? Tue { get; set; }
        [JsonProperty]
        public bool? Wed { get; set; }

        [JsonProperty]
        public int? DaySortCode { get; set; }

        public StudentSchedule ShallowCopy()
        {
            return this.MemberwiseClone() as StudentSchedule;
        }

    }
}
