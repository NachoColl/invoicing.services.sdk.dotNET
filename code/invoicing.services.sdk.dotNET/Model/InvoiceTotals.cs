using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model
{
    public class InvoiceTotals {

        [JsonProperty(PropertyName = "subTotal")]
        public decimal SubTotal { get; set; } = decimal.Zero;

        [JsonProperty(PropertyName = "taxTotals")]
        public List<InvoiceTaxTotal> TaxTotals;

        [JsonProperty(PropertyName = "total", Required = Required.Always)]
        public decimal Total { get; set; } = decimal.Zero;
    }
}
