using Newtonsoft.Json;

namespace TPass.Models
{

    public class BehaviorType
    {
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public int VltnTypID { get; set; }
    }


    public class DropOffType
    {
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public int DrpTID { get; set; }
    }


    public class TardyType
    {

        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public int PKID { get; set; }
    }

    //allow me to overload methods..
    public struct CCode
    {

        public CCode(int num)
        {
            val = num;
        }

        public int val { get; set; }
    }

    public struct CompID
    {
        public int val { get; set; }
    }

    public struct BadgeNo
    {
        public int val { get; set; }
    }
}
