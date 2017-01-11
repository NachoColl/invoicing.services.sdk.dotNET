using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace invoicing.services.sdk.dotNET.Model
{
    public class Invoice
    {
        [JsonProperty(PropertyName = "dummy")]
        public bool Dummy { get; set; } = false;

        [JsonProperty(PropertyName = "templateNumber")]
        public int TemplateNumber { get; set; } = 1;

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public long Date { get; set; } = Utils.Timestamp.CurrentTimeMillis();

        [JsonProperty(PropertyName = "currencyCode")]
        public string CurrencyCode { get; set; } = "USD";

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; } = "US";

        [JsonProperty(PropertyName = "seller")]
        public Actor Seller { get; set; }
      
        [JsonProperty(PropertyName = "buyer", Required = Required.Always)]
        public Actor Buyer { get; set; }

        [JsonProperty(PropertyName = "items", Required = Required.Always)]
        public List<InvoiceItem> Items { get; set; }

        [JsonProperty(PropertyName = "totals", Required = Required.Always)]
        public InvoiceTotals Totals { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public TextBlock Notes { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public Labels Labels { get; set; } = new Labels();

        [JsonProperty(PropertyName = "colors")]
        public Colors Colors { get; set; } = new Colors();
    }
}
