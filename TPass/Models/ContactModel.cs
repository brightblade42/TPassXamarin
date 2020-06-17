using Newtonsoft.Json;
using System;

namespace TPass.Models
{

    public class AssociatedContact
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
        public bool CustodyIssue { get; set; }
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


    [JsonObject]
    public class Visitor : IModel
    {

        [JsonProperty]
        public long? Agency { get; set; }
        [JsonProperty]
        public string BdgNo { get; set; } = "";
        [JsonProperty]
        public long? CCode { get; set; }
        [JsonProperty]
        public DateTime? Date { get; set; }
        [JsonProperty]
        public string Details { get; set; } = "";
        [JsonProperty]
        public string FName { get; set; } = "";
        [JsonProperty]
        public string Host { get; set; } = "";
        [JsonProperty]
        public string HostImageUrl { get; set; } = "";
        [JsonProperty]
        public string HostName { get; set; } = "";
        [JsonProperty]
        public int? HostType { get; set; }
        [JsonProperty]
        public string LName { get; set; } = "";
        [JsonProperty]
        public string Location { get; set; } = "";
        [JsonProperty]
        public long? PKID { get; set; }
        [JsonProperty]
        public string Purpose { get; set; } = "";
        [JsonProperty]
        public string Reason { get; set; } = "";
        [JsonProperty]
        public bool? Selected { get; set; }
        [JsonProperty]
        public DateTime? TimeIn { get; set; }
        [JsonProperty]
        public DateTime? TimeOut { get; set; }
        [JsonProperty]
        public string VisitorImageUrl { get; set; } = "";
        [JsonProperty]
        public int? VisitorType { get; set; }
        [JsonProperty]
        public long? VstPKID { get; set; }
        [JsonProperty]
        public string VstType { get; set; } = "";
        [JsonProperty]
        public string VstrCompany { get; set; } = "";

        public string TimeInFormat {
            get {
                string t = "";
                if (TimeIn.HasValue)
                {
                    t = TimeIn.Value.ToString("hh:mm");
                }

                return t;
            }
        }

    }
}
