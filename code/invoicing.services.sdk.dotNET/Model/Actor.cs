using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;

namespace invoicing.services.sdk.dotNET.Model {

    public class Actor {

        [JsonProperty(PropertyName = "id", Order = 1)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name", Order = 2, Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "line1", Order = 3)]
        public string Line1 { get; set; }

        [JsonProperty(PropertyName = "line2", Order = 4)]
        public string Line2 { get; set; }

        [JsonProperty(PropertyName = "line3", Order = 5)]
        public string Line3 { get; set; }

        [JsonProperty(PropertyName = "taxIds", Order = 6)]
        public List<ActorTaxId> TaxIds { get; set; }

        /// <summary>
        /// Use this when calling seller/update API method.
        /// </summary>
        [JsonProperty(PropertyName = "logoUrl", Order = 7)]
        public string LogoUrl { get; set; }

        public bool ShouldSerializeTaxIds() {
            return (HasTaxIds());
        }

        public bool ShouldDeSerializeTaxIds() {
            return (HasTaxIds());
        }

        public bool HasTaxIds() {
            if (TaxIds != null && TaxIds.Count > 0) {
                bool hasValues = false;
                foreach (ActorTaxId tax in TaxIds)
                    hasValues = hasValues || !string.IsNullOrWhiteSpace(tax.Name);
                return hasValues;
            }
            else return false;       
        }
    }
}
