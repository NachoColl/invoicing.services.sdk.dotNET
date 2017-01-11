using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model {

    public class Colors
    {
        const string HTMLColorRegex = "^#(?:[0-9a-fA-F]{2}){3}$";

        [JsonIgnore]
        private string color1 = "#2E2E2E";
        [JsonProperty(PropertyName = "color1")]
        public string Color1 { get { return color1; } set { if (Regex.IsMatch(value,HTMLColorRegex)) color1 = value; } }

        [JsonIgnore]
        private string color2 = "#585858";
        [JsonProperty(PropertyName = "color2")]
        public string Color2 { get { return color2; } set { if (Regex.IsMatch(value, HTMLColorRegex)) color2 = value; } }

        [JsonIgnore]
        private string color3 = "#424242";
        [JsonProperty(PropertyName = "color3")]
        public string Color3 { get { return color3; } set { if (Regex.IsMatch(value, HTMLColorRegex)) color3 = value; } }

    }
}
