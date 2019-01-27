using Newtonsoft.Json;

namespace invoicing.services.sdk.dotNET.Model
{
    public class Price
    {
        [JsonProperty(PropertyName = "productId")]
        public string productId { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; } = "US";

        [JsonProperty(PropertyName = "currencyCode")]
        public string CurrencyCode {get;set;} = "USD";

        [JsonProperty(PropertyName = "amount")]
         public decimal Amount {get; set;}  = 0;
    }
}