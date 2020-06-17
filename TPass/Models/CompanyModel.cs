using Newtonsoft.Json;
using System;

namespace TPass.Models
{

    public class Company
    {

        [JsonProperty]
        public int CompID { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public DateTime? SchlTimeIn { get; set; }

    }
}