using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIResponses.List
{
    public class ListCheckoutResponse {

        [JsonProperty(PropertyName = "query")]
        public APIQueries.List.ListCheckoutQuery Query { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "list")]
        public List<ListCheckoutResponseItem> List { get; set; }
    }
}
