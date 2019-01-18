using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIResponses.List
{
    public class ListInvoiceResponseItem
    {
     
        [JsonProperty(PropertyName = "invoiceGuid")]
        public string InvoiceGuid { get; set; }
        [JsonProperty(PropertyName = "invoiceId")]
        public string InvoiceId { get; set; }
        [JsonProperty(PropertyName = "invoiceDate")]
        public long InvoiceDate { get; set; }
        [JsonProperty(PropertyName = "invoiceBuyerName")]
        public string InvoiceBuyerName { get; set; }
        [JsonProperty(PropertyName = "invoiceTotal")]
        public decimal InvoiceTotal { get; set; }
        [JsonProperty(PropertyName = "invoiceCurrency")]
        public string InvoiceCurrency { get; set; }
        [JsonProperty(PropertyName = "invoiceUrl")]
        public string InvoiceUrl { get; set; }
    }
}
