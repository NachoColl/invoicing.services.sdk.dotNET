using Newtonsoft.Json;

namespace invoicing.services.sdk.dotNET.Model {
    public class CheckoutStatus {

        [JsonProperty(PropertyName = "gatewayTransactionId")]
        public string GatewayTransactionId { get; set; }

        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }


    }
}