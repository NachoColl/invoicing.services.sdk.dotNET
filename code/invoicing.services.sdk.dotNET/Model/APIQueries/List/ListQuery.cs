using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Model.APIQueries.List
{
    public class Months {
        public static int January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12;
    }

    public class ListQuery
    {
        [JsonProperty(PropertyName = "year", Required = Required.Always)]
        public int Year { get; set; }
        [JsonProperty(PropertyName = "month")]
        public int Month { get; set; } = 0;
        [JsonProperty(PropertyName = "day")]
        public int Day { get; set; } = 0;

        [JsonProperty(PropertyName = "quarter")]
        public int Quarter { get; set; } = 0;
    }
}
