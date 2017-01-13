using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model {

    public class Labels
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; } = "INVOICE #";

        [JsonProperty(PropertyName = "sellerLabel")]
        public string SellerLabel { get; set; } = "Bill From:";

        [JsonProperty(PropertyName = "sellerTaxIdsLabel")]
        public string SellerTaxIdsLabel { get; set; } = "Tax ID(s)";

        [JsonProperty(PropertyName = "buyerLabel")]
        public string BuyerLabel { get; set; } = "Bill To:";

        [JsonProperty(PropertyName = "buyerTaxIdsLabel")]
        public string BuyerTaxIdsLabel { get; set; } = "Tax ID(s)";

        [JsonProperty(PropertyName = "itemsListItem")]
        public string ItemsListItem { get; set; } = "Item";

        [JsonProperty(PropertyName = "itemsListPrice")]
        public string ItemsListPrice { get; set; } = "Price";

        [JsonProperty(PropertyName = "itemsListQty")]
        public string ItemsListQty { get; set; } = "Qty";

        [JsonProperty(PropertyName = "itemsListSubtotal")]
        public string ItemsListSubtotal { get; set; } = "Subtotal";

        [JsonProperty(PropertyName = "itemsListTaxes")]
        public string ItemsListTaxes { get; set; } = "Taxes";

        [JsonProperty(PropertyName = "itemsListTotal")]
        public string ItemsListTotal { get; set; } = "Amount";

        [JsonProperty(PropertyName = "subtotal")]
        public string Subtotal { get; set; } = "Subtotal";

        [JsonProperty(PropertyName = "total")]
        public string Total { get; set; } = "Total";
    }
}
