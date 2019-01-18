using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIResponses.List
{
    public class ListCheckoutResponseItem
    {
     
        [JsonProperty(PropertyName = "checkoutId")]
        public string checkoutId { get; set; }

       

        [JsonProperty(PropertyName = "checkoutDate")]
        public long CheckoutDate { get; set; }

        [JsonProperty(PropertyName = "totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "gatewayId")]
        public string GatewayId { get; set; }

         [JsonProperty(PropertyName = "gatewayTransactionId")]
        public string GatewayTransactionId { get; set; }


    }
}
