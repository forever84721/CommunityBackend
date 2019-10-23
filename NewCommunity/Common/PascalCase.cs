using Service.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewCommunity.Common
{
    public class PascalCase : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (name?.Length > 0)
            {
                StringBuilder sb = new StringBuilder(name[0].ToString(CultureInfo.CurrentCulture).ToUpper(CultureInfo.CurrentCulture));
                return sb.Append(name[1..]).ToString();
            }
            return "";
        }
    }
}