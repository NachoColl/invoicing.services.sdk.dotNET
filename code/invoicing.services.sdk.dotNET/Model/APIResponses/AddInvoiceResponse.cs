using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIResponses
{
    public class AddInvoiceResponse
    {
        [JsonProperty(PropertyName = "invoiceId")]
        public string InvoiceId { get; set; }

        [JsonProperty(PropertyName = "invoiceDate")]
        public string InvoiceDate { get; set; }

        [JsonProperty(PropertyName = "invoiceFileURL")]
        public string InvoiceFileURL { get; set; }
    }
}
