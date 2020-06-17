using Newtonsoft.Json;
using System;

namespace TPass.Models
{

    [JsonObject]
    public class StudentLog
    {

        [JsonProperty]
        public int Agency { get; set; }
        [JsonProperty]
        public string BdgNo { get; set; }
        [JsonProperty]
        public long CCode { get; set; }
        [JsonProperty]
        public DateTime Date { get; set; }
        [JsonProperty]
        public string Details { get; set; }
        [JsonProperty]
        public string FName { get; set; }
        [JsonProperty]
        public long Host { get; set; }
        [JsonProperty]
        public string HostImageUrl { get; set; }
        [JsonProperty]
        public string HostName { get; set; }
        [JsonProperty]
        public int HostType { get; set; }
        [JsonProperty]
        public string LName { get; set; }
        [JsonProperty]
        public string Location { get; set; }
        [JsonProperty]
        public long PKID { get; set; }
        [JsonProperty]
        public string Purpose { get; set; }
        [JsonProperty]
        public string Reason { get; set; }
        [JsonProperty]
        public bool Selected { get; set; }
        [JsonProperty]
        public DateTime TimeIn { get; set; }
        [JsonProperty]
        public DateTime TimeOut { get; set; }
        [JsonProperty]
        public string VisitorImageUrl { get; set; }
        [JsonProperty]
        public int VisitorType { get; set; }
        [JsonProperty]
        public long VstPKID { get; set; }
        [JsonProperty]
        public string VstType { get; set; }
        [JsonProperty]
        public string VstrCompany { get; set; }

    }

}
