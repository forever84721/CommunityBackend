using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCommunity.Common
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1724:類型名稱不應與命名空間相符", Justification = "<暫止>")]
    public static class Utils
    {
        public static DateTime GetDateTimeWithoutMillisecond()
        {
            var Now = DateTime.Now;
            return Now.AddTicks(-(Now.Ticks % TimeSpan.TicksPerSecond));
        }
    }
}
