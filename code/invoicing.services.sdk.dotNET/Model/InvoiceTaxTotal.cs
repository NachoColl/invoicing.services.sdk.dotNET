using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model
{
    public class InvoiceTaxTotal {

        [JsonProperty(PropertyName = "taxName", Required = Required.Always)]
        public string TaxName { get; set; } = "";

        [JsonProperty(PropertyName = "taxRate")]
        public decimal TaxRate { get; set; } = 0m;

        [JsonProperty(PropertyName = "taxTotal", Required = Required.Always)]
        public decimal TaxTotal { get; set; } = 0m;
    }
}
