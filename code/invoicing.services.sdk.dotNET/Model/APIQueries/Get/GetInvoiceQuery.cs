using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIQueries.Get
{
    public class GetInvoiceQuery
    {
        [JsonProperty(PropertyName = "invoiceGuid", Required = Required.Always)]
        public string InvoiceGuid { get; set; }
    }
}
