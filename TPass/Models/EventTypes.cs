using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TPass.Models
{

    [JsonObject]
    public class EventRecord
    {
        [JsonProperty] public int EvntID { get; set; }    //>> You will be using this to load the event attendees, load the latest check-in log of the attendee, check in and out an event.
        [JsonProperty] public int EvntTypID { get; set; }
        [JsonProperty] public string Type { get; set; }
        [JsonProperty] public int CompID { get; set; }
        [JsonProperty] public DateTime Date { get; set; }
        [JsonProperty] public string Description { get; set; }
        [JsonProperty] public string Name { get; set; }
        [JsonProperty] public string Loctn { get; set; }
        [JsonProperty] public DateTime? FromTime { get; set; }
        [JsonProperty] public DateTime? ToTime { get; set; }
        [JsonProperty] public string StatusDescription { get; set; }
        [JsonProperty] public string RecurrenceInfo { get; set; }
        [JsonProperty] public bool WalkIn { get; set; }
    }

    [JsonObject]
    public class EventAttendeeRec
    {
        [JsonProperty] public int AttPkID { get; set; }
        [JsonProperty] public int EvntID { get; set; }
        [JsonProperty] public long? CCode { get; set; }
        [JsonProperty] public int ClntTID { get; set; }
        [JsonProperty] public string IDNumber { get; set; }
        [JsonProperty] public string FName { get; set; }
        [JsonProperty] public string LName { get; set; }
        [JsonProperty] public string ImageUrl { get; set; }
        [JsonProperty] public DateTime? SignIn { get; set; }
        [JsonProperty] public DateTime? SignOut { get; set; }
        [JsonProperty] public string Type { get; set; }
    }

    [JsonObject]
    public class SignedEventAttendeeRec
    {
        [JsonProperty] public int PkID { get; set; }  // >> You pass this value when you are going to check out the attendee
        [JsonProperty] public int? AttPkID { get; set; }
        [JsonProperty] public int EvntID { get; set; }
        [JsonProperty] public long? CCode { get; set; }
        [JsonProperty] public DateTime Date { get; set; }
        [JsonProperty] public DateTime? SignIn { get; set; }
        [JsonProperty] public DateTime? SignOut { get; set; }
    }

    [JsonObject]
    public class EventCheckInRec
    {
        [JsonProperty] public string CCode { get; set; }
        [JsonProperty] public string PkID { get; set; }    //>> This is empty for check in
        [JsonProperty] public string EvntID { get; set; }   //>> You need to set this to the value of the selected event for check in
        [JsonProperty] public string Time { get; set; }
    }


}
