using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Utils
{
    public class Timestamp
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long CurrentTimeMillis() {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        public static long TimeToMillis(DateTime TimeStamp) {
            return (long)(TimeStamp - Jan1st1970).TotalMilliseconds;
        }
    }
}
