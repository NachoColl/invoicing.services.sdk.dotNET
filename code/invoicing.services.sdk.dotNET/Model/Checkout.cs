using System.Collections.Generic;
using Newtonsoft.Json;

namespace invoicing.services.sdk.dotNET.Model {

    public class Checkout {

        [JsonProperty(PropertyName = "checkoutId")]
        public string CheckoutId { get; set; }

        [JsonProperty(PropertyName = "gateway")]
        public string Gateway { get; set; }

        [JsonProperty(PropertyName = "gatewayTransactionId")]
        public string GatewayTransactionId { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty(PropertyName = "checkoutItems")]
        public string CheckoutItems { get; set; }

        [JsonProperty(PropertyName = "statusHistory")]
        public List<CheckoutStatus> StatusHistory { get; set; }

    }
}