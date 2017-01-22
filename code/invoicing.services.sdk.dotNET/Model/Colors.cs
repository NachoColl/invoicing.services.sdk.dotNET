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

        /// <summary>
        /// Text Color 1.
        /// </summary>
        [JsonProperty(PropertyName = "color1")]
        public string Color1 { get { return color1; } set { if (Regex.IsMatch(value,HTMLColorRegex)) color1 = value; } }

        [JsonIgnore]
        private string color2 = "#a6a6a6";
        /// <summary>
        /// Text Color 2.
        /// </summary>
        [JsonProperty(PropertyName = "color2")]
        public string Color2 { get { return color2; } set { if (Regex.IsMatch(value, HTMLColorRegex)) color2 = value; } }

        [JsonIgnore]
        private string color3 = "#424242";
        /// <summary>
        /// Text Color 3.
        /// </summary>
        [JsonProperty(PropertyName = "color3")]
        public string Color3 { get { return color3; } set { if (Regex.IsMatch(value, HTMLColorRegex)) color3 = value; } }

        [JsonIgnore]
        private string color4 = "#e62e00";
        /// <summary>
        /// Text Color 4.
        /// </summary>
        [JsonProperty(PropertyName = "color4")]
        public string Color4 { get { return color4; } set { if (Regex.IsMatch(value, HTMLColorRegex)) color4 = value; } }

        [JsonIgnore]
        private string color5 = "#a6a6a6";
        /// <summary>
        /// Line Color.
        /// </summary>
        [JsonProperty(PropertyName = "color5")]
        public string Color5 { get { return color5; } set { if (Regex.IsMatch(value, HTMLColorRegex)) color5 = value; } }

        [JsonIgnore]
        private string color6 = "#b3d9ff";
        /// <summary>
        /// Background Color.
        /// </summary>
        [JsonProperty(PropertyName = "color6")]
        public string Color6 { get { return color6; } set { if (Regex.IsMatch(value, HTMLColorRegex)) color6 = value; } }

    }
}
