using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIQueries.Get {
    public class GetCheckoutQuery {
        [JsonProperty(PropertyName = "checkoutId", Required = Required.Always)]
        public string CheckoutId { get; set; }
    }
}
