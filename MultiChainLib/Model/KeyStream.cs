using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Model
{
    public class KeyStream
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("items")]
        public string Items { get; set; }

        [JsonProperty("confirmed")]
        public string Confirmed { get; set; }
    }
}
