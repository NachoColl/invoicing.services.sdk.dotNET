using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model
{
    public class Tax {

        [JsonProperty(PropertyName = "taxName")]
        public string TaxName { get; set; } = "";

        [JsonProperty(PropertyName = "taxRate")]
        public decimal TaxRate { get; set; } = 0m;
    }
}
