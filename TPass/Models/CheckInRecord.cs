using Newtonsoft.Json;
//using Xamarin.Forms.PlatformConfiguration;

namespace TPass.Models
{
    [JsonObject]
    public class CheckInRec
    {
        public Rec rec { get; set; }

        public CheckInRec()
        {
        }
    }

    [JsonObject]
    public class Rec
    {
        [JsonProperty]
        public string CCode { get; set; }
        [JsonProperty]
        public string CompID { get; set; }
        [JsonProperty]
        public string Excused { get; set; }
        [JsonProperty]
        public string IDNum { get; set; }
        [JsonProperty]
        public string PKID { get; set; }
        [JsonProperty]
        public string Remarks { get; set; }
        [JsonProperty]
        public string Stamp { get; set; }
    }

    [JsonObject]
    public class CheckInResult
    {
        [JsonProperty]
        public string CheckInStudentResult { get; set; }
    }
}
