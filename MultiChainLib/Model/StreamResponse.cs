using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Model
{
    public class StreamResponse
    {
        [JsonProperty("publishers")]
        public List<string> Publishers { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("confirmations")]
        public string Confirmations { get; set; }

        [JsonProperty("blocktime")]
        public string BlockTime { get; set; }

        [JsonProperty("txid")]
        public string TxId { get; set; }
    }
}
