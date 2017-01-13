using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIResponses
{
    public class ListInvoiceResponse {
        [JsonProperty(PropertyName = "query")]
        public APIQueries.ListInvoiceQuery Query { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "list")]
        public List<ListInvoiceResponseItem> List { get; set; }
    }
}
