using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIResponses.Init {
    public class InitCheckoutResponse {

        [JsonProperty(PropertyName = "productId")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }


        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

    }
}
