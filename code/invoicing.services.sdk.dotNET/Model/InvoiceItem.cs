using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model
{
    public class InvoiceItem {
        
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; } = "";

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; } = 1;

        [JsonProperty(PropertyName = "unitPrice")]
        public decimal UnitPrice { get; set; } = 0;

        [JsonProperty(PropertyName = "taxes")]
        public List<Tax> Taxes { get; set; }

        [JsonProperty(PropertyName = "itemSubTotalAmount")]
        public decimal ItemSubTotalAmount { get; set; } = 0m;

        [JsonProperty(PropertyName = "itemTotalAmount", Required = Required.Always)]
        public decimal ItemTotalAmount { get; set; } = 0m; 

    }
}
