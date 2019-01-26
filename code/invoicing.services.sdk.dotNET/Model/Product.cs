using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace invoicing.services.sdk.dotNET.Model
{
    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")] 
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")] 
        public string Description { get; set; }
        [JsonProperty(PropertyName = "prices")] 
        public List<Price> Prices { get; set; } = new List<Price>();

    }
}
