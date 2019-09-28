using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public interface ISetting
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
        public string IdentityConnection { get; set; }
    }
    public class ApplicationSettings: ISetting
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
        public string IdentityConnection { get; set; }
    }
}
